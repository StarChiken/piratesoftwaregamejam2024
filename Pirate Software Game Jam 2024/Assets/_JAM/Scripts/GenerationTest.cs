using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;
using Resources = UnityEngine.Resources;
using Base.Core.Managers;

public class GenerationTest : MonoBehaviour
{
    [Header("Grid Settings")]
    public int gridX;
    public int gridZ;
    public int gridSpacing;

    public int minStartingHouses;
    public int maxStartingHouses;
    
    [Header("Building Color By Type")]
    public Color houseColor;
    public Color templeColor;
    public Color factionDutyColor;
    public Color sanityColor;
    public Color healthColor;

    [Header("Assignemnt")]
    public GameObject building1x1;
    public GameObject building2x2;
    public GameObject building2x1;
    public GameObject buildingL;

    private GameObject[] buildings = new GameObject[4];

    private Color[] buildingColors = new Color[4];

    public Dictionary<Vector2, Building> buildingGrid = new();

    public List<Vector2> buildingPositions;
    public List<Building> buildingThings;

    void Start()
    {
        buildings[0] = building1x1;
        buildings[1] = building2x1;
        buildings[2] = building2x2;
        buildings[3] = buildingL;

        buildingColors[0] = houseColor;
        buildingColors[1] = factionDutyColor;
        buildingColors[2] = sanityColor;
        buildingColors[3] = healthColor;

        GenerateGrid(Random.Range(minStartingHouses, maxStartingHouses + 1));
    }

    private void Update()
    {
        //Testing Temple Choosing
        if (Mouse.current.leftButton.IsActuated())
        {
            Collider2D col = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Mouse.current.position.value), 0.25f);

            if (col != null)
            {
                Building clickedBuilding = buildingGrid[col.transform.position];
                
                if (clickedBuilding.buildingSize == BuildingSize.TwoByTwo)
                {
                    clickedBuilding.TurnBuildingIntoTemple(templeColor);
                }
            }
        }
    }

    private void GenerateGrid(int houses)
    {
        int buildingsSpawned = 0;
        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridZ; y++)
            {
                Vector2 position = new Vector2(x + 0.5f, y + 0.5f);
                
                //Randomly picks a building prefab to spawn from the buildings array
                int randomBuildingIndex = Random.Range(0, 4);
                GameObject buildingObject = Instantiate(buildings[randomBuildingIndex], position, Quaternion.Euler(0 ,0 , Random.Range(1, 4) * 90));

                buildingObject.name = $"Grid {x} {y}";

                Vector2[] buildingChildrenPositions = new Vector2[buildingObject.transform.childCount];
                
                for (int i = 0; i < buildingObject.transform.childCount; i++)
                {
                    buildingChildrenPositions[i] = buildingObject.transform.GetChild(i).position;
                }

                int buildingColorIndex;

                if (buildingsSpawned < houses)
                {
                    buildingColorIndex = 0;
                }
                else
                {
                    buildingColorIndex = Random.Range(1, 4);
                }

                Building building = new Building($"Grid {x} {y}", buildingChildrenPositions, (BuildingSize)randomBuildingIndex, buildingObject, (BuildingType)buildingColorIndex);

                SpriteRenderer[] spriteRenderers = building.buildingObject.GetComponentsInChildren<SpriteRenderer>();

                for (int i = 0; i < buildingObject.transform.childCount; i++)
                {
                    buildingGrid.Add(buildingChildrenPositions[i], building);
                    spriteRenderers[i].color = buildingColors[buildingColorIndex];
                }

                y += gridSpacing;
                buildingsSpawned++;
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
                        case BuildingType.Sanity:
                            foreach (var citizen in tempListForDoAction)
                            {
                                citizen.Sanity += 1;
                            }
                            break;
                        // Get a bit of Resources
                        case BuildingType.Temple:
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

        public void TurnBuildingIntoTemple(Color templeColor)
        {
            buildingType = BuildingType.Temple;


            for (int i = 0; i < buildingObject.transform.childCount; i++)
            {
                SpriteRenderer[] spriteRenderers = buildingObject.GetComponentsInChildren<SpriteRenderer>();
                spriteRenderers[i].color = templeColor;
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
        House,
        Temple,
        Faction,
        Sanity,
        Health
    }
    
    public enum ActionOptions
    {
        DoAction,
        AskFavorFromFaction,
        ReplaceWithTemple
    }
}