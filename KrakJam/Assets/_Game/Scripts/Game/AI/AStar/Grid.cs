using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.AI.Astar
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private Transform player;
        
        [SerializeField] private LayerMask unwalkableMask;
        [SerializeField] private Vector2 gridWorldSize;
        [SerializeField] private float nodeRadius;
        [SerializeField] private bool showGizmos = true;

        private float nodeDiamater;
        private int gridSizeX, gridSizeY;
        private Node[,] grid;

        public int MaxSize => gridSizeX * gridSizeY;
        
        private void Awake()
        {
            nodeDiamater = nodeRadius * 2;
            gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiamater);
            gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiamater);

            CreateGrid();
        }

        public Node NodeFromWorldPoint(Vector3 worldPosition)
        {
            float percentX = (worldPosition.x + gridWorldSize.x/2) / gridWorldSize.x;
            float percentY = (worldPosition.y + gridWorldSize.y/2) / gridWorldSize.y;
            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
            int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

            return grid[x, y];
        }

        public bool IsPositionWalkable(Vector3 position)
        {
            return NodeFromWorldPoint(position).Walkable;
        }

        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }

                    int checkX = node.GridX + x;
                    int checkY = node.GridY + y;

                    if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    {
                        neighbours.Add(grid[checkX, checkY]);
                    }
                }
            }

            return neighbours;
        }

        private void CreateGrid()
        {
            grid = new Node[gridSizeX,  gridSizeY];
            Vector3 worldbottomLeft =
                (Vector2)transform.position - new Vector2(gridWorldSize.x / 2, gridWorldSize.y / 2);

            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    Vector2 worldPoint = (Vector2)worldbottomLeft +
                                         new Vector2(x * nodeDiamater + nodeRadius, y * nodeDiamater + nodeRadius);
                    bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkableMask));
                    grid[x,y] = new Node(walkable, worldPoint, x, y);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y , 1));

            if (grid != null && showGizmos)
            {
                Node playerNode = NodeFromWorldPoint(player.position);

                foreach (var node in grid)
                {
                    Gizmos.color = node.Walkable ? Color.white : Color.red;
                    Gizmos.DrawCube(node.WorldPosition, Vector3.one * (nodeDiamater - 0.01f));
                }
            }
        }
    }

}