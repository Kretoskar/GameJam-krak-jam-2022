using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.AI.Astar
{
    public class AStarUnit : MonoBehaviour
    {
        public Transform Target;
        private float speed = 10;
        private Vector3[] path;
        private int targetIndex;

        public Vector3[] Path => path;
        
        private void Start()
        {
            StartCoroutine(FindPathContinously());
        }

        private IEnumerator FindPathContinously()
        {
            while (true)
            {
                targetIndex = 0;
                path = new Vector3[0];
                PathRequestManager.RequestPath(transform.position, Target.position, OnPathFound);
                yield return  new WaitForSeconds(.1f);
            }

            yield return null;
        }

        private void OnPathFound(Vector3[] newPath, bool pathSuccessful)
        {
            if (pathSuccessful)
            {
                path = newPath;
                StopCoroutine(FollowPath());
                StartCoroutine(FollowPath());
            }
        }

        private IEnumerator FollowPath()
        {
            Vector3 currentWaypoint = path[0];

            while (true)
            {
                if (transform.position == currentWaypoint)
                {
                    targetIndex++;
                    if (targetIndex >= path.Length)
                    {
                        targetIndex = 0;
                        path = new Vector3[0];
                        yield break;
                    }

                    currentWaypoint = path[targetIndex];
                }
                yield return null;
            }
        }

        private void OnDrawGizmos()
        {
            if (path != null)
            {
                for (int i = targetIndex; i < path.Length; i++)
                {
                    Gizmos.color = Color.black;
                    Gizmos.DrawCube(path[i], Vector3.one / 5);

                    if (i == targetIndex)
                    {
                        Gizmos.DrawLine(transform.position, path[i]);
                    }
                    else
                    {
                        Gizmos.DrawLine(path[i-1], path[i]);
                    }
                }
            }
        }
    }
}