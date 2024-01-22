using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Resources = UnityEngine.Resources;
using Base.Core.Managers;

public class GenerationTest : MonoBehaviour
{
    [Header("Grid Settings")]
    public int gridX;
    public int gridZ;
    public int gridSpacing;

    [Header("Building Objects")]
    public GameObject edoHouse;

    public GameObject building1x1;
    public GameObject building2x2;
    public GameObject building2x1;
    public GameObject buildingL;

    private GameObject[] buildings = new GameObject[4];

    private Dictionary<Vector2, Building> buildingGrid = new();

    public List<Vector2> buildingPositions;
    public List<Building> buildingThings;

    void Start()
    {
        buildings[0] = building1x1;
        buildings[1] = building2x1;
        buildings[2] = building2x2;
        buildings[3] = buildingL;

        GenerateGrid();

        foreach (Building building in buildingGrid.Values)
        {
            if (building.buildingSize == BuildingSize.TwoByOne)
            {
                print($"Edo House at {building.gridPositions[0]}");
                Instantiate(Resources.Load("EdoHouse"), building.gridPositions[0], Quaternion.identity);
            }
        }
    }

    private void GenerateGrid()
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridZ; y++)
            {
                Vector2 position = new Vector2(x, y);

                int randomBuildingIndex = Random.Range(0, 4);

                GameObject buildingObject = Instantiate(buildings[randomBuildingIndex], position, Quaternion.Euler(0 ,0 , Random.Range(1, 4) * 90));

                buildingObject.name = $"Grid {x} {y}";

                Vector2[] buildingChildrenPositions = new Vector2[buildingObject.transform.childCount];
                
                for (int i = 0; i < buildingObject.transform.childCount; i++)
                {
                    buildingChildrenPositions[i] = buildingObject.transform.GetChild(i).position;
                }

                // Shy
                Building building = new Building($"Grid {x} {y}", buildingChildrenPositions, (BuildingSize)randomBuildingIndex, buildingObject, BuildingType.Default);
                // Building building = new Building($"Grid {x} {y}", buildingChildrenPositions, (BuildingSize)randomBuildingIndex, buildingObject);

                for (int i = 0; i < buildingObject.transform.childCount; i++)
                {
                    buildingGrid.Add(buildingChildrenPositions[i], building);
                }

                y += gridSpacing;
            }
            x += gridSpacing;
        }
    }

    [Serializable]
    public class Building
    {
        // Etho
        public string name;
        public Vector2[] gridPositions;
        public BuildingSize buildingSize;
        public GameObject buildingObject;
        public Mesh buildingMesh;

        // Shy
        public BuildingType buildingType;
        public ActionOptions buildingActionOptions;
        public List<Citizen> tempListForDoAction = new();
        
        public Building(string _name, Vector2[] _gridPositions, BuildingSize _buildingSize, GameObject _buildingObject, BuildingType buildingType)
        {
            name = _name;
            buildingSize = _buildingSize;
            gridPositions = _gridPositions;
            buildingObject = _buildingObject;
            
            // Shy
            this.buildingType = buildingType;
        }
        // Shy
        public void DoBuildingAction()
        {
            switch (buildingActionOptions)
            {
                case ActionOptions.DoAction:

                    switch (buildingType)
                    {
                        // Get Happiness for Followers
                        case BuildingType.Entertainment:
                            foreach (var citizen in tempListForDoAction)
                            {
                                citizen.Happiness += 1;
                            }
                            break;
                        // Get a bit of Resources
                        case BuildingType.Business:
                            GameManager.Instance.Player.Resources.AddResources(5);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    
                    break;
                case ActionOptions.AskFavorFromFaction:
                    break;
                case ActionOptions.ReplaceWithTemple:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum BuildingSize
    {
        OneByOne,
        TwoByOne,
        TwoByTwo,
        LTwoByTwo
    }
    
    public enum BuildingType
    {
        Default,
        Entertainment,
        Business
    }
    
    public enum ActionOptions
    {
        DoAction,
        AskFavorFromFaction,
        ReplaceWithTemple
    }
}