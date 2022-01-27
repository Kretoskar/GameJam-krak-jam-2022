using System.Collections;
using System.Collections.Generic;
using Game.Input;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    private Animator aniamtor;
    
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        aniamtor = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Mathf.Abs(playerInput.AxisInput.x) > 0.1f)
        {
            aniamtor.SetBool("FacingRight", playerInput.AxisInput.x > 0);
        }
    }
}
