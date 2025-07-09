using System;
using System.Collections.Generic;
using Base.Core.Components;
using UnityEngine;
using Random = UnityEngine.Random;
using Base.Core.Managers;
using UnityEngine.InputSystem;
using Base.Gameplay;

namespace Base.Gameplay
{
    /// <summary>
    /// Handles procedural generation of buildings and citizens for the city grid.
    /// </summary>
    public class GenerationTest : MyMonoBehaviour
    {
        [Header("Grid Settings")]
        /// <summary>Number of grid cells in the X direction.</summary>
        public int gridX;
        /// <summary>Number of grid cells in the Z direction.</summary>
        public int gridZ;
        /// <summary>Spacing between grid cells.</summary>
        public int gridSpacing;

        /// <summary>Minimum number of starting houses.</summary>
        public int minStartingHouses;
        /// <summary>Maximum number of starting houses.</summary>
        public int maxStartingHouses;

        [Header("Building Color By Type")]
        /// <summary>Color for house buildings.</summary>
        public Color houseColor;
        /// <summary>Material for house buildings.</summary>
        public Material houseMaterial;
        /// <summary>Material for faction duty buildings.</summary>
        public Material factionDutyMaterial;
        /// <summary>Material for sanity buildings.</summary>
        public Material sanityMaterial;
        /// <summary>Material for health buildings.</summary>
        public Material healthMaterial;

        [Header("Prefab Assignment")]
        /// <summary>Prefab for 1x1 building.</summary>
        public GameObject building1x1;
        /// <summary>Prefab for 2x2 building.</summary>
        public GameObject building2x2;
        /// <summary>Prefab for 2x1 building.</summary>
        public GameObject building2x1;
        /// <summary>Prefab for L-shaped building.</summary>
        public GameObject buildingL;
        /// <summary>Prefab for citizen agent.</summary>
        public GameObject citizenPrefab;

        private bool canSpawnTemple = true;
        private GameObject[] buildings = new GameObject[3];
        private Material[] buildingMaterials = new Material[4];
        private GridManager gridManager;
        private CitizenSpawner citizenSpawner;
        private PathfindingTest pathfindingScript;

        public GridManager GridManager => gridManager;

        /// <summary>
        /// Unity Start method. Initializes grid, managers, and generates the city grid.
        /// </summary>
        void Start()
        {
            pathfindingScript = GetComponent<PathfindingTest>();
            gridManager = new GridManager();
            citizenSpawner = new CitizenSpawner(citizenPrefab, gridManager, pathfindingScript);
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

        /// <summary>
        /// Generates the city grid and populates it with buildings and citizens.
        /// </summary>
        /// <param name="houses">Number of houses to spawn.</param>
        private void GenerateGrid(int houses)
        {
            int buildingsSpawned = 0;
            for (int x = 0; x < gridX; x++)
            {
                for (int z = 0; z < gridZ; z++)
                {
                    Vector3 position = new Vector3(x + 0.5f, 0, z + 0.5f);
                    // Randomly pick a building prefab to spawn
                    int randomBuildingIndex = Random.Range(0, 3);
                    GameObject buildingObject = Instantiate(buildings[randomBuildingIndex], position, Quaternion.Euler(90, Random.Range(1, 4) * 90, 0));
                    int buildingTypeIndex = (buildingsSpawned < houses) ? 0 : Random.Range(1, 4);
                    buildingObject.GetComponent<BuildingObject>().SetRoofMaterial(buildingMaterials[buildingTypeIndex]);
                    Transform[] childObjects = buildingObject.GetComponentsInChildren<Transform>();
                    List<Vector2> buildingChildrenPositions = new();
                    // Collect all grid tile positions for this building
                    for (int i = 0; i < childObjects.Length; i++)
                    {
                        if (childObjects[i].tag == "Grid Tile")
                        {
                            Vector3 buildingPosition = childObjects[i].transform.position;
                            buildingChildrenPositions.Add(new Vector2(buildingPosition.x, buildingPosition.z));
                        }
                    }
                    // Create and register the building
                    Building building = BuildingFactory.CreateBuilding($"Grid {x} {z} {(BuildingType)buildingTypeIndex}", buildingChildrenPositions.ToArray(), (BuildingSize)randomBuildingIndex, buildingObject, (BuildingType)buildingTypeIndex);
                    gridManager.AddBuilding(building);
                    // Spawn citizens for house buildings
                    for (int i = 0; i < childObjects.Length; i++)
                    {
                        if (childObjects[i].tag == "Grid Tile")
                        {
                            Vector3 buildingPosition = childObjects[i].transform.position;
                            if (buildingsSpawned < houses)
                            {
                                citizenSpawner.SpawnCitizen(buildingPosition, building);
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

        /// <summary>
        /// Returns a random building of the specified type using the GridManager.
        /// </summary>
        /// <param name="buildingType">The type of building to retrieve.</param>
        /// <returns>A random Building of the specified type, or null if none exist.</returns>
        public Building GetRandomBuildingByType(BuildingType buildingType)
        {
            return gridManager.GetRandomBuildingByType(buildingType);
        }
    }
}
