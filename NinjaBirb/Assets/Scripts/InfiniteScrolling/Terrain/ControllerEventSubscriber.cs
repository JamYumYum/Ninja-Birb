using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ControllerEventSubscriber : MonoBehaviour
{
    [Tooltip("Listens to this Gameobject's CharacterController component. If empty, then looking for any ChracterController.")]
    public GameObject player;
    
    private CharacterController controller;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        if (player != null) controller = GetComponent<CharacterController>();
        else controller = FindObjectOfType<CharacterController>();

        rb = GetComponent<Rigidbody2D>();

        if (controller != null)
        {
            controller.OnGravityUpdate += controller_OnGravityUpdate;
            controller.OnSetVelocity += controller_OnSetVelocity;
            controller.OnScaleVelocity += controller_OnScaleVelocity;
            
        }
    }

    void controller_OnGravityUpdate(object sender, EventArgs args)
    {
        if (controller == null) return;
        rb.AddForce(controller.GravityDirection* -1 * controller.Gravity * controller.GravityScale);
    }

    void controller_OnSetVelocity(object sender, CharacterController.OnSetVelocityArgs args)
    {
        rb.velocity = args.direction.normalized* -1 * args.velocity;
    }

    void controller_OnScaleVelocity(object sender, CharacterController.OnScaleVelocityArgs args)
    {
        rb.velocity *= args.scale;
    }

}
