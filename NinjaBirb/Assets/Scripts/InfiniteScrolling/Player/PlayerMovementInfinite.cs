using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementInfinite : MonoBehaviour, IPlayerMovement
{
    public float intensityScale = 60f;
    Coroutine timeoutDashCo;

    private float DashDuration = 0.1f;
    private bool IsDashing = false;

    public float dashDuration { get => DashDuration; set => DashDuration = value; }

    public bool isDashing { get => IsDashing; set => IsDashing = value; }
    public float bounceLockDuration { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public bool bounceLock { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private CharacterController controller;

    public void Dash(Vector2 direction, float intensity)
    {
        throw new System.NotImplementedException();
    }

    private void DashEnd()
    {
        controller.UseGravity(true);


        //controller.SetVelocity(Vector2.zero, 0f);
        controller.ScaleVelocity(0.05f);
        isDashing = false;
    }

    IEnumerator TimeoutDash()
    {
        yield return new WaitForSeconds(dashDuration);
        DashEnd();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (timeoutDashCo != null) StopCoroutine(timeoutDashCo);

        if (isDashing) DashEnd();

        Debug.Log(collision);
    }
}
