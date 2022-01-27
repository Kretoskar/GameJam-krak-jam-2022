using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.AI.Astar
{
    public class PathRequestManager : MonoBehaviour
    {
        private Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
        private PathRequest currentPathRequest;

        private static PathRequestManager instance;
        private Pathfinding pathFinding;

        private bool isProcessingPath;

        private void Awake()
        {
            instance = this;
            pathFinding = GetComponent<Pathfinding>();
        }
        
        public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
        {
            PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
            instance.pathRequestQueue.Enqueue(newRequest);
            instance.TryProcessNext();
        }

        public void FinishProcessingPath(Vector3[] path, bool success)
        {
            currentPathRequest.Callback(path, success);
            isProcessingPath = false;
            TryProcessNext();
        }
        
        private void TryProcessNext()
        {
            if (!isProcessingPath && pathRequestQueue.Count > 0)
            {
                currentPathRequest = pathRequestQueue.Dequeue();
                isProcessingPath = true;
                pathFinding.StartFindPath(currentPathRequest.PathStart, currentPathRequest.PathEnd);
            }
        }

        struct PathRequest
        {
            public Vector3 PathStart;
            public Vector3 PathEnd;
            public Action<Vector3[], bool> Callback;

            public PathRequest(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
            {
                PathStart = pathStart;
                PathEnd = pathEnd;
                Callback = callback;
            }
        }
    }
}