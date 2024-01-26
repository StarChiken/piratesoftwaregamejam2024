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
        }

        public Vector2[] FindPath(Vector2 startPos, Vector2 endPos)
        {
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

                for (int i = 0; i < children.Count; i++)
                {
                    if (closedList.Contains(children[i]))
                    {
                        continue;
                    }

                    children[i].g = currentNode.g + 1;
                    children[i].h = Mathf.Pow(children[i].GetPosition().x - endPos.x, 2) + Mathf.Pow(children[i].GetPosition().y - endPos.y, 2);
                    children[i].f = children[i].g + children[i].h;

                    if (openList.Contains(children[i]))
                    {
                        continue;
                    }

                    openList.Add(children[i]);
                }
            }

            return path.ToArray();
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