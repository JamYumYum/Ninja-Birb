using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Afterimage : MonoBehaviour
{
    private static SpriteRenderer sourceRenderer;
    private static Rigidbody2D worldRb;
    private static float parentScale;

    public float imageDuration = 0.2f;
    public float startAlpha = 100f;
    public float endAlpha = 0f;
    private Transform thisTransform;

    private float localPosX = 0f;
    private float localPosY = 0f;

    private float currentLifeTime = 0f;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();

        thisTransform = gameObject.transform;
    }

    private void OnEnable()
    {
        localPosX = 0f;
        localPosY = 0f;
        currentLifeTime = 0f;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, (startAlpha) / 255);
        spriteRenderer.sprite = sourceRenderer.sprite;
    }


    // Update is called once per frame
    void Update()
    {
        if(currentLifeTime > imageDuration)
        {
            GameObjectPool<Afterimage>.instance.Add(gameObject);
            gameObject.SetActive(false);
            return;
        }
        
        Color color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b,
            spriteRenderer.color.a - ((Time.deltaTime / imageDuration) * (startAlpha - endAlpha)/255));
        spriteRenderer.color = color;
        currentLifeTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        localPosX += worldRb.velocity.x * Time.fixedDeltaTime ;
        localPosY += worldRb.velocity.y * Time.fixedDeltaTime ;

        thisTransform.localPosition = new Vector3(localPosX/parentScale,localPosY/parentScale, 0f);
    }

    public static void Init(SpriteRenderer source, Rigidbody2D worldRb, float parentScale)
    {
        sourceRenderer = source;
        Afterimage.worldRb = worldRb;
        Afterimage.parentScale = parentScale;
    }
}
