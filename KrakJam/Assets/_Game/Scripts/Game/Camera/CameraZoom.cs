using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Game.Control.Player
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private Transform playerSoul;
        [SerializeField] private Transform playerBody;

        [SerializeField] [Range(0, 10)] private float minOrtSize = 4;
        [SerializeField] [Range(0,10)] private float maxOrtSize = 6;

        [SerializeField] [Range(0,15)] private float maxDistance = 15;

        [SerializeField] private AnimationCurve zoomCurve;
        
        private UnityEngine.Camera camera;

        private void Awake()
        {
            camera = GetComponent<Camera>();
        }

        private void Update()
        {
            camera.orthographicSize = Mathf.Lerp(minOrtSize, maxOrtSize, 
                zoomCurve.Evaluate(Vector3.Distance(playerSoul.position, playerBody.position) / maxDistance));
        }
    }
}