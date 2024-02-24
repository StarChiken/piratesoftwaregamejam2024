using System;
using System.Collections.Generic;
using Base.Core.Components;
using Base.Core.Managers;
using TMPro;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Base.Gameplay
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
        public CanvasGroup UI;
        
        // Events Thing
        public TextMeshProUGUI EventText;
        public List<int> devotionMilestones = new() { 12,48,192,768 };

        // Player Data
        public Player player;
        public City city;
        
        // Get All Player & City Variables
        private void Awake()
        {
            new GameManager(() =>
            {
                player = GameManager.Player;
                city = GameManager.City;

                InitializeSectorObjects();
            });
        }

        private void InitializeSectorObjects()
        {
            SectorScript[] sectorScriptList = FindObjectsByType<SectorScript>(FindObjectsSortMode.None);
            
            for (int i = 0; i < sectorScriptList.Length; i++)
            {
                // Access the corresponding sector using the loop index
                Sector sector = city.Sectors[i];
                SectorScript sectorScript = sectorScriptList[i];

                // Assign the sector name to the SectorScript
                sectorScript.sector = sector;
                sectorScript.name.text = $"{sectorScript.sector.SectorName}";
            }
        }


        public void DoMiracleOnCitizens(MiracleType miracleType, List<Citizen> targetCitizens)
        {
            int devotionPoints = GameManager.Player.Devotion.DevotionPoints;
            
            if (devotionPoints > 0)
            {
                GameManager.Player.Devotion.ChangeDevotionAmount(-1);
                
                Debug.Log($"A <color=red>{miracleType}</color> is being cast...");
                
                foreach (var citizen in targetCitizens)
                {
                    GameManager.Player.Devotion.DoMiracle(miracleType, citizen);
                    
                    // Match Miracle Type to Trait Type and Change Attraction
                    if (IsTraitMatchingMiracle(citizen.FaithAttractionTrait, miracleType))
                    {
                        int attractionAmount = GameManager.Player.Devotion.MiracleFaithAttractionByType(miracleType);
                        citizen.ChangeAttractionAmount(attractionAmount);

                        Debug.Log($"<color=red>{citizen.CitizenName}</color> is happy about <color=red>{miracleType.ToString()}</color>, " +
                                  $"because he is a {citizen.FaithAttractionTrait}. His faith attraction is now {citizen.PlayerGodAttraction}");
                    }
                }
            }
            else
            {
                Debug.Log($"A <color=red>{miracleType}</color> was NOT cast! 0 Devotion Points");
            }
        }

        public void DoFactionAction(FactionAction factionAction, Faction faction)
        {
            switch (factionAction)
            {
                case FactionAction.GetResource:
                    if (faction.FactionAlignment >= 10)
                    {
                        faction.DoAction(factionAction);

                    }
                    else
                    {
                        Debug.Log($"A <color=red>{factionAction}</color> was NOT preformed! " +
                                  $"FactionAlignment is bellow 10");
                    }
                    break;
                
                case FactionAction.GetFavor:
                    if (faction.FactionAlignment >= 11)
                    {
                        faction.DoAction(factionAction);

                    }
                    else
                    {
                        Debug.Log($"A <color=red>{factionAction}</color> was NOT preformed! " +
                                  $"FactionAlignment is bellow 10");
                    }
                    break;
                
                case FactionAction.GetInfluence:
                    if (faction.FactionAlignment >= 50)
                    {
                        faction.DoAction(factionAction);

                    }
                    else
                    {
                        Debug.Log($"A <color=red>{factionAction}</color> was NOT preformed! " +
                                  $"FactionAlignment is bellow 50");
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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
        
        private void CalculateFaithAttractionForCity()
        {
            var sectors = GameManager.City.Sectors;

            foreach (var sector in sectors)
            {
                var sectorPop = sector.SectorPopulace;
                
                // calculate faith attraction for all citizens
                foreach (Citizen citizen in sectorPop)
                {
                    CheckIfCitizensCanBecomeFollowers(citizen, GameManager.Player.FollowerCount);
                }
            
                Debug.Log("<color=red>Calculated faith attraction to all citizens!</color>");
            }
        }
        
        private void CheckIfCitizensCanBecomeFollowers(Citizen citizen, List<Citizen> followers)
        {
            if (citizen.PlayerGodAttraction < 3) return; // Temporary calculation, attraction starts at 0, add citizen at 3
            
            Debug.Log($"<color=red>{citizen.CitizenName}</color> has joined you, with <color=red>{citizen.PlayerGodAttraction}</color>");
            
            followers.Add(citizen);
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
        
        // Test, called by a button in test environment
        public void ReportAboutGame()
        {
            Debug.Log($"<color=red>{GameManager.Player.CharacterName}</color> has <color=red>{GameManager.Player.FollowerCount.Count}</color> followers, " +
                      $"and <color=red>{GameManager.Player.Devotion.DevotionPoints}</color> Devotion Points.");
            
            Citizen follower = GameManager.Player.FollowerCount[Random.Range(0, GameManager.Player.FollowerCount.Count)];
            
            Debug.Log($"A random follower named, <color=red>{follower.CitizenName}</color> with Faith Attraction:" +
                      $" <color=red>{follower.PlayerGodAttraction}</color>");
        }
        
        public void ChooseRedCommandment()
        {
            GameManager.Player.Devotion.AddCommandment(CommandmentType.Prayer);
            ChangeState(GameState.PlayerTurnPhase);
        }
        
        public void ChooseGreenCommandment()
        {
            GameManager.Player.Devotion.AddCommandment(CommandmentType.MaterialOfferings);
            ChangeState(GameState.PlayerTurnPhase);
        }
        
        public void ChooseBlueCommandment()
        {
            GameManager.Player.Devotion.AddCommandment(CommandmentType.Feast);
            ChangeState(GameState.PlayerTurnPhase);
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

                    commandmentPanel.GetComponent<EventsPanel>().OpenPanel(panelType.ChooseCommandment);
                    
                    // wait for GameManager.Player input
                    // Choose First Commandment
                    break;
                
                case GameState.PlayerTurnPhase:
                    devotionPointsText.text = GameManager.Player.Devotion.DevotionPoints.ToString();
                    myGameStateText.text = "Player Phase, Choose A Miracle To Be Cast On Random City Sector";
                    
                    commandmentPanel.SetActive(false);
                    
                    // wait for GameManager.Player input
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
                    // wait for GameManager.Player input
                    // choose commandments
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }
        
        // public GameObject MiracleObject;
        //
        // public void MiralceButton()
        // {
        //     MiracleObject.SetActive(true);
        //     MiracleObject.GetComponent<MiracleObject>().SetFollowCursor(true);
        //     print("MIRACLE BUTTON PRESSED");
        // }
    }
    public enum GameState
    {
        StartGamePhase = 0,
        PlayerTurnPhase = 1,
        CalculateCityPhase = 2,
        GetCommandmentPhase = 3,
    }
}