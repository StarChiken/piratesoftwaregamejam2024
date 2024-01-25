using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Base.Gameplay
{


    public class Map : MonoBehaviour
    {
        [Header("Grid Settings")] public int numberOfDistricts = 9;
        public int gridX = 5;
        public int gridZ = 5;
        public int gridSpacing = 5;
        public int gridPadding = 5;
        public List<GameObject> objectsControlList;
        
        [Header("Assignemnt")]
        public GameObject building1x1;
        public GameObject building2x2;
        public GameObject building2x1;
        public GameObject buildingL;
        public GameObject[] buildings;
        
        [Header("Variation State")] public PaddingVariationState paddingState;

        [Header("Sinusoidal Variation")] public float sinusoidalAmplitude = 2f;

        [Header("Random Variation")] public float randomMin = -1f;
        public float randomMax = 1f;

        [Header("Exponential Decay Variation")]
        public float exponentialDecayRate = 0.1f;

        [Header("Step Function Variation")] public int stepInterval = 3;
        public float stepFunctionValue = 2f;

        [Header("Triangle Wave Variation")] public float triangleWaveFrequency = 0.1f;
        public float triangleWaveAmplitude = 2f;

        // Enumeration of different variations on padding amount
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

        // Method to initiate the city generation
        public void DoMapTest()
        {
            ChangePaddingVariation(paddingState);

            DestroyExistingObjectsAndClearList();

            GenerateGridForEachDistrict();
        }

        private void GenerateGridForEachDistrict()
        {
            int districtsPerRow =
                Mathf.CeilToInt(Mathf.Sqrt(numberOfDistricts)); // Calculate districts per row to distribute evenly

            for (int i = 0; i < numberOfDistricts; i++)
            {
                int row = i / districtsPerRow;
                int col = i % districtsPerRow;

                List<GameObject> tempControlList = new List<GameObject>(); // Create a new list for each district

                GenerateGrid(row, col, tempControlList); // Generate grid based on specified row, col, and list

                // Assign color to each building in the district and add it to the control list
                foreach (var obj in tempControlList)
                {
                    objectsControlList.Add(obj);

                    List<int> randomList = new() { 90, 180, 270 };
                    int randomYIndex = Random.Range(0, randomList.Count);
                    int randomY = randomList[randomYIndex];

                    obj.gameObject.GetComponent<Transform>().eulerAngles = new Vector3(0, randomY, 0); // adjust angle

                    // Assign color only works on 1-material objects with a Renderer

                    Color districtColor = GetDistrictColor(i); // Get color based on district index
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
                    // float padding =
                    //     CalculatePadding(gridPadding); // Call the method to get padding based on the chosen variation
                    //
                    // float xPos = (x + col * gridX) * (gridSpacing + padding);
                    // float yPos = 0.5f;
                    // float zPos = (z + row * gridZ) * (gridSpacing + padding);

                    // var padding = CalculatePadding(gridSpacing);

                    float xPos = (x + col * gridX) * gridSpacing;
                    float yPos = 0;
                    float zPos = (z + row * gridZ) * gridSpacing;
                    
                    Vector3 position = new Vector3(xPos, yPos, zPos);
                    var randomIndex = Random.Range(0, buildings.Length);
                    
                    var building =
                        Instantiate(Resources.Load(buildings[randomIndex].name), position, Quaternion.identity) as GameObject;

                    building.name = $"Grid {xPos} {zPos}";

                    list.Add(building);
                }
            }
        }

        // Method to get the color for a specific district index
        private Color GetDistrictColor(int districtIndex)
        {
            switch (districtIndex)
            {
                case 0:
                    return new Color(0.8f, 0.0f, 0.0f, 1.0f); // Dark Red
                case 1:
                    return new Color(0.5f, 0.0f, 0.5f, 1.0f); // Purple
                case 2:
                    return new Color(0.0f, 0.5f, 0.0f, 1.0f); // Dark Green
                case 3:
                    return new Color(1.0f, 1.0f, 0.0f, 1.0f); // Pure Yellow
                case 4:
                    return new Color(0.0f, 0.8f, 1.0f, 1.0f); // Light Blue
                case 5:
                    return new Color(0.8f, 0.0f, 0.5f, 1.0f); // Rose
                case 6:
                    return new Color(0.7f, 0.7f, 0.7f, 1.0f); // Silver
                case 7:
                    return new Color(1.0f, 0.5f, 0.0f, 1.0f); // Orange
                case 8:
                    return new Color(0.0f, 0.0f, 0.0f, 1.0f); // Black
                case 9:
                    return new Color(1.0f, 0.8f, 0.0f, 1.0f); // Gold
                default:
                    Debug.LogError("Invalid district index");
                    return Color.white; // Default color if the district index is invalid
            }
        }

        // Method to change the variation state
        private void ChangePaddingVariation(PaddingVariationState newState)
        {
            paddingState = newState;
        }

        // Method to calculate padding based on the chosen variation state
        float CalculatePadding(int x)
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
