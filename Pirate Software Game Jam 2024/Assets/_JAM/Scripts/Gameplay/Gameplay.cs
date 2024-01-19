using System;
using System.Collections.Generic;
using Base.Core.Components;
using Base.Core.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

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
        
        // Player Data
        public int _followerCount;
        public string _characterName;
        public float _devotionPoints;
        
        public List<Citizen> _followers;
        public Devotion _playerDevotion;
        
        // City Data
        public string _cityName;
        public int _sectorsCount;
        public List<Sector> _citySectors;
        
        // Events Thing
        public TextMeshProUGUI EventText;
        public List<int> devotionMilestones;

        public void ShowEvent()
        {
            EventText.text = GameManager.GameEvents.DoEventGiveDevotionPoints();
            devotionPointsText.text = GameManager.Player.Devotion.DevotionPoints.ToString();
        }
        
        public void ChangeState(GameState newState)
        {
            myGameState = newState;
            switch (newState)
            {
                case GameState.StartGamePhase:
                    RedMiraclebutton.interactable = false;
                    BlueMiraclebutton.interactable = false;
                    GreenMiraclebutton.interactable = false;
                    
                    commandmentPanel.SetActive(true);
                    
                    devotionPointsText.text = GameManager.Player.Devotion.DevotionPoints.ToString();
                    myGameStateText.text = "Start Game Phase, Choose First Commandment";
                    
                    // wait for player input
                    // Choose First Commandment
                    break;
                
                case GameState.PlayerTurnPhase:
                    commandmentPanel.SetActive(false);
                    
                    RedMiraclebutton.interactable = true;
                    BlueMiraclebutton.interactable = true;
                    GreenMiraclebutton.interactable = true;
                    
                    devotionPointsText.text = GameManager.Player.Devotion.DevotionPoints.ToString();
                    myGameStateText.text = "Player Phase, Choose A Miracle To Be Cast On Random City Sector";
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
                    
                    RedMiraclebutton.interactable = false;
                    BlueMiraclebutton.interactable = false;
                    GreenMiraclebutton.interactable = false;
                    commandmentPanel.SetActive(true);
                    
                    myGameStateText.text = "Get Commandment Phase, Choose Next Commandment, Evolve Your Religion";
                    // wait for player input
                    // choose commandments
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }

        private void CalculateBonusDevotionPoints()
        {
            var followerCount = GameManager.Player.FollowerCount;
            if (followerCount.Count == 0)
            {
                ChangeState(GameState.PlayerTurnPhase);
                return;
            }
            
            int tempFaith = 0;

            foreach (var follower in followerCount)
            {
                var temp = follower.PlayerFaithAttraction;
                tempFaith += temp;
            }

            int calculatedFaith = 1 - tempFaith % 2;
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

        private void Awake()
        {
            new GameManager(() =>
            {
                _characterName = GameManager.Player.CharacterName;
                _followers = GameManager.Player.FollowerCount;
                _followerCount = _followers.Count;
                _playerDevotion = GameManager.Player.Devotion;
                _devotionPoints = _playerDevotion.DevotionPoints;
                
                _cityName = GameManager.City.CityName;
                _citySectors = GameManager.City.SectorsCount;
                _sectorsCount = GameManager.City.SectorsCount.Count;

                devotionMilestones = new List<int> { 12,48,192,768 };
            });
        }
        
        public void StartGame()
        {
            ChangeState(GameState.StartGamePhase);
        }
        
        public void ReportAboutGame()
        {
            Debug.Log($"<color=red>{_characterName}</color> has <color=red>{_followerCount}</color> starting followers, and <color=red>{_devotionPoints}</color> Devotion Points.");
            Debug.Log($"<color=red>{_cityName}</color> has <color=red>{_sectorsCount}</color> sectors.");
        }
        
        public void ChooseCommandment(CommandmentType commandment)
        {
            _playerDevotion.AddCommandment(commandment);
            ChangeState(GameState.PlayerTurnPhase);
        }
        
        public static void CheckIfCitizensCanBecomeFollowers(Citizen citizen, Sector sector, List<Citizen> followers)
        {
            if (citizen.PlayerFaithAttraction < 3) return;
            
            Debug.Log($"<color=red>{citizen.CitizenName}</color> from <color=red>{sector.SectorName}</color> " +
                      $"has joined you, with <color=red>{citizen.PlayerFaithAttraction}</color>");
            
            followers.Add(citizen);
            sector.SectorPopulace.Remove(citizen);
        }
        public void DoMiracleOnSector(Sector sector, MiracleType miracleType)
        {
            foreach (var citizen in sector.SectorPopulace)
            {
                DoMiracleOnCitizens(sector, miracleType, citizen);
            }
        }
        
        public void DoMiracleOnFollowers(MiracleType miracleType)
        {
            var followers = GameManager.Player.FollowerCount;

            foreach (var follower in followers)
            {
                if (IsTraitMatchingMiracle(follower.FaithAttractionTrait, miracleType))
                {
                    int amountOfBoostFromMiracle = GameManager.Player.Devotion.Miracle(miracleType);
                    follower.ChangeFaithAmount(amountOfBoostFromMiracle);

                    Debug.Log($"<color=red>{follower.CitizenName}</color> from your follower is happy about <color=red>{miracleType}</color>");
                }
            }
        }
        
        private bool IsTraitMatchingMiracle(TraitType citizenTrait, MiracleType miracleType)
        {
            switch (citizenTrait)
            {
                case TraitType.Academic:
                case TraitType.Apologist:
                case TraitType.Spiritual:
                    return miracleType == MiracleType.RedMiracle;
                case TraitType.Collector:
                case TraitType.Witch:
                    return miracleType == MiracleType.RedMiracle || miracleType == MiracleType.GreenMiracle;
                case TraitType.Poet:
                case TraitType.Scheduled:
                    return miracleType == MiracleType.RedMiracle || miracleType == MiracleType.BlueMiracle;
                case TraitType.Performer:
                case TraitType.Naturalist:
                case TraitType.Soldier:
                    return miracleType == MiracleType.BlueMiracle;
                case TraitType.Aesthetic:
                case TraitType.Masochist:
                    return miracleType == MiracleType.GreenMiracle || miracleType == MiracleType.BlueMiracle;
                case TraitType.Fanatic:
                case TraitType.Noble:
                case TraitType.Farmer:
                    return miracleType == MiracleType.GreenMiracle;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void DoMiracleOnCitizens(Sector sector, MiracleType miracleType, Citizen citizen)
        {
            // Check if the citizen's trait matches the miracle type
            if (IsTraitMatchingMiracle(citizen.FaithAttractionTrait, miracleType))
            {
                int amountOfBoostFromMiracle = GameManager.Player.Devotion.Miracle(miracleType);
                citizen.ChangeFaithAmount(amountOfBoostFromMiracle);

                Debug.Log($"<color=red>{citizen.CitizenName}</color> from <color=red>{sector.SectorName}</color> is happy about <color=red>{miracleType}</color>");
            }
        }
        
        private void CalculateFaithAttractionForCity()
        {
            var citySectors = GameManager.City.SectorsCount;
            // calculate faith attraction to all sectors
            foreach (var sector in citySectors)
            {
                var sectorPop = sector.SectorPopulace;

                for (var index = 0; index < sectorPop.Count; index++)
                {
                    var citizen = sectorPop[index];
                    CheckIfCitizensCanBecomeFollowers(citizen, sector, _followers);
                }
            }
            
            Debug.Log("<color=red>Calculated faith attraction to all sectors!</color>");

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