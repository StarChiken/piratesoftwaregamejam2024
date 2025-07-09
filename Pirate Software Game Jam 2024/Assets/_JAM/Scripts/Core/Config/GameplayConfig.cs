using System;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Core.Config
{
    /// <summary>
    /// Configuration data for gameplay mechanics and settings.
    /// </summary>
    [CreateAssetMenu(fileName = "GameplayConfig", menuName = "Game/Config/Gameplay Config")]
    public class GameplayConfig : BaseConfig
    {
        [Header("Grid Settings")]
        [SerializeField] private int numberOfDistricts = 9;
        [SerializeField] public int gridX = 5;
        [SerializeField] public int gridZ = 5;
        [SerializeField] private int gridSpacing = 5;
        [SerializeField] private int gridPadding = 5;
        
        [Header("Building Generation")]
        [SerializeField] private int minStartingHouses = 16;
        [SerializeField] private int maxStartingHouses = 24;
        
        [Header("Variation Settings")]
        [SerializeField] private float sinusoidalAmplitude = 2f;
        [SerializeField] private float randomMin = -1f;
        [SerializeField] private float randomMax = 1f;
        [SerializeField] private float exponentialDecayRate = 0.1f;
        [SerializeField] private int stepInterval = 3;
        [SerializeField] private float stepFunctionValue = 2f;
        [SerializeField] private float triangleWaveFrequency = 0.1f;
        [SerializeField] private float triangleWaveAmplitude = 2f;
        
        [Header("Citizen Agent Settings")]
        [SerializeField] private int buildingChecksPerMove = 3;
        [SerializeField] private float checkBuildingTime = 1f;
        [SerializeField] private float moveTime = 1f;
        [SerializeField] private int dutySanityDrainPerSecond = 3;
        [SerializeField] private int naturalHealthDrainPerSecond = 2;
        [SerializeField] private int naturalSanityDrainPerSecond = 2;
        [SerializeField] private int naturalDutyDrainPerSecond = 2;
        
        [Header("Devotion Milestones")]
        [SerializeField] private List<int> devotionMilestones = new() { 12, 48, 192, 768 };

        // Public properties for backward compatibility
        public int NumberOfDistricts => numberOfDistricts;
        public int GridX => gridX;
        public int GridZ => gridZ;
        public int GridSpacing => gridSpacing;
        public int GridPadding => gridPadding;
        public int MinStartingHouses => minStartingHouses;
        public int MaxStartingHouses => maxStartingHouses;
        public float SinusoidalAmplitude => sinusoidalAmplitude;
        public float RandomMin => randomMin;
        public float RandomMax => randomMax;
        public float ExponentialDecayRate => exponentialDecayRate;
        public int StepInterval => stepInterval;
        public float StepFunctionValue => stepFunctionValue;
        public float TriangleWaveFrequency => triangleWaveFrequency;
        public float TriangleWaveAmplitude => triangleWaveAmplitude;
        public int BuildingChecksPerMove => buildingChecksPerMove;
        public float CheckBuildingTime => checkBuildingTime;
        public float MoveTime => moveTime;
        public int DutySanityDrainPerSecond => dutySanityDrainPerSecond;
        public int NaturalHealthDrainPerSecond => naturalHealthDrainPerSecond;
        public int NaturalSanityDrainPerSecond => naturalSanityDrainPerSecond;
        public int NaturalDutyDrainPerSecond => naturalDutyDrainPerSecond;
        public List<int> DevotionMilestones => devotionMilestones;

        protected override string ConfigFileName => "GameplayConfig";
    }
} 