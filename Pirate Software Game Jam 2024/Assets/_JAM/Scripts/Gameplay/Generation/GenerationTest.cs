using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.InputSystem;

/// <summary>
/// Handles procedural generation of buildings and citizens for the city grid.
/// </summary>
public class GenerationTest : MyMonoBehaviour
{
    // Config properties
    [SerializeField] private ConfigManager m_configManager;
    public GameplayConfig GameplayConfig => m_configManager.GetConfig<GameplayConfig>();
    private BuildingConfig m_buildingConfig => m_configManager.GetConfig<BuildingConfig>();

    private bool m_canSpawnTemple = true;
    private GameObject[] m_buildings = new GameObject[3];
    private Material[] m_buildingMaterials = new Material[4];
    private GridManager m_gridManager;
    private CitizenSpawner m_citizenSpawner;
    private PathfindingTest m_pathfindingScript;

    public GridManager GridManager => m_gridManager;

    /// <summary>
    /// Unity Start method. Initializes grid, managers, and generates the city grid.
    /// </summary>
    void Start()
    {
        if (!TryGetComponent<PathfindingTest>(out m_pathfindingScript))
        {
            Debug.LogError("PathfindingTest component is missing from GenerationTest.");
        }
        m_gridManager = new GridManager();
        m_citizenSpawner = new CitizenSpawner(m_buildingConfig.CitizenPrefab, m_gridManager, m_pathfindingScript);
        m_buildings[0] = m_buildingConfig.Building1x1;
        m_buildings[1] = m_buildingConfig.Building2x1;
        m_buildings[2] = m_buildingConfig.BuildingL;
        m_buildingMaterials[0] = m_buildingConfig.HouseMaterial;
        m_buildingMaterials[1] = m_buildingConfig.FactionDutyMaterial;
        m_buildingMaterials[2] = m_buildingConfig.SanityMaterial;
        m_buildingMaterials[3] = m_buildingConfig.HealthMaterial;
        GenerateGrid(Random.Range(GameplayConfig.MinStartingHouses, GameplayConfig.MaxStartingHouses + 1));
    }

    private void Update()
    {
        //Testing Temple Choosing
        /*
        if (m_canSpawnTemple && Mouse.current.leftButton.wasPressedThisFrame)
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

                        m_canSpawnTemple = false;
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
        for (int x = 0; x < GameplayConfig.GridX; x++)
        {
            for (int z = 0; z < GameplayConfig.GridZ; z++)
            {
                Vector3 position = new Vector3(x + 0.5f, 0, z + 0.5f);
                // Randomly pick a building prefab to spawn
                int randomBuildingIndex = Random.Range(0, 3);
                GameObject buildingObject = Instantiate(m_buildings[randomBuildingIndex], position, Quaternion.Euler(90, Random.Range(1, 4) * 90, 0));
                int buildingTypeIndex = (buildingsSpawned < houses) ? 0 : Random.Range(1, 4);
                BuildingObject buildingObjComponent;
                if (buildingObject.TryGetComponent<BuildingObject>(out buildingObjComponent))
                {
                    buildingObjComponent.SetRoofMaterial(m_buildingMaterials[buildingTypeIndex]);
                }
                else
                {
                    Debug.LogError($"BuildingObject component missing on {buildingObject.name}");
                }
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
                m_gridManager.AddBuilding(building);
                // Spawn citizens for house buildings
                for (int i = 0; i < childObjects.Length; i++)
                {
                    if (childObjects[i].tag == "Grid Tile")
                    {
                        Vector3 buildingPosition = childObjects[i].transform.position;
                        if (buildingsSpawned < houses)
                        {
                            m_citizenSpawner.SpawnCitizen(buildingPosition, building);
                        }
                    }
                }
                buildingObject.name = $"Grid {x} {z} {(BuildingType)buildingTypeIndex}";
                z += GameplayConfig.GridSpacing;
                buildingsSpawned++;
            }
            x += GameplayConfig.GridSpacing;
        }
    }

    /// <summary>
    /// Returns a random building of the specified type using the GridManager.
    /// </summary>
    /// <param name="buildingType">The type of building to retrieve.</param>
    /// <returns>A random Building of the specified type, or null if none exist.</returns>
    public Building GetRandomBuildingByType(BuildingType buildingType)
    {
        return m_gridManager.GetRandomBuildingByType(buildingType);
    }
}
