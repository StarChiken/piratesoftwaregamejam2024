using System;
using System.Collections.Generic;
using Base.Core.Managers;

namespace Base.Core.Managers
{
    [Serializable]
    public class Devotion
    {
        public int DevotionPoints;
        public Dictionary<MiracleType, int> DevotionActionsList; // Dictionary to store counts of devotion actions for each miracle type
        
        public Dictionary<CommandmentType, bool> CommandmentsList; // Dictionary to store counts of commandments for each commandment type
        
        public Devotion(int startingPoints)
        {
            DevotionPoints = startingPoints;
            InitializeMiraclesAndCursesList();
            InitializeCommandmentsList();
        }
        
        // Method to add a commandment and update devotion actions
        public void AddCommandment(CommandmentType commandment)
        {
            switch (commandment)
            {
                // Red Commandments
                case CommandmentType.Prayer:
                case CommandmentType.ReadingScripture:
                    DevotionActionsList[MiracleType.RedBasic]++;
                    break;
                case CommandmentType.CopyingText:
                case CommandmentType.Research:
                    DevotionActionsList[MiracleType.RedIntermediate]++;
                    break;
                case CommandmentType.Confessions:
                case CommandmentType.Exorcism:
                case CommandmentType.Alchemy:
                    DevotionActionsList[MiracleType.RedSuperior]++;
                    break;

                // Blue Commandments
                case CommandmentType.Feast:
                case CommandmentType.Creation:
                    DevotionActionsList[MiracleType.BlueBasic]++;
                    break;
                case CommandmentType.Dance:
                case CommandmentType.Song:
                    DevotionActionsList[MiracleType.BlueIntermediate]++;
                    break;
                case CommandmentType.RitualisticAction:
                case CommandmentType.RitualisticPunishment:
                    DevotionActionsList[MiracleType.BlueSuperior]++;
                    break;

                // Green Commandments
                case CommandmentType.MaterialOfferings:
                case CommandmentType.ReligiousCultivation:
                    DevotionActionsList[MiracleType.GreenBasic]++;
                    break;
                case CommandmentType.Relics:
                case CommandmentType.Shrines:
                    DevotionActionsList[MiracleType.GreenIntermediate]++;
                    break;
                case CommandmentType.GatheringBlood:
                case CommandmentType.Donations:
                case CommandmentType.RitualSacrifice:
                    DevotionActionsList[MiracleType.GreenSuperior]++;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(commandment), commandment, null);
            }

            CommandmentsList[commandment] = true;
        }
        
        // Method to check citizens for their likeness to a miracle type
        private void CheckCitizensForMiracleLikeness(MiracleType miracleType, List<Citizen> targetCitizens)
        {
            foreach (Citizen citizen in targetCitizens)
            {
                var trait = citizen.FaithAttractionTrait;
                if (IsTraitMatchingMiracle(trait, miracleType))
                {
                    Dictionary<FaithType, int> dic = citizen.CitizenFaith.FaithTypeDictionary;
                    
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
        
        public void ChangeDevotionAmount(int amount)
        {
            DevotionPoints += amount;
        }

        // Method to perform a miracle on a target citizen
        public void DoMiracle(MiracleType type, Citizen targetCitizen)
        {
            switch (type)
            {
                // Temporary calculations, attribute starts at 0, miracles do basic amount, commandments add variation.
                case MiracleType.RedBasic:
                    targetCitizen.Health += 1;
                    break;
                case MiracleType.RedIntermediate:
                    targetCitizen.Health += 2;
                    break;
                case MiracleType.RedSuperior:
                    targetCitizen.Health += 5;
                    break;
                case MiracleType.BlueBasic:
                    targetCitizen.Sanity += 1;
                    break;
                case MiracleType.BlueIntermediate:
                    targetCitizen.Sanity += 2;
                    break;
                case MiracleType.BlueSuperior:
                    targetCitizen.Sanity += 5;
                    break;
                case MiracleType.GreenBasic:
                    targetCitizen.Happiness += 1;
                    break;
                case MiracleType.GreenIntermediate:
                    targetCitizen.Happiness += 2;
                    break;
                case MiracleType.GreenSuperior:
                    targetCitizen.Happiness += 5;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        
        // Method to get the faith attraction amount for a specific miracle type
        public int MiracleFaithAttractionByType(MiracleType type)
        {
            DevotionActionsList.TryGetValue(type, out int amount);
            
            return amount;
        }
        
        private void InitializeCommandmentsList()
        {
            CommandmentsList = new Dictionary<CommandmentType, bool>
            {
                { CommandmentType.ReadingScripture, false },
                { CommandmentType.CopyingText, false },
                { CommandmentType.Research, false },
                { CommandmentType.Prayer, false },
                { CommandmentType.Confessions, false },
                { CommandmentType.Exorcism, false },
                { CommandmentType.Alchemy, false },
                { CommandmentType.Dance, false },
                { CommandmentType.Song, false },
                { CommandmentType.Feast, false },
                { CommandmentType.Creation, false },
                { CommandmentType.RitualisticAction, false },
                { CommandmentType.RitualisticPunishment, false },
                { CommandmentType.RitualSacrifice, false },
                { CommandmentType.MaterialOfferings, false },
                { CommandmentType.Relics, false },
                { CommandmentType.Shrines, false },
                { CommandmentType.GatheringBlood, false },
                { CommandmentType.Donations, false },
                { CommandmentType.ReligiousCultivation, false }
            };
        }
        
        private void InitializeMiraclesAndCursesList()
        {
            DevotionActionsList = new Dictionary<MiracleType, int>
            {
                { MiracleType.RedBasic, 0 },
                { MiracleType.RedIntermediate, 0 },
                { MiracleType.RedSuperior, 0 },
                { MiracleType.BlueBasic, 0 },
                { MiracleType.BlueIntermediate, 0 },
                { MiracleType.BlueSuperior, 0 },
                { MiracleType.GreenBasic, 0 },
                { MiracleType.GreenIntermediate, 0 },
                { MiracleType.GreenSuperior, 0 }
            };
        }
        
        // Method to check if a citizen's trait matches a specific miracle type // Same in Gameplay.cs maybe do static
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
    }

    public class Commandment
    {
        private CommandmentType _commandmentType;
        
        public void CommandmentEffect()
        {
            
        }
    }
}