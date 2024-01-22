using System;
using System.Collections.Generic;
using Base.Core.Components;
using Base.Core.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Base.Gameplay.UI
{
    public class Gameplay : MyMonoBehaviour
    {
        // Game Order Stuff
        public GameObject commandmentPanel;
        public GameObject eventsPanel;
        [SerializeField] private TextMeshProUGUI myGameStateText;
        [SerializeField] private TextMeshProUGUI devotionPointsText;
        [SerializeField] private TextMeshProUGUI devotionTierText;

        public GameState myGameState;
        public Button RedMiraclebutton;
        public Button BlueMiraclebutton;
        public Button GreenMiraclebutton;
        
        // Events Thing
        public TextMeshProUGUI EventText;
        public List<int> devotionMilestones = new() { 12,48,192,768 };

        // Player Data
        private Player player;
        private City city;
        
        // Get All Player & City Variables
        private void Awake()
        {
            new GameManager(() =>
            {
                player = GameManager.Player;
                city = GameManager.City;
            });
        }
        
        // Starting a state machine, called by a button in test environment
        public void StartGame()
        {
            ChangeState(GameState.StartGamePhase);
        }
        
        // Random Event Test, called by a button in test environment
        public void ShowEvent()
        {
            EventText.text = GameManager.GameEvents.DoEventGiveDevotionPoints();
            devotionPointsText.text = GameManager.Player.Devotion.DevotionPoints.ToString();
        }
        
        private void CalculateBonusDevotionPoints()
        {
            List<Citizen> followerCount = GameManager.Player.FollowerCount;
            
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

            int calculatedFaith = 1 - tempFaith % 2; // Temporary calculation, returns 0 or 1
            
            GameManager.Player.Devotion.ChangeDevotionAmount(calculatedFaith);

            foreach (int milestone in devotionMilestones)
            {
                if (tempFaith >= milestone)
                {
                    ChangeState(GameState.GetCommandmentPhase);
                    devotionTierText.text = milestone.ToString();
                    devotionMilestones.Remove(milestone);
                    return; 
                }
            }
            
            ChangeState(GameState.PlayerTurnPhase);
        }
        
        // Test, called by a button in test environment
        public void ReportAboutGame()
        {
            var player = GameManager.Player;
            
            Debug.Log($"<color=red>{player.CharacterName}</color> has <color=red>{player.FollowerCount.Count}</color> followers, " +
                      $"and <color=red>{player.Devotion.DevotionPoints}</color> Devotion Points.");
            
            Citizen follower = player.FollowerCount[Random.Range(0, player.FollowerCount.Count)];
            
            Debug.Log($"A random follower named, <color=red>{follower.CitizenName}</color> with Faith Attraction:" +
                      $" <color=red>{follower.PlayerGodAttraction}</color>");
        }
        
        public void ChooseCommandment(CommandmentType commandment)
        {
            player.Devotion.AddCommandment(commandment);
            ChangeState(GameState.PlayerTurnPhase);
        }

        private void CheckIfCitizensCanBecomeFollowers(Citizen citizen, List<Citizen> followers)
        {
            if (citizen.PlayerGodAttraction < 3) return; // Temporary calculation, attraction starts at 0, add citizen at 3
            
            Debug.Log($"<color=red>{citizen.CitizenName}</color> has joined you, with <color=red>{citizen.PlayerGodAttraction}</color>");
            
            followers.Add(citizen);
        }

        private bool IsTraitMatchingMiracle(TraitType citizenTrait, MiracleType miracleType)
        {
            switch (citizenTrait)
            {
                case TraitType.Academic:
                case TraitType.Apologist:
                case TraitType.Spiritual:
                    return miracleType is MiracleType.RedBasic or MiracleType.RedIntermediate or MiracleType.RedSuperior;
                case TraitType.Collector:
                case TraitType.Witch:
                    return miracleType is MiracleType.RedBasic or MiracleType.RedIntermediate or MiracleType.RedSuperior 
                        or MiracleType.GreenBasic or MiracleType.GreenIntermediate or MiracleType.GreenSuperior;
                case TraitType.Poet:
                case TraitType.Scheduled:
                    return miracleType is MiracleType.RedBasic or MiracleType.RedIntermediate or MiracleType.RedSuperior or MiracleType.BlueBasic;
                case TraitType.Performer:
                case TraitType.Naturalist:
                case TraitType.Soldier:
                    return miracleType is MiracleType.BlueBasic or MiracleType.BlueIntermediate or MiracleType.BlueSuperior;
                case TraitType.Aesthetic:
                case TraitType.Masochist:
                    return miracleType is MiracleType.GreenBasic or MiracleType.GreenIntermediate or MiracleType.GreenSuperior
                        or MiracleType.BlueBasic or MiracleType.BlueIntermediate or MiracleType.BlueSuperior;
                case TraitType.Fanatic:
                case TraitType.Noble:
                case TraitType.Farmer:
                    return miracleType is MiracleType.GreenBasic or MiracleType.GreenIntermediate or MiracleType.GreenSuperior;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void DoMiracleOnCitizens(MiracleType miracleType, List<Citizen> targetCitizens)
        {
            int devotionPoints = GameManager.Player.Devotion.DevotionPoints;
            
            if (devotionPoints > 0)
            {
                GameManager.Player.Devotion.ChangeDevotionAmount(-1);
                Debug.Log($"A <color=red>{miracleType}</color> is being cast...");

                foreach (Citizen citizen in targetCitizens)
                {
                    GameManager.Player.Devotion.DoMiracle(miracleType, citizen);
                    
                    // Check if the citizen's trait matches the miracle type
                    if (IsTraitMatchingMiracle(citizen.FaithAttractionTrait, miracleType))
                    {
                        int attractionAmount = GameManager.Player.Devotion.MiracleFaithAttractionByType(miracleType);
                        citizen.ChangeAttractionAmount(attractionAmount);

                        Debug.Log($"<color=red>{citizen.CitizenName}</color> is happy about <color=red>{miracleType.ToString()}</color>, " +
                                  $"because he is a {citizen.FaithAttractionTrait}. His faith attraction is now {citizen.PlayerGodAttraction}");
                    }
                }
                
                ChangeState(GameState.CalculateCityPhase);
            }
            else
            {
                Debug.Log($"Not enough Devotion Points, currently <color=red>{devotionPoints}</color. Skipping turn.");
                ChangeState(GameState.CalculateCityPhase);
            }
        }
        
        private void CalculateFaithAttractionForCity()
        {
            var cityPop = GameManager.City.CityPopulace;
            // calculate faith attraction for all citizens
            foreach (Citizen citizen in cityPop)
            {
                CheckIfCitizensCanBecomeFollowers(citizen, player.FollowerCount);
            }
            
            Debug.Log("<color=red>Calculated faith attraction to all citizens!</color>");
        }
        
        // Testing Purposes Only
        public void ChangeState(GameState newState)
        {
            myGameState = newState;
            switch (newState)
            {
                case GameState.StartGamePhase:
                    devotionPointsText.text = GameManager.Player.Devotion.DevotionPoints.ToString();
                    myGameStateText.text = "Start Game Phase, Choose First Commandment";
                    
                    commandmentPanel.SetActive(true);
                    RedMiraclebutton.interactable = false;
                    BlueMiraclebutton.interactable = false;
                    GreenMiraclebutton.interactable = false;
                    
                    // wait for player input
                    // Choose First Commandment
                    break;
                
                case GameState.PlayerTurnPhase:
                    devotionPointsText.text = GameManager.Player.Devotion.DevotionPoints.ToString();
                    myGameStateText.text = "Player Phase, Choose A Miracle To Be Cast On Random City Sector";
                    
                    commandmentPanel.SetActive(false);
                    RedMiraclebutton.interactable = true;
                    BlueMiraclebutton.interactable = true;
                    GreenMiraclebutton.interactable = true;
                    
                    // wait for player input
                    // do miracles
                    break;
                
                case GameState.CalculateCityPhase:
                    devotionPointsText.text = GameManager.Player.Devotion.DevotionPoints.ToString();
                    myGameStateText.text = "Calculate City Phase";
                    
                    CalculateFaithAttractionForCity();
                    CalculateBonusDevotionPoints();
                    break;
                
                case GameState.GetCommandmentPhase:
                    devotionPointsText.text = GameManager.Player.Devotion.DevotionPoints.ToString();
                    myGameStateText.text = "Get Commandment Phase, Choose Next Commandment, Evolve Your Religion";
                    
                    commandmentPanel.SetActive(true);
                    RedMiraclebutton.interactable = false;
                    BlueMiraclebutton.interactable = false;
                    GreenMiraclebutton.interactable = false;
                    
                    // wait for player input
                    // choose commandments
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
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