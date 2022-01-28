using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopySortingOrder : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private int addition = 1;

    private SpriteRenderer selfSr;

    private void Awake()
    {
        selfSr = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        selfSr.sortingOrder = sr.sortingOrder + addition;
    }
}
