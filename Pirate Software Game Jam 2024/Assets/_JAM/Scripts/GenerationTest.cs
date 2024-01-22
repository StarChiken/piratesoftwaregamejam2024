using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

                Building building = new Building($"Grid {x} {y}", buildingChildrenPositions, (BuildingSize)randomBuildingIndex, buildingObject);

                for (int i = 0; i < buildingObject.transform.childCount; i++)
                {
                    buildingGrid.Add(buildingChildrenPositions[i], building);
                }

                y += gridSpacing;
            }
            x += gridSpacing;
        }
    }

    [System.Serializable]
    public class Building
    {
        public string name;
        public Vector2[] gridPositions;
        public BuildingSize buildingSize;
        public GameObject buildingObject;
        public Mesh buildingMesh;

        public Building(string _name, Vector2[] _gridPositions, BuildingSize _buildingSize, GameObject _buildingObject)
        {
            name = _name;
            buildingSize = _buildingSize;
            gridPositions = _gridPositions;
            buildingObject = _buildingObject;
        }
    }

    public enum BuildingSize
    {
        OneByOne,
        TwoByOne,
        TwoByTwo,
        LTwoByTwo
    }
}
