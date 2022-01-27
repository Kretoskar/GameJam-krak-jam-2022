using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Rendering
{
    [RequireComponent(typeof(Renderer))]
    public class PositionRendererSorter : MonoBehaviour
    {
        [SerializeField] [Range(-10, 10)] private float _offset = 0;
        [SerializeField]
        private bool _runOnce = true;
        
        private int _sortingOrderBase;
        private Renderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _sortingOrderBase = _renderer.sortingOrder;
        }

        private void LateUpdate()
        {
            _renderer.sortingOrder = (int) ((_sortingOrderBase - transform.position.y - _offset)*100);    
            if(_runOnce)
                Destroy(this);
        }

        private void OnDrawGizmos()
        {
            var position = transform.position;
            Vector2 gizmoPosition = new Vector2(position.x, position.y + _offset);
            Gizmos.DrawSphere(gizmoPosition, .05f);
        }
    }
}