using System;
using System.Collections;
using System.Collections.Generic;
using Game.Input;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private bool isSoul;
    [SerializeField] private GameObject head;
    [SerializeField] [Range(0,360)] private float headRotOffset = 0;
    [SerializeField] [Range(0, 350)] private float headRotClamp = 60;
 
    private SpriteRenderer sr;
    private SpriteRenderer headSr;
    
    private Animator headAnimator;
    private Animator aniamtor;
    
    private PlayerInput playerInput;

    [SerializeField] private float debugRot;
    
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        aniamtor = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerInput.MouseClick += Shoot;
    }

    private void OnDisable()
    {
        playerInput.MouseClick -= Shoot;
    }

    private void Start()
    {
        headSr = head.GetComponent<SpriteRenderer>();
        headAnimator = head.GetComponent<Animator>();
    }

    private void Update()
    {
        bool lookRight = playerInput.MousePos.x > transform.position.x;
        headSr.flipX = !lookRight;
        
        Vector3 diff = (Vector3)playerInput.MousePos - transform.position;
        diff.Normalize();

        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        float rot;

        rot = rotZ - headRotOffset;
        if (headSr.flipX)
        {
            rot += 180;

            debugRot = rot;
            
            if (rot > 270 && rot < 300)
            {
                rot = 360 - headRotClamp;
            }
            else if (rot > headRotClamp && rot < 90)
                rot = headRotClamp;
        }
        else
        {
            rot = Mathf.Clamp(rot, -headRotClamp, headRotClamp);
        }

        head.transform.parent.rotation = Quaternion.Euler(0f, 0f, rot);
        
        bool moving = Mathf.Abs(playerInput.AxisInput.x) > 0.1f;
        headAnimator.SetBool("IsMoving", moving);
        aniamtor.SetBool("IsMoving", moving);
        
        if (!moving) return;

        if (isSoul)
        {
            sr.flipX = playerInput.AxisInput.x > 0;
        }
        else
        {
            sr.flipX = playerInput.AxisInput.x < 0;
        }
    }

    private void Shoot()
    {
        headAnimator.SetTrigger("Shoot");
    }
}
