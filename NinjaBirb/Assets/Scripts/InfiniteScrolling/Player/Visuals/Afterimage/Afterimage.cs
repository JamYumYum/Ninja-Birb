using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Afterimage : MonoBehaviour
{
    private static SpriteRenderer sourceRenderer;

    public float imageDuration = 0.2f;
    public float startAlpha = 100f;
    public float endAlpha = 0f;
    private float currentAlpha;

    private float currentLifeTime = 0f;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();

        currentAlpha = startAlpha;

    }

    private void OnEnable()
    {
        currentLifeTime = 0f;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, (startAlpha) / 255);
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
        spriteRenderer.sprite = sourceRenderer.sprite;
        Color color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b,
            spriteRenderer.color.a - ((Time.deltaTime / imageDuration) * (startAlpha - endAlpha)/255));
        spriteRenderer.color = color;
        currentLifeTime += Time.deltaTime;
    }

    public static void SetTarget(SpriteRenderer source)
    {
        sourceRenderer = source;
    }
}
