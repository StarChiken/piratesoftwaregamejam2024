using System;
using Base.Core.Components;
using Base.Core.Managers;
using TMPro;
using UnityEngine.EventSystems;

namespace Base.Gameplay.UI
{
    public class BuildingTile : MyMonoBehaviour,IDropHandler
    {
        public Gameplay gameplayManager;
        private MiracleButton dragedMiracleAction;
        private SectorType type;
        private ActionOptions optionsType;
        private MainBuilding building;

        public TextMeshProUGUI actionText;
        public TextMeshProUGUI favorText;
        public TextMeshProUGUI miracleText;



        private void Awake()
        {
            building = GetComponent<MainBuilding>();
        }

        public void OnConfirm()
        {
            switch (optionsType)
            {
                case ActionOptions.DoMiracle:
                    CheckSectorCitizensForMiracleLikeness(dragedMiracleAction);
                    break;
                
                case ActionOptions.DoAction:
                    switch (type)
                    {
                        case SectorType.Entertainment:
                            building.DoAction();
                            break;
                        case SectorType.Park:
                            
                            break;
                        case SectorType.Business:
                            
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case ActionOptions.AskFavor:
                    switch (type)
                    {
                        case SectorType.Entertainment:
                            
                            break;
                        case SectorType.Park:
                            
                            break;
                        case SectorType.Business:
                            
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            MiracleButton miracleButton = eventData.pointerDrag.GetComponent<MiracleButton>();

            if (miracleButton != null)
            {
                // miracleButton.DoMiracleButton();
                miracleText.text = miracleButton.miracleType.ToString();
                CheckSectorCitizensForMiracleLikeness(miracleButton);
            }
            else
            {
                switch (type)
                {
                    case SectorType.Entertainment:
                        ShowActionDetails(SectorType.Entertainment);
                        break;
                    case SectorType.Park:
                        ShowActionDetails(SectorType.Park);
                        break;
                    case SectorType.Business:
                        ShowActionDetails(SectorType.Business);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void CheckSectorCitizensForMiracleLikeness(MiracleButton miracleButton)
        {
            var thisSectorPopulace = building.sectorOfOrigion.SectorPopulace;
            var miracleType = miracleButton.miracleType;

            foreach (var citizen in thisSectorPopulace)
            {
                var trait = citizen.FaithAttractionTrait;
                if (gameplayManager.IsTraitMatchingMiracle(trait, miracleType))
                {
                    var dic = citizen.CitizenFaith.FaithTypeDictionary;
                    switch (trait)
                    {
                        case TraitType.Academic:
                            dic[FaithType.GodOfLore]++;
                            break;
                        case TraitType.Apologist:
                            dic[FaithType.TricksterGoddess]++;
                            break;
                        case TraitType.Spiritual:
                            dic[FaithType.MotherOfTheGods]++;
                            break;
                        case TraitType.Collector:
                            dic[FaithType.SeekerGod]++;
                            break;
                        case TraitType.Witch:
                            dic[FaithType.ChaosGoddess]++;
                            break;
                        case TraitType.Poet:
                            dic[FaithType.GoddessOfPoetry]++;
                            break;
                        case TraitType.Scheduled:
                            dic[FaithType.FatherOfTheGods]++;
                            break;
                        case TraitType.Performer:
                            dic[FaithType.LoveGod]++;
                            break;
                        case TraitType.Naturalist:
                            dic[FaithType.NatureGoddess]++;
                            break;
                        case TraitType.Soldier:
                            dic[FaithType.WarGod]++;
                            break;
                        case TraitType.Aesthetic:
                            dic[FaithType.BeautyGoddess]++;
                            break;
                        case TraitType.Masochist:
                            dic[FaithType.FuryGoddess]++;
                            break;
                        case TraitType.Fanatic:
                            dic[FaithType.EvilGod]++;
                            break;
                        case TraitType.Noble:
                            dic[FaithType.WealthGod]++;
                            break;
                        case TraitType.Farmer:
                            dic[FaithType.HouseholdGod]++;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }

        private void ShowActionDetails(SectorType type)
        {
            switch (type)
            {
                case SectorType.Entertainment:
                    actionText.text = "Get Happiness for Followers";
                    favorText.text = "Get a lot of Resources";
                    break;
                case SectorType.Park:
                    actionText.text = "Get Happiness for Followers";
                    favorText.text = "Get a Preaching Place";
                    break;
                case SectorType.Business:
                    actionText.text = "Get a bit of Resources";
                    favorText.text = "Build another Temple";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}