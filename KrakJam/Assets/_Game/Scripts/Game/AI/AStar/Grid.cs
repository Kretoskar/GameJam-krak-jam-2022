using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

namespace Game.AI.Astar
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private LayerMask unwalkableMask;
        [SerializeField] private Vector2 gridWorldSize;
        [SerializeField] private float nodeRadius;

        private float nodeDiamater;
        private int gridSizeX, gridSizeY;
        private Node[,] grid;

        private void Start()
        {
            nodeDiamater = nodeRadius * 2;
            gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiamater);
            gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiamater);

            CreateGrid();
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
                    bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius));
                    grid[x,y] = new Node(walkable, worldPoint);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y , 1));

            if (grid != null)
            {
                foreach (var node in grid)
                {
                    Gizmos.color = node.Walkable ? Color.white : Color.red;
                    Gizmos.DrawCube(node.WorldPosition, Vector3.one * (nodeDiamater - 0.1f));
                }
            }
        }
    }

}