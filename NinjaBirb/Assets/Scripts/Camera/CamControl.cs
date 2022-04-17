using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    [SerializeField] GameObject centerObject;
    [SerializeField] [Tooltip("Between -0.5f to 0.5f")] float RelOffsetX = -0.25f;
    [SerializeField] [Tooltip("Between -0.5f to 0.5f")] float RelOffsetY = 0f;
    [SerializeField] [Tooltip("Screenpercentage")]private float deadZone = 0.05f;

    private float worldSpaceDeadZone;

    private float offsetX;
    private float offsetY;

    private Transform target;
    private CharacterController controller;
    private Camera cam;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        target = centerObject.GetComponent<Transform>();
        controller = centerObject.GetComponent<CharacterController>();
        cam = GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();

        float screenOffsetX = Screen.width * RelOffsetX;
        float screenOffsetY = Screen.height * RelOffsetY;

        Vector3 offset = cam.ScreenToWorldPoint(new Vector3(0f, 0f, -10f)) 
            - cam.ScreenToWorldPoint(new Vector3(screenOffsetX, screenOffsetY, -10f));
        offsetX = offset.x;
        offsetY = offset.y;

        worldSpaceDeadZone = (cam.ScreenToWorldPoint(new Vector3(Screen.width * deadZone, 0f, 0f))
            - cam.ScreenToWorldPoint(Vector3.zero)).magnitude;

        transform.position = new Vector3(target.position.x - offsetX, target.position.y - offsetY, -10);

        if (controller != null)
        {
            controller.OnSetVelocity += controller_OnSetVelocity;
            controller.OnScaleVelocity += controller_OnScaleVelocity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = new Vector3(centerObject.transform.position.x, centerObject.transform.position.y, 0f);
        Vector3 cameraPos = new Vector3(transform.position.x, transform.position.y, 0f);
        //Debug.Log(worldSpaceDeadZone);
        if ((cameraPos-playerPos).magnitude > worldSpaceDeadZone)
        {
            transform.position = new Vector3(target.position.x - offsetX, target.position.y - offsetY, -10);

        }
    }

    public void controller_OnSetVelocity(object sender, CharacterController.OnSetVelocityArgs args)
    {

    }

    public void controller_OnScaleVelocity(object sender, CharacterController.OnScaleVelocityArgs args)
    {

    }
}
