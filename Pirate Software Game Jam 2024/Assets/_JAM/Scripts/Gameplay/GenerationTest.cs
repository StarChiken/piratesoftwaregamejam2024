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
        public Material houseMaterial;
        public Material factionDutyMaterial;
        public Material sanityMaterial;
        public Material healthMaterial;

        [Header("Prefab Assignemnt")]
        public GameObject building1x1;
        public GameObject building2x2;
        public GameObject building2x1;
        public GameObject buildingL;
        public GameObject citizenPrefab;

        private bool canSpawnTemple = true;

        private GameObject[] buildings = new GameObject[3];

        private Material[] buildingMaterials = new Material[4];

        public Dictionary<Vector2, Building> buildingGrid = new();

        private PathfindingTest pathfindingScript;

        void Start()
        {
            pathfindingScript = GetComponent<PathfindingTest>();

            buildings[0] = building1x1;
            buildings[1] = building2x1;
            buildings[2] = buildingL;

            buildingMaterials[0] = houseMaterial;
            buildingMaterials[1] = factionDutyMaterial;
            buildingMaterials[2] = sanityMaterial;
            buildingMaterials[3] = healthMaterial;

            GenerateGrid(Random.Range(minStartingHouses, maxStartingHouses + 1));
        }

        private void Update()
        {
            //Testing Temple Choosing
            /*
            if (canSpawnTemple && Mouse.current.leftButton.wasPressedThisFrame)
            {
                Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.value);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 300))
                {
                    print(hit.point);
                    Collider[] clickColliders = Physics.OverlapSphere(hit.point, 1);
                    foreach (Collider collider in clickColliders)
                    {
                        Building clickedBuilding = null;
                        buildingGrid.TryGetValue(new Vector2(collider.transform.position.x, collider.transform.position.z), out clickedBuilding);
                        if (clickedBuilding != null && clickedBuilding.buildingType != BuildingType.House)
                        {
                            //Remove old building from dictionary
                            Transform clickedParentTransform = collider.transform.parent;

                            for (int i = 0; i < clickedParentTransform.childCount; i++)
                            {
                                if (clickedParentTransform.GetChild(i).tag == "Grid Tile")
                                {
                                    buildingGrid.Remove(new Vector2(clickedParentTransform.GetChild(i).position.x, clickedParentTransform.GetChild(i).position.z));
                                }
                            }

                            //Create Temple building object
                            GameObject buildingObject = Instantiate(building2x2, collider.transform.parent.position, Quaternion.Euler(90, 0, 0));
                            buildingObject.name = "Temple";

                            //These are the correct positions for a 2x2 building (temple)
                            Vector2[] buildingPositions = new Vector2[4];
                            buildingPositions[0] = new Vector2(collider.transform.parent.position.x - 0.5f, collider.transform.parent.position.z - 0.5f);
                            buildingPositions[1] = new Vector2(collider.transform.parent.position.x - 0.5f, collider.transform.parent.position.z + 0.5f);
                            buildingPositions[2] = new Vector2(collider.transform.parent.position.x + 0.5f, collider.transform.parent.position.z + 0.5f);
                            buildingPositions[3] = new Vector2(collider.transform.parent.position.x + 0.5f, collider.transform.parent.position.z - 0.5f);

                            Building templeBuilding = new Building("Temple", buildingPositions, BuildingSize.TwoByTwo, buildingObject, BuildingType.Temple);

                            for (int i = 0; i < 4; i++)
                            {
                                buildingGrid.Add(buildingPositions[i], templeBuilding);
                            }

                            Destroy(clickedBuilding.buildingObject);

                            canSpawnTemple = false;
                            break;
                        }
                    }
                }
            }*/
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

                    int buildingTypeIndex;

                    if (buildingsSpawned < houses)
                    {
                        buildingTypeIndex = 0;
                    }
                    else
                    {
                        buildingTypeIndex = Random.Range(1, 4);
                    }

                    buildingObject.GetComponent<BuildingObject>().SetRoofMaterial(buildingMaterials[buildingTypeIndex]);

                    Transform[] childObjects = buildingObject.GetComponentsInChildren<Transform>();

                    List<Vector2> buildingChildrenPositions = new();

                    for (int i = 0; i < childObjects.Length; i++)
                    {
                        if (childObjects[i].tag == "Grid Tile")
                        {
                            Vector3 buildingPosition = childObjects[i].transform.position;
                            buildingChildrenPositions.Add(new Vector2(buildingPosition.x, buildingPosition.z));
                        }
                    }

                    Building building = new Building($"Grid {x} {z} {(BuildingType)buildingTypeIndex}", buildingChildrenPositions.ToArray(), (BuildingSize)randomBuildingIndex, buildingObject, (BuildingType)buildingTypeIndex);

                    for (int i = 0; i < childObjects.Length; i++)
                    {
                        if (childObjects[i].tag == "Grid Tile")
                        {
                            Vector3 buildingPosition = childObjects[i].transform.position;
                            buildingGrid.Add(new Vector2(buildingPosition.x, buildingPosition.z), building);

                            if (buildingsSpawned < houses)
                            {
                                CitizenAgent citizenAgent = Instantiate(citizenPrefab, buildingPosition, Quaternion.identity).GetComponent<CitizenAgent>();
                                citizenAgent.generationTestScript = this;
                                citizenAgent.pathfindingTestScript = pathfindingScript;
                                citizenAgent.citizen = new Citizen();
                                citizenAgent.citizen.InitializeCitizen();
                                citizenAgent.citizen.housePosition = new Vector2(buildingPosition.x, buildingPosition.z);
                            }
                        }
                    }

                    buildingObject.name = $"Grid {x} {z} {(BuildingType)buildingTypeIndex}";

                    z += gridSpacing;
                    buildingsSpawned++;
                }

                x += gridSpacing;
            }
        }

        public Building GetRandomBuildingByType(BuildingType buildingType)
        {
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
