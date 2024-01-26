using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

namespace Base.Gameplay
{
    public class PathfindingTest : MonoBehaviour
    {
        public GenerationTest generationTestScript;

        private Vector2[] adjacentPositions = new Vector2[8];

        private void Start()
        {
            adjacentPositions[0] = new Vector2(-1, 0);
            adjacentPositions[1] = new Vector2(-1, 1);
            adjacentPositions[2] = new Vector2(0, 1);
            adjacentPositions[3] = new Vector2(1, 1);
            adjacentPositions[4] = new Vector2(0, 1);
            adjacentPositions[5] = new Vector2(1, 0);
            adjacentPositions[6] = new Vector2(-1, 0);
            adjacentPositions[7] = new Vector2(-1, -1);

            //GenerateNodeGrid();
        }

        public Vector2[] FindPath(Vector2 startPos, Vector2 endPos)
        {
            foreach (TextMeshProUGUI textMesh in pathfindingGridNumbers.Values)
            {
                textMesh.text = "0";
            }

            if (generationTestScript.buildingGrid.ContainsKey(endPos))
            {
                print("End is wall");
            }

            List<Node> openList = new();
            List<Node> closedList = new();

            openList.Add(new Node(null, startPos));

            List<Vector2> path = new();

            int iteration = 0;
            while (openList.Count > 0)
            {
                iteration++;

                Node currentNode = openList[0];

                for (int i = 0; i < openList.Count; i++)
                {
                    if (openList[i].f < currentNode.f)
                    {
                        currentNode = openList[i];
                    }
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                if (currentNode.GetPosition() == endPos || iteration == 128)
                {
                    while (currentNode.GetParent() != null)
                    {
                        path.Add(currentNode.GetPosition());
                        currentNode = currentNode.GetParent();
                    }
                    path.Add(startPos);
                    break;
                }

                List<Node> children = new();

                for (int i = 0; i < 8; i++)
                {
                    Vector2 nodePos = currentNode.GetPosition() + adjacentPositions[i];

                    if (nodePos.x < 0 || nodePos.x > generationTestScript.gridX || nodePos.y < 0 || nodePos.y > generationTestScript.gridZ)
                    {
                        continue;
                    }

                    if (nodePos != endPos && generationTestScript.buildingGrid.ContainsKey(nodePos))
                    {
                        continue;
                    }

                    children.Add(new Node(currentNode, nodePos));
                }

                foreach (Node child in children)
                {
                    if (closedList.Contains(child))
                    {
                        continue;
                    }

                    child.g = currentNode.g + 1;
                    child.h = Mathf.Pow(child.GetPosition().x - endPos.x, 2) + Mathf.Pow(child.GetPosition().y - endPos.y, 2);
                    child.f = child.g + child.h;

                    /*
                    if (pathfindingGridNumbers.ContainsKey(child.GetPosition()))
                    {
                        pathfindingGridNumbers[child.GetPosition()].text = $"{child.f}";
                    }*/

                    if (openList.Contains(child))
                    {
                        continue;
                    }

                    openList.Add(child);
                }
            }

            return path.ToArray();
        }

        //THIS IS ALL FOR DEBUGGING THE GRID
        public Transform canvas;
        public GameObject gridNumberPrefab;
        
        private Dictionary<Vector2, TextMeshProUGUI> pathfindingGridNumbers = new();


        private void GenerateNodeGrid()
        {
            for (int x = 0; x < generationTestScript.gridX; x++)
            {
                for (int y = 0; y < generationTestScript.gridZ; y++)
                {
                    Vector2 position = new Vector2(x, y);
                    GameObject gridNumber = Instantiate(gridNumberPrefab, position, Quaternion.identity, canvas);

                    pathfindingGridNumbers.Add(position, gridNumber.GetComponent<TextMeshProUGUI>());
                }
            }
        }
    }

    public class Node
    {
        private Vector2 position;
        private Node parent;

        public float g = 0;
        public float h = 0;
        public float f = 0;

        public Node(Node _parent, Vector2 _position)
        {
            parent = _parent;
            position = _position;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public Node GetParent()
        {
            return parent;
        }
    }
}