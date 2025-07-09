using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Base.Gameplay
{
    /// <summary>
    /// Handles procedural map/grid generation and district coloring.
    /// </summary>
    public class Map : MonoBehaviour
    {
        [Header("Grid Settings")]
        [SerializeField] private int numberOfDistricts = 9;
        [SerializeField] private int gridX = 5;
        [SerializeField] private int gridZ = 5;
        [SerializeField] private int gridSpacing = 5;
        [SerializeField] private int gridPadding = 5;
        [SerializeField] private List<GameObject> objectsControlList;
        
        [Header("Assignment")]
        [SerializeField] private GameObject building1x1;
        [SerializeField] private GameObject building2x2;
        [SerializeField] private GameObject building2x1;
        [SerializeField] private GameObject buildingL;
        [SerializeField] private GameObject[] buildings;
        
        [Header("Variation State")]
        [SerializeField] private PaddingVariationState paddingState;

        [Header("Sinusoidal Variation")]
        [SerializeField] private float sinusoidalAmplitude = 2f;

        [Header("Random Variation")]
        [SerializeField] private float randomMin = -1f;
        [SerializeField] private float randomMax = 1f;

        [Header("Exponential Decay Variation")]
        [SerializeField] private float exponentialDecayRate = 0.1f;

        [Header("Step Function Variation")]
        [SerializeField] private int stepInterval = 3;
        [SerializeField] private float stepFunctionValue = 2f;

        [Header("Triangle Wave Variation")]
        [SerializeField] private float triangleWaveFrequency = 0.1f;
        [SerializeField] private float triangleWaveAmplitude = 2f;

        /// <summary>
        /// Enumeration of different variations on padding amount.
        /// </summary>
        public enum PaddingVariationState
        {
            Sinusoidal,
            Random,
            ExponentialDecay,
            StepFunction,
            TriangleWave
        }
        
        private void Start()
        {
            buildings = new [] { building1x1, building2x1, buildingL };
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DoMapTest();
            }
        }

        /// <summary>
        /// Initiates city generation.
        /// </summary>
        public void DoMapTest()
        {
            ChangePaddingVariation(paddingState);
            DestroyExistingObjectsAndClearList();
            GenerateGridForEachDistrict();
        }

        private void GenerateGridForEachDistrict()
        {
            int districtsPerRow = Mathf.CeilToInt(Mathf.Sqrt(numberOfDistricts));
            for (int i = 0; i < numberOfDistricts; i++)
            {
                int row = i / districtsPerRow;
                int col = i % districtsPerRow;
                List<GameObject> tempControlList = new List<GameObject>();
                GenerateGrid(row, col, tempControlList);
                foreach (var obj in tempControlList)
                {
                    objectsControlList.Add(obj);
                    List<int> randomList = new() { 90, 180, 270 };
                    int randomYIndex = Random.Range(0, randomList.Count);
                    int randomY = randomList[randomYIndex];
                    obj.gameObject.GetComponent<Transform>().eulerAngles = new Vector3(0, randomY, 0);
                    Color districtColor = GetDistrictColor(i);
                    //obj.gameObject.GetComponent<Renderer>().material.color = districtColor; 
                }
            }
        }

        private void DestroyExistingObjectsAndClearList()
        {
            foreach (var gameObject in objectsControlList)
            {
                Destroy(gameObject);
            }
            objectsControlList.Clear();
        }

        private void GenerateGrid(int row, int col, List<GameObject> list)
        {
            for (int x = 0; x < gridX; x++)
            {
                for (int z = 0; z < gridZ; z++)
                {
                    float xPos = (x + col * gridX) * gridSpacing;
                    float yPos = 0;
                    float zPos = (z + row * gridZ) * gridSpacing;
                    Vector3 position = new Vector3(xPos, yPos, zPos);
                    var randomIndex = Random.Range(0, buildings.Length);
                    var building = Instantiate(Resources.Load(buildings[randomIndex].name), position, Quaternion.identity) as GameObject;
                    building.name = $"Grid {xPos} {zPos}";
                    list.Add(building);
                }
            }
        }

        /// <summary>
        /// Gets the color for a specific district index.
        /// </summary>
        private Color GetDistrictColor(int districtIndex)
        {
            switch (districtIndex)
            {
                case 0: return new Color(0.8f, 0.0f, 0.0f, 1.0f); // Dark Red
                case 1: return new Color(0.5f, 0.0f, 0.5f, 1.0f); // Purple
                case 2: return new Color(0.0f, 0.5f, 0.0f, 1.0f); // Dark Green
                case 3: return new Color(1.0f, 1.0f, 0.0f, 1.0f); // Pure Yellow
                case 4: return new Color(0.0f, 0.8f, 1.0f, 1.0f); // Light Blue
                case 5: return new Color(0.8f, 0.0f, 0.5f, 1.0f); // Rose
                case 6: return new Color(0.7f, 0.7f, 0.7f, 1.0f); // Silver
                case 7: return new Color(1.0f, 0.5f, 0.0f, 1.0f); // Orange
                case 8: return new Color(0.0f, 0.0f, 0.0f, 1.0f); // Black
                case 9: return new Color(1.0f, 0.8f, 0.0f, 1.0f); // Gold
                default:
                    Debug.LogError("Invalid district index");
                    return Color.white;
            }
        }

        private void ChangePaddingVariation(PaddingVariationState newState)
        {
            paddingState = newState;
        }

        /// <summary>
        /// Calculates padding based on the chosen variation state.
        /// </summary>
        private float CalculatePadding(int x)
        {
            switch (paddingState)
            {
                case PaddingVariationState.Sinusoidal:
                    return Mathf.Sin(x * sinusoidalAmplitude) * gridSpacing;
                case PaddingVariationState.Random:
                    return Random.Range(randomMin, randomMax);
                case PaddingVariationState.ExponentialDecay:
                    return Mathf.Exp(-x * exponentialDecayRate);
                case PaddingVariationState.StepFunction:
                    return (x % stepInterval == 0 ? stepFunctionValue : 0f);
                case PaddingVariationState.TriangleWave:
                    return Mathf.PingPong(x * triangleWaveFrequency, triangleWaveAmplitude);
                default:
                    return 0f;
            }
        }
    }
}
