﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Base.Core.Managers
{
    [Serializable]
    public class Citizen
    {
        // Citizen properties
        public string CitizenName;
        public TraitType FaithAttractionTrait; // What kind of faith actions they like
        public int PlayerGodAttraction; // Current amount of attraction to the players faith
        public Faciton CitizenFaction;
        public Vector2 housePosition; //1 Grid Sqaure that Contains their house
        
        // Subsystem stats
        public int Health;
        public int Sanity;
        public int HealthThreshold;
        public int Sanityhreshold;
        
        
        
        // TODO REFACTOR
        public int Happiness;
        public int FactionDuty;
        public CitizenFaith CitizenFaith;
        public CitizenNeeds CitizenNeeds;

        public void InitializeCitizen()
        {
            // Initialize subsystems
            CitizenFaith = new CitizenFaith(this);
            CitizenNeeds = new CitizenNeeds();
            CitizenNeeds.CalculateNeeds(Sanity,Happiness, FactionDuty); // Calculate needs based on initial sanity and health

            // Initialize properties
            PlayerGodAttraction = 0;
        Health = 5;
        Sanity = 5;
        HealthThreshold = 5;
        Sanityhreshold = 5;
            
            //Sanity = Random.Range(45, 65); OLD VALUES
            //Happiness = Random.Range(45, 65); OLD VALUES
            FactionDuty = Random.Range(50, 90);
            Health = CalculateHappinessAmount();
            CitizenName = GenerateName();
            FaithAttractionTrait = GenerateRandomTrait();
            CitizenFaction = GenerateRandomFaction();
        }

        // Calculate happiness based on health, sanity, and personality
        public int CalculateHappinessAmount()
        {
           // int personality = CitizenFaith.LookUpFaithTypeDictionaryWithTraitType(FaithAttractionTrait);
           return (Happiness + Sanity);// * personality;
        }

        public void ChangeSanityAmount(int amount)
        {
            Sanity += amount;
        }

        public void ChangeHealthAmount(int amount)
        {
            Happiness += amount;
        }

        public void ChangeAttractionAmount(int amount)
        {
            PlayerGodAttraction += amount;
        }

        private string GenerateName()
        {
            string[] names = { "John", "Jane", "Alex", "Emily", "Michael", "Olivia", "David", "Sophia" };
            int index = UnityEngine.Random.Range(0, names.Length);
            return names[index];
        }

        private TraitType GenerateRandomTrait()
        {
            Array values = Enum.GetValues(typeof(TraitType));
            int index = UnityEngine.Random.Range(0, values.Length);
            return (TraitType)values.GetValue(index);
        }

        private Faciton GenerateRandomFaction()
        {
            Array values = Enum.GetValues(typeof(Faciton));
            int index = Random.Range(0, values.Length);
            return (Faciton)values.GetValue(index);
        }
    }

    public class CitizenFaith
    {
        public Dictionary<FaithType, int> FaithTypeDictionary;
        private Citizen _thisCitizen;
        
        public CitizenFaith(Citizen thisCitizen)
        {
            // Initialize faith types with default values
            _thisCitizen = thisCitizen;
            FaithTypeDictionary = new Dictionary<FaithType, int> {
                { FaithType.PlayerGod, 0},
                { FaithType.GodOfLore, 0 },
                { FaithType.GoddessOfPoetry, 0 },
                { FaithType.SeekerGod, 0 },
                { FaithType.FatherOfTheGods, 0 },
                { FaithType.MotherOfTheGods, 0 },
                { FaithType.TricksterGoddess, 0 },
                { FaithType.ChaosGoddess, 0 },
                { FaithType.BeautyGoddess, 0 },
                { FaithType.LoveGod, 0 },
                { FaithType.NatureGoddess, 0 },
                { FaithType.FuryGoddess, 0 },
                { FaithType.WarGod, 0 },
                { FaithType.EvilGod, 0 },
                { FaithType.WealthGod, 0 },
                { FaithType.HouseholdGod, 0 }
            };
            
            // Assign faith type values based on the citizen's trait
            switch (_thisCitizen.FaithAttractionTrait)
            {
                case TraitType.Academic:
                    FaithTypeDictionary[FaithType.GodOfLore] = 3;
                    break;
                case TraitType.Apologist:
                    FaithTypeDictionary[FaithType.TricksterGoddess] = 3;
                    break;
                case TraitType.Spiritual:
                    FaithTypeDictionary[FaithType.MotherOfTheGods] = 3;
                    break;
                case TraitType.Collector:
                    FaithTypeDictionary[FaithType.SeekerGod] = 3;
                    break;
                case TraitType.Witch:
                    FaithTypeDictionary[FaithType.ChaosGoddess] = 3;
                    break;
                case TraitType.Poet:
                    FaithTypeDictionary[FaithType.GoddessOfPoetry] = 3;
                    break;
                case TraitType.Scheduled:
                    FaithTypeDictionary[FaithType.FatherOfTheGods] = 3;
                    break;
                case TraitType.Performer:
                    FaithTypeDictionary[FaithType.LoveGod] = 3;
                    break;
                case TraitType.Naturalist:
                    FaithTypeDictionary[FaithType.NatureGoddess] = 3;
                    break;
                case TraitType.Soldier:
                    FaithTypeDictionary[FaithType.WarGod] = 3;
                    break;
                case TraitType.Aesthetic:
                    FaithTypeDictionary[FaithType.BeautyGoddess] = 3;
                    break;
                case TraitType.Masochist:
                    FaithTypeDictionary[FaithType.FuryGoddess] = 3;
                    break;
                case TraitType.Fanatic:
                    FaithTypeDictionary[FaithType.EvilGod] = 3;
                    break;
                case TraitType.Noble:
                    FaithTypeDictionary[FaithType.WealthGod] = 3;
                    break;
                case TraitType.Farmer:
                    FaithTypeDictionary[FaithType.HouseholdGod] = 3;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        // Lookup faith type dictionary with a given trait type
        public int LookUpFaithTypeDictionaryWithTraitType(TraitType trait)
        {
            switch (trait)
            {
                case TraitType.Academic:
                    return FaithTypeDictionary[FaithType.GodOfLore];
                case TraitType.Apologist:
                    return FaithTypeDictionary[FaithType.TricksterGoddess];
                case TraitType.Spiritual:
                    return FaithTypeDictionary[FaithType.MotherOfTheGods];
                case TraitType.Collector:
                    return FaithTypeDictionary[FaithType.SeekerGod];
                case TraitType.Witch:
                    return FaithTypeDictionary[FaithType.ChaosGoddess];
                case TraitType.Poet:
                    return FaithTypeDictionary[FaithType.GoddessOfPoetry];
                case TraitType.Scheduled:
                    return FaithTypeDictionary[FaithType.FatherOfTheGods];
                case TraitType.Performer:
                    return FaithTypeDictionary[FaithType.LoveGod];
                case TraitType.Naturalist:
                    return FaithTypeDictionary[FaithType.NatureGoddess];
                case TraitType.Soldier:
                    return FaithTypeDictionary[FaithType.WarGod];
                case TraitType.Aesthetic:
                    return FaithTypeDictionary[FaithType.BeautyGoddess];
                case TraitType.Masochist:
                    return FaithTypeDictionary[FaithType.FuryGoddess];
                case TraitType.Fanatic:
                    return FaithTypeDictionary[FaithType.EvilGod];
                case TraitType.Noble:
                    return FaithTypeDictionary[FaithType.WealthGod];
                case TraitType.Farmer:
                    return FaithTypeDictionary[FaithType.HouseholdGod];
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        // Match trait type with corresponding faith type
        public FaithType MatchTraitTypeWithFaithType(TraitType trait)
        {
            switch (trait)
            {
                case TraitType.Academic:
                    return FaithType.GodOfLore;
                case TraitType.Apologist:
                    return FaithType.TricksterGoddess;
                case TraitType.Spiritual:
                    return FaithType.MotherOfTheGods;
                case TraitType.Collector:
                    return FaithType.SeekerGod;
                case TraitType.Witch:
                    return FaithType.ChaosGoddess;
                case TraitType.Poet:
                    return FaithType.GoddessOfPoetry;
                case TraitType.Scheduled:
                    return FaithType.FatherOfTheGods;
                case TraitType.Performer:
                    return FaithType.LoveGod;
                case TraitType.Naturalist:
                    return FaithType.NatureGoddess;
                case TraitType.Soldier:
                    return FaithType.WarGod;
                case TraitType.Aesthetic:
                    return FaithType.BeautyGoddess;
                case TraitType.Masochist:
                    return FaithType.FuryGoddess;
                case TraitType.Fanatic:
                    return FaithType.EvilGod;
                case TraitType.Noble:
                    return FaithType.WealthGod;
                case TraitType.Farmer:
                    return FaithType.HouseholdGod;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }
    
    public enum FaithType
    {
        PlayerGod,
        GodOfLore,
        GoddessOfPoetry,
        SeekerGod,
        FatherOfTheGods,
        MotherOfTheGods,
        TricksterGoddess,
        ChaosGoddess,
        BeautyGoddess,
        LoveGod,
        NatureGoddess,
        FuryGoddess,
        WarGod,
        EvilGod,
        WealthGod,
        HouseholdGod
    }

    public enum Faciton
    {
        Farmer,
        Smith,
        Noble
    }

    public class CitizenNeeds
    {
        public float sanityRatio;
        public float healthRatio;
        public float dutyRatio;
        
        public void CalculateNeeds(int sanity, int health, int factionDuty)
        {
            sanityRatio = sanity / 100f;
            healthRatio = health / 100f;
            dutyRatio = factionDuty / 100f;
        }
    }
}