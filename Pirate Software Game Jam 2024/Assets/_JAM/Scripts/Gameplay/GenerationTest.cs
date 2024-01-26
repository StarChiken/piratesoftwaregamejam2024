using System;
using System.Collections.Generic;
using Base.Core.Components;
using UnityEngine;
using Random = UnityEngine.Random;
using Base.Core.Managers;
using UnityEngine.InputSystem;

namespace Base.Gameplay
{
    public class GenerationTest : MyMonoBehaviour
    {
        [Header("Grid Settings")] public int gridX;
        public int gridZ;
        public int gridSpacing;

        public int minStartingHouses;
        public int maxStartingHouses;

        [Header("Building Color By Type")] public Color houseColor;
        public Color templeColor;
        public Color factionDutyColor;
        public Color sanityColor;
        public Color healthColor;

        [Header("Assignemnt")] public GameObject building1x1;
        public GameObject building2x2;
        public GameObject building2x1;
        public GameObject buildingL;

        private bool canSpawnTemple = true;

        private GameObject[] buildings = new GameObject[3];

        private Color[] buildingColors = new Color[4];

        public Dictionary<Vector2, Building> buildingGrid = new();

        public List<Vector2> buildingPositions;
        public List<Building> buildingThings;

        void Start()
        {
            buildings[0] = building1x1;
            buildings[1] = building2x1;
            buildings[2] = buildingL;

            buildingColors[0] = houseColor;
            buildingColors[1] = factionDutyColor;
            buildingColors[2] = sanityColor;
            buildingColors[3] = healthColor;

            GenerateGrid(Random.Range(minStartingHouses, maxStartingHouses + 1));
        }

        private void Update()
        {
            //Testing Temple Choosing
            if (canSpawnTemple && Mouse.current.leftButton.wasPressedThisFrame)
            {
                Collider2D col = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Mouse.current.position.value), 0.25f);

                if (col != null)
                {
                    //REMOVE OLD BUILDING
                    Building clickedBuilding = buildingGrid[col.transform.position];

                    Transform clickedParentTransform = col.transform.parent;

                    for (int i = 0; i < clickedParentTransform.childCount; i++)
                    {
                        buildingGrid.Remove(clickedParentTransform.GetChild(i).position);
                    }

                    GameObject buildingObject = Instantiate(building2x2, col.transform.parent.position, Quaternion.Euler(0, 0, Random.Range(1, 4) * 90));
                    buildingObject.name = "Temple";

                    //These are the correct positions for a 2x2 building (temple)
                    Vector2[] buildingPositions = new Vector2[4];
                    buildingPositions[0] = (Vector2)col.transform.position + new Vector2(-0.5f, -0.5f);
                    buildingPositions[1] = (Vector2)col.transform.position + new Vector2(-0.5f, 0.5f);
                    buildingPositions[2] = (Vector2)col.transform.position + new Vector2(0.5f, 0.5f);
                    buildingPositions[3] = (Vector2)col.transform.position + new Vector2(0.5f, -0.5f);

                    Building templeBuilding = new Building("Temple", buildingPositions, BuildingSize.TwoByTwo, buildingObject, BuildingType.Temple);

                    //Gets all sprite renders to change the color to the temple color
                    SpriteRenderer[] spriteRenderers = templeBuilding.buildingObject.GetComponentsInChildren<SpriteRenderer>();

                    for (int i = 0; i < buildingObject.transform.childCount; i++)
                    {
                        buildingGrid.Add(buildingPositions[i], templeBuilding);
                        spriteRenderers[i].color = templeColor;
                    }

                    Destroy(clickedBuilding.buildingObject);

                    canSpawnTemple = false;
                }
            }
        }

        private void GenerateGrid(int houses)
        {
            int buildingsSpawned = 0;
            for (int x = 0; x < gridX; x++)
            {
                for (int z = 0; z < gridZ; z++)
                {
                    Vector3 position = new Vector3(x + 0.5f, 0, z + 0.5f);

                    //Randomly picks a building prefab to spawn from the buildings array
                    int randomBuildingIndex = Random.Range(0, 3);
                    GameObject buildingObject = Instantiate(buildings[randomBuildingIndex], position, Quaternion.Euler(90, Random.Range(1, 4) * 90, 0));

                    int buildingColorIndex;

                    if (buildingsSpawned < houses)
                    {
                        buildingColorIndex = 0;
                    }
                    else
                    {
                        buildingColorIndex = Random.Range(1, 4);
                    }

                    SpriteRenderer[] spriteRenderers = buildingObject.GetComponentsInChildren<SpriteRenderer>();
                    print(spriteRenderers.Length);

                    Vector2[] buildingChildrenPositions = new Vector2[spriteRenderers.Length];

                    for (int i = 0; i < spriteRenderers.Length; i++)
                    {
                        Vector3 buildingPosition = spriteRenderers[i].transform.position;
                        buildingChildrenPositions[i] = new Vector2(buildingPosition.x, buildingPosition.z);
                        spriteRenderers[i].color = buildingColors[buildingColorIndex];
                    }

                    Building building = new Building($"Grid {x} {z} {(BuildingType)buildingColorIndex}", buildingChildrenPositions, (BuildingSize)randomBuildingIndex, buildingObject, (BuildingType)buildingColorIndex);

                    for (int i = 0; i < spriteRenderers.Length; i++)
                    {
                        print(spriteRenderers[i].transform.name);
                        Vector3 buildingPosition = spriteRenderers[i].transform.position;
                        buildingGrid.Add(new Vector2(buildingPosition.x, buildingPosition.z), building);
                    }

                    buildingObject.name = $"Grid {x} {z} {(BuildingType)buildingColorIndex}";

                    z += gridSpacing;
                    buildingsSpawned++;
                }

                x += gridSpacing;
            }
        }

        public Building GetRandomBuildingByType(BuildingType buildingType)
        {
            print("Got called");
            List<Vector2> buildingPositions = new();
            foreach (Vector2 gridPos in buildingGrid.Keys)
            {
                if (buildingGrid[gridPos].buildingType == buildingType)
                {
                    buildingPositions.Add(gridPos);
                }
            }
            return buildingGrid[buildingPositions[Random.Range(0, buildingPositions.Count)]];
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

        public Building(string _name, Vector2[] _gridPositions, BuildingSize _buildingSize,
            GameObject _buildingObject, BuildingType buildingType)
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
                        // Get a bit of Devotion Points ??? Shy
                        case BuildingType.Temple:
                            //GameManager.Player.Devotion.ChangeDevotionAmount(5); 
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
        LTwoByTwo,
        TwoByTwo
    }

    public enum BuildingType
    {
        House,
        Faction,
        Sanity,
        Health,
        Temple
    }

    public enum ActionOptions
    {
        DoAction,
        AskFavorFromFaction,
        ReplaceWithTemple
    }
}
