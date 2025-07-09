using System;
using System.Collections.Generic;
using Base.Core.Managers;
using TMPro;
using UnityEngine;

namespace Base.Gameplay
{
    /// <summary>
    /// Central orchestrator for game state, events, and high-level gameplay coordination.
    /// </summary>
    public class GameOrchestrator : MonoBehaviour
    {
        public Player Player { get; private set; }
        public City City { get; private set; }
        public RandomEvents GameEvents { get; private set; }
        public GameState CurrentState { get; private set; }

        [Header("UI References")]
        public TextMeshProUGUI EventText;
        public TextMeshProUGUI DevotionPointsText;
        public TextMeshProUGUI DevotionTierText;
        public TextMeshProUGUI GameStateText;
        public GameObject CommandmentPanel;
        public CanvasGroup UI;
        public List<int> DevotionMilestones = new() { 12, 48, 192, 768 };

        public event Action<GameState> OnStateChanged;

        private void Awake()
        {
            GameManager gameManager = null;
            gameManager = new GameManager(() =>
            {
                Player = gameManager.Player;
                City = gameManager.City;
                GameEvents = gameManager.GameEvents;
                // Optionally: Initialize UI, subscribe to events, etc.
            });

            // Subscribe to ActionButton and MiracleButton events
            foreach (var actionButton in FindObjectsOfType<ActionButton>())
            {
                actionButton.OnActionTriggered += DoFactionAction;
            }
            foreach (var miracleButton in FindObjectsOfType<MiracleButton>())
            {
                miracleButton.OnMiracleTriggered += DoMiracleOnCitizens;
            }
        }

        /// <summary>
        /// Starts the game and sets the initial state.
        /// </summary>
        public void StartGame()
        {
            ChangeState(GameState.StartGamePhase);
        }

        /// <summary>
        /// Performs a miracle on a list of citizens.
        /// </summary>
        public void DoMiracleOnCitizens(MiracleType miracleType, List<Citizen> targetCitizens)
        {
            int devotionPoints = Player.Devotion.DevotionPoints;
            if (devotionPoints > 0)
            {
                Player.Devotion.ChangeDevotionAmount(-1);
                Debug.Log($"A <color=red>{miracleType}</color> is being cast...");
                foreach (var citizen in targetCitizens)
                {
                    Player.Devotion.DoMiracle(miracleType, citizen);
                    if (TraitMiracleMatcher.IsTraitMatchingMiracle(citizen.FaithAttractionTrait, miracleType))
                    {
                        int attractionAmount = Player.Devotion.MiracleFaithAttractionByType(miracleType);
                        citizen.ChangeAttractionAmount(attractionAmount);
                        Debug.Log($"<color=red>{citizen.CitizenName}</color> is happy about <color=red>{miracleType}</color>, " +
                                  $"because he is a {citizen.FaithAttractionTrait}. His faith attraction is now {citizen.PlayerGodAttraction}");
                    }
                }
            }
            else
            {
                Debug.Log($"A <color=red>{miracleType}</color> was NOT cast! 0 Devotion Points");
            }
        }

        /// <summary>
        /// Performs a faction action if the faction alignment is sufficient.
        /// </summary>
        public void DoFactionAction(FactionAction factionAction, Faction faction)
        {
            int requiredAlignment = factionAction switch
            {
                FactionAction.GetResource => 10,
                FactionAction.GetFavor => 11,
                FactionAction.GetInfluence => 50,
                _ => throw new ArgumentOutOfRangeException()
            };
            if (faction.FactionAlignment >= requiredAlignment)
            {
                faction.DoAction(factionAction);
            }
            else
            {
                Debug.Log($"A <color=red>{factionAction}</color> was NOT performed! FactionAlignment is below {requiredAlignment}");
            }
        }

        /// <summary>
        /// Shows a random event and updates devotion points UI.
        /// </summary>
        public void ShowEvent()
        {
            EventText.text = GameEvents.DoEventGiveDevotionPoints();
            DevotionPointsText.text = Player.Devotion.DevotionPoints.ToString();
        }

        /// <summary>
        /// Reports about the current game state (for debugging/testing).
        /// </summary>
        public void ReportAboutGame()
        {
            Debug.Log($"<color=red>{Player.CharacterName}</color> has <color=red>{Player.FollowerCount.Count}</color> followers, " +
                      $"and <color=red>{Player.Devotion.DevotionPoints}</color> Devotion Points.");
            var followerList = new List<Citizen>(Player.FollowerCount);
            Citizen follower = RandomUtil.GetRandom(followerList);
            Debug.Log($"A random follower named, <color=red>{follower.CitizenName}</color> with Faith Attraction: <color=red>{follower.PlayerGodAttraction}</color>");
        }

        /// <summary>
        /// Changes the current game state and updates UI accordingly.
        /// </summary>
        public void ChangeState(GameState newState)
        {
            CurrentState = newState;
            OnStateChanged?.Invoke(newState);
            switch (newState)
            {
                case GameState.StartGamePhase:
                    DevotionPointsText.text = Player.Devotion.DevotionPoints.ToString();
                    GameStateText.text = "Start Game Phase, Choose First Commandment";
                    CommandmentPanel.GetComponent<EventsPanel>().OpenPanel(PanelType.ChooseCommandment);
                    break;
                case GameState.PlayerTurnPhase:
                    DevotionPointsText.text = Player.Devotion.DevotionPoints.ToString();
                    GameStateText.text = "Player Phase, Choose A Miracle To Be Cast On Random City District";
                    CommandmentPanel.SetActive(false);
                    break;
                case GameState.CalculateCityPhase:
                    DevotionPointsText.text = Player.Devotion.DevotionPoints.ToString();
                    GameStateText.text = "Calculate City Phase";
                    CalculateFaithAttractionForCity();
                    CalculateBonusDevotionPoints();
                    break;
                case GameState.GetCommandmentPhase:
                    DevotionPointsText.text = Player.Devotion.DevotionPoints.ToString();
                    GameStateText.text = "Get Commandment Phase, Choose Next Commandment, Evolve Your Religion";
                    CommandmentPanel.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        /// <summary>
        /// Calculates bonus devotion points based on followers' faith.
        /// </summary>
        private void CalculateBonusDevotionPoints()
        {
            var followerCount = Player.FollowerCount;
            if (followerCount.Count == 0)
            {
                ChangeState(GameState.PlayerTurnPhase);
                return;
            }
            int tempFaith = 0;
            foreach (Citizen follower in followerCount)
            {
                int temp = follower.PlayerGodAttraction;
                tempFaith += temp;
            }
            int calculatedFaith = 1 - tempFaith % 2;
            Player.Devotion.ChangeDevotionAmount(calculatedFaith);
            foreach (int milestone in DevotionMilestones)
            {
                if (tempFaith >= milestone)
                {
                    ChangeState(GameState.GetCommandmentPhase);
                    DevotionTierText.text = milestone.ToString();
                    DevotionMilestones.Remove(milestone);
                    return;
                }
            }
            ChangeState(GameState.PlayerTurnPhase);
        }

        /// <summary>
        /// Calculates faith attraction for all citizens in the city.
        /// </summary>
        private void CalculateFaithAttractionForCity()
        {
            var districts = City.Districts;
            foreach (var district in districts)
            {
                var districtPop = district.DistrictPopulace;
                foreach (Citizen citizen in districtPop)
                {
                    CheckIfCitizensCanBecomeFollowers(citizen, (List<Citizen>)Player.FollowerCount);
                }
                Debug.Log("<color=red>Calculated faith attraction to all citizens!</color>");
            }
        }

        /// <summary>
        /// Checks if a citizen can become a follower and adds them if so.
        /// </summary>
        private void CheckIfCitizensCanBecomeFollowers(Citizen citizen, List<Citizen> followers)
        {
            if (citizen.PlayerGodAttraction < 3) return;
            Debug.Log($"<color=red>{citizen.CitizenName}</color> has joined you, with <color=red>{citizen.PlayerGodAttraction}</color>");
            followers.Add(citizen);
        }
    }

    public enum GameState
    {
        StartGamePhase = 0,
        PlayerTurnPhase = 1,
        CalculateCityPhase = 2,
        GetCommandmentPhase = 3,
    }
} 