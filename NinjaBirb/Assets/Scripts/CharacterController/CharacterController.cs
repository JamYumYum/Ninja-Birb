using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterController : MonoBehaviour
{
    private float gravityScale = 2;
    private Vector2 gravityDirection = Vector2.down;
    private bool gravityActive = true;
    private float gravity = 9.81f;
    [SerializeField]private bool isGrounded = false;
    private bool isTouchingRight = false;
    private Vector2 groundNormal;
    private Vector2 rightWallNormal;

    public LayerMask groundMask;
    public float raycastLengthGround = 0.5f;
    public float boxcastWidth = 0.5f;
    public float boxcastHeight = 0.1f;
    public float raycastLengthRight = 0.5f;

    public float GravityScale { get => gravityScale;}
    public Vector2 GravityDirection { get => gravityDirection; }
    public bool GravityActive { get => gravityActive; }
    public float Gravity { get => gravity; }
    public bool IsGrounded { get => isGrounded; }
    public bool IsTouchingRight { get => isTouchingRight; }
    public Vector2 GroundNormal { get => groundNormal; }
    public Vector2 RightWallNormal { get => rightWallNormal; }

    [SerializeField] private bool infiniteScrollBased = false;
    private Rigidbody2D rb;


    //Events for infintescroll
    public event EventHandler OnGravityUpdate;
    public event EventHandler<OnSetVelocityArgs> OnSetVelocity;
    public event EventHandler<OnScaleVelocityArgs> OnScaleVelocity;
    public event EventHandler OnLanding;

    //Eventargs
    public class OnSetVelocityArgs : EventArgs
    {
        public Vector2 direction;
        public float velocity;
    }
    public class OnScaleVelocityArgs : EventArgs
    {
        public float scale;
    }



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (infiniteScrollBased)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        if (gravityActive)
        {
            if (infiniteScrollBased)
            {
                OnGravityUpdate?.Invoke(this, EventArgs.Empty);
            }
            else rb.AddForce(gravityDirection * gravity * gravityScale);
        }
        //Groundcheck
        //RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), gravityDirection, raycastLengthGround, groundMask);
        RaycastHit2D hit = Physics2D.BoxCast(new Vector2(transform.position.x, transform.position.y), 
            new Vector2(boxcastWidth, boxcastHeight), 0f, gravityDirection, raycastLengthGround, groundMask);
        Debug.DrawRay(transform.position, gravityDirection * raycastLengthGround);
        
        if (hit.collider != null)
        {
            if(isGrounded == false)
            {
                OnLanding?.Invoke(this, EventArgs.Empty);
            }
            
            isGrounded = true;
            groundNormal = hit.normal;
            
        }
        else
        {
            isGrounded = false;
        }

        RaycastHit2D[] hits = new RaycastHit2D[3];

        //Wallcheck Right side
        hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, raycastLengthRight, groundMask);
        Debug.DrawRay(transform.position, Vector2.right * raycastLengthRight);

        if (hit.collider != null)
        {
            isTouchingRight = true;
            rightWallNormal = hit.normal;
        }
        else
        {
            isTouchingRight = false;
        }
    }


    public void SetGravityScale(float scale)
    {
        gravityScale = scale;
    }

    public void SetGravityDirection(Vector2 direction)
    {
        gravityDirection = direction.normalized;
    }

    public void UseGravity(bool x)
    {
        gravityActive = x;
    }

    public void SetVelocity(Vector2 direction, float velocity)
    {
        if (infiniteScrollBased)
        {
            OnSetVelocity?.Invoke(this, new OnSetVelocityArgs { direction = direction, velocity = velocity });
        }
        else rb.velocity = direction.normalized * velocity;
    }

    public void ScaleVelocity(float scale)
    {
        if (infiniteScrollBased)
        {
            OnScaleVelocity?.Invoke(this, new OnScaleVelocityArgs { scale = scale });
        }
        else rb.velocity *= scale;
    }

    
    
}
