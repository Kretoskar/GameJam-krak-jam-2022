using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.AI.Astar
{
    public class Node : IHeapItem<Node>
    {
        private int gridX;
        private int gridY;
        private bool walkable;
        private Vector3 worldPosition;
        private int gCost;
        private int hCost;
        private Node parent;
        private int heapIndex;

        public Node Parent
        {
            get => parent;
            set => parent = value;
        }

        public int GCost
        {
            get => gCost;
            set => gCost = value;
        }

        public int GridX
        {
            get => gridX;
            set => gridX = value;
        }

        public int GridY
        {
            get => gridY;
            set => gridY = value;
        }

        public int HCost
        {
            get => hCost;
            set => hCost = value;
        }

        public bool Walkable
        {
            get => walkable;
            set => walkable = value;
        }

        public Vector3 WorldPosition
        {
            get => worldPosition;
            set => worldPosition = value;
        }

        public int FCost => gCost + hCost;

        public Node(bool walkable, Vector3 worldPosition, int gridX, int gridY)
        {
            this.walkable = walkable;
            this.worldPosition = worldPosition;
            this.gridX = gridX;
            this.gridY = gridY;
        }

        public int CompareTo(Node other)
        {
            int compare = FCost.CompareTo(other.FCost);
            if (compare == 0)
            {
                compare = hCost.CompareTo(other.HCost);
            }

            return -compare;
        }

        public int HeapIndex
        {
            get { return heapIndex; }
            set { heapIndex = value; }
        }
    }

}