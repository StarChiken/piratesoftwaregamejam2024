using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

/// <summary>
/// Handles procedural map/grid generation and district coloring.
/// </summary>
public class Map : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private List<GameObject> objectsControlList;
    
    [Header("Assignment")]
    [SerializeField] private GameObject[] buildings;
    
    [Header("Variation State")]
    [SerializeField] private PaddingVariationState paddingState;

    // Config properties
    private GameplayConfig GameplayConfig => ConfigManager.Instance.GetConfig<GameplayConfig>();
    private BuildingConfig BuildingConfig => ConfigManager.Instance.GetConfig<BuildingConfig>();

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
        buildings = new [] { BuildingConfig.Building1x1, BuildingConfig.Building2x1, BuildingConfig.BuildingL };
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
        int districtsPerRow = Mathf.CeilToInt(Mathf.Sqrt(GameplayConfig.NumberOfDistricts));
        for (int i = 0; i < GameplayConfig.NumberOfDistricts; i++)
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
        for (int x = 0; x < GameplayConfig.GridX; x++)
        {
            for (int z = 0; z < GameplayConfig.GridZ; z++)
            {
                float xPos = (x + col * GameplayConfig.GridX) * GameplayConfig.GridSpacing;
                float yPos = 0;
                float zPos = (z + row * GameplayConfig.GridZ) * GameplayConfig.GridSpacing;
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
                return Mathf.Sin(x * GameplayConfig.SinusoidalAmplitude) * GameplayConfig.GridSpacing;
            case PaddingVariationState.Random:
                return Random.Range(GameplayConfig.RandomMin, GameplayConfig.RandomMax);
            case PaddingVariationState.ExponentialDecay:
                return Mathf.Exp(-x * GameplayConfig.ExponentialDecayRate);
            case PaddingVariationState.StepFunction:
                return (x % GameplayConfig.StepInterval == 0 ? GameplayConfig.StepFunctionValue : 0f);
            case PaddingVariationState.TriangleWave:
                return Mathf.PingPong(x * GameplayConfig.TriangleWaveFrequency, GameplayConfig.TriangleWaveAmplitude);
            default:
                return 0f;
        }
    }
}
