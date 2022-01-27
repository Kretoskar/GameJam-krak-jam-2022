using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.AI.Astar
{
    public class Node
    {
        private bool walkable;
        private Vector3 worldPosition;

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

        public Node(bool walkable, Vector3 worldPosition)
        {
            this.walkable = walkable;
            this.worldPosition = worldPosition;
        }
    }

}