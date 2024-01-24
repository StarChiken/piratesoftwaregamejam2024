using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Base.Gameplay
{


    public class PathfindingTest : MonoBehaviour
    {
        public GenerationTest generationTestScript;
        public GameObject circleIndicator;

        public GameObject nodePrefab;

        private Vector2[] adjacentPositions = new Vector2[8];

        public Vector2 startPostion;
        public Vector2 endPostion;

        private void Start()
        {
            adjacentPositions[0] = new Vector2(-1, 0);
            adjacentPositions[1] = new Vector2(-1, 1);
            adjacentPositions[2] = new Vector2(0, 1);
            adjacentPositions[3] = new Vector2(1, 1);
            adjacentPositions[4] = new Vector2(0, 1);
            adjacentPositions[5] = new Vector2(-1, 1);
            adjacentPositions[6] = new Vector2(-1, 0);
            adjacentPositions[7] = new Vector2(-1, -1);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                StartCoroutine(FindPath(startPostion, endPostion));
                circleIndicator.transform.position = endPostion;
                print(generationTestScript.buildingGrid.ContainsKey(endPostion));
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (circleIndicator.transform.position == (Vector3)endPostion)
                {
                    circleIndicator.transform.position = startPostion;
                }
                else
                {
                    circleIndicator.transform.position = endPostion;
                }
            }
        }

        public IEnumerator FindPath(Vector2 startPos, Vector2 endPos)
        {
            List<Node> openList = new();
            List<Node> closedList = new();

            openList.Add(new Node(null, startPos));

            List<Vector2> path = new();

            while (openList.Count > 0)
            {
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

                if (currentNode.GetPosition() == endPos)
                {
                    print("found end");
                    while (currentNode.GetParent() != null)
                    {
                        path.Add(currentNode.GetPosition());
                        currentNode = currentNode.GetParent();
                    }

                    circleIndicator.transform.position = endPos;
                    break;
                }

                List<Node> children = new();

                for (int i = 0; i < 8; i++)
                {
                    Vector2 nodePos = currentNode.GetPosition() + adjacentPositions[i];

                    if (nodePos.x < 0 || nodePos.x > generationTestScript.gridX || nodePos.y < 0 ||
                        nodePos.y > generationTestScript.gridZ)
                    {
                        continue;
                    }

                    if (generationTestScript.buildingGrid.ContainsKey(nodePos))
                    {
                        print("found wall");
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
                    child.h = (int)Mathf.Pow(child.GetPosition().x - endPos.x, 2) +
                              (int)Mathf.Pow(child.GetPosition().y - endPos.y, 2);
                    child.f = child.g + child.h;

                    if (openList.Contains(child))
                    {
                        continue;
                    }

                    openList.Add(child);
                    print("added child to open list");
                }

                print("Current Node: " + currentNode.GetPosition());
                circleIndicator.transform.position = currentNode.GetPosition();
                yield return new WaitForSeconds(0.05f);
            }
        }
    }

    public class Node
    {
        private Vector2 position;
        private Node parent;

        public int g = 0;
        public int h = 0;
        public int f = 0;

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