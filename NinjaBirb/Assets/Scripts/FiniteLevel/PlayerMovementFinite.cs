﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovementFinite : MonoBehaviour, IPlayerMovement
{
    public float intensityScale = 100f;
    Coroutine timeoutDashCo;

    private float DashDuration = 0.12f;
    private bool IsDashing = false;

    public int airdashAmount = 0;
    public int maxAirdashAmount = 1;

    public float dashDuration { get => DashDuration; set => DashDuration = value; }

    public bool isDashing { get => IsDashing; set => IsDashing = value; }

    public float bounceIntensity = 10f;

    public float groundBounceIntensity = 15f;


    private CharacterController controller;

    public void Dash(Vector2 direction, float intensity)
    {
        //check airborne
        if(!controller.IsGrounded)
        {
            //check airdash available
            if (airdashAmount > 0)
            {
                airdashAmount--;
            }
            else return;
        }
        
        //check downtap while grounded
        if (controller.IsGrounded && (direction.y < 0))
        {
            Bounce(direction, controller.GroundNormal * -1, groundBounceIntensity);
            return;
        }
        //check righttap while touching right wall
        if(controller.IsTouchingRight && (direction.x > 0))
        {
            Bounce(direction, controller.RightWallNormal * -1, bounceIntensity);
            return;
        }

        //normal dash 
        if (timeoutDashCo != null) StopCoroutine(timeoutDashCo);
        timeoutDashCo = StartCoroutine(TimeoutDash());

        controller.UseGravity(false);
        controller.SetVelocity(direction.normalized, (intensity <= 0.16f? 3*0.16f : (3f*intensity<=1? 3f*intensity : 1)) *intensityScale);
        isDashing = true;

        Debug.Log(intensity);
    }

    private void DashEnd()
    {
        controller.UseGravity(true);
        

        
        //controller.SetVelocity(Vector2.zero, 0f);
        controller.ScaleVelocity(0.05f);
        isDashing = false;
    }

    private void Bounce(Vector2 inDirection, Vector2 inNormal, float intensity)
    {
        controller.SetVelocity(Vector2.Reflect(inDirection, inNormal).normalized, intensity);
        airdashAmount = maxAirdashAmount;
    }

    IEnumerator TimeoutDash()
    {
        yield return new WaitForSeconds(dashDuration);
        DashEnd();
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        controller.OnLanding += controller_OnLanding;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void controller_OnLanding(object sender, EventArgs args)
    {
        airdashAmount = maxAirdashAmount;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (timeoutDashCo != null) StopCoroutine(timeoutDashCo);

        if (isDashing)
        {
            DashEnd();
            Bounce(collision.relativeVelocity * -1, collision.GetContact(0).normal* -1, bounceIntensity);
            
        }
        else if (controller.IsGrounded)
        {
            controller.ScaleVelocity(0.2f);
        }

        
    }

    


}