using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    private float width;
    private float height;

    //Position of player; for now screencenter
    private float posX;
    private float posY;

    private IPlayerMovement player;

    [SerializeField] GameObject PlayerCam;
    private void Start()
    {
        player = GetComponent<IPlayerMovement>();
        width = Screen.width;
        height = Screen.height;

        if(PlayerCam != null)
        {
            Camera cam = PlayerCam.GetComponent<Camera>();
            Vector3 playerPos = cam.WorldToScreenPoint(transform.position);
            posX = playerPos.x;
            posY = playerPos.y;
        }
        //posX = width / 2;
        //posY = height / 2;
        
    }


   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (!player.isDashing && !player.bounceLock)
                {
                    Vector3 mousePos = Input.mousePosition;
                    Vector2 direction = new Vector2(mousePos.x - posX, mousePos.y - posY);
                    //float relIntensityX = direction.x / width;
                    //float relIntensityY = direction.y / height;
                    //float intensity = new Vector2(relIntensityX, relIntensityY).magnitude;
                    float intensity = direction.magnitude / new Vector2(width, height).magnitude;

                    player.Dash(direction, intensity);
                }
            }
        }
    }
}
