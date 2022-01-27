using System.Collections;
using System.Collections.Generic;
using Game.Input;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private bool isSoul;

    private SpriteRenderer sr;

    private Animator aniamtor;
    private PlayerInput playerInput;
    
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        aniamtor = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!(Mathf.Abs(playerInput.AxisInput.x) > 0.1f)) return;

        if (isSoul)
            sr.flipX = playerInput.AxisInput.x > 0;
        else
            sr.flipX = playerInput.AxisInput.x < 0;
    }
}
