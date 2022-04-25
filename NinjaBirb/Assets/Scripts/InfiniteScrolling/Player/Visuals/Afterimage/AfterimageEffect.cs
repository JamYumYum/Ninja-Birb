using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterimageEffect : MonoBehaviour
{
    private GameObject world; //the world, which moves relative to the player in scrollbased mode
    private Rigidbody2D worldRb;

    private SpriteRenderer sourceRenderer;
    private GameObjectPool<Afterimage> pool;
    private PlayerMovementFinite playerMovement;
    private Transform targetTransform;
    private Transform thisTransform;


    public float afterimageInterval = 0.1f;
    private float afterimageTimer = 0f;

    public int startPoolSize = 40;

    private void Awake()
    {

        playerMovement = gameObject.GetComponentInParent<PlayerMovementFinite>();
        pool = new GameObjectPool<Afterimage>("Afterimage");

    }

    // Start is called before the first frame update
    void Start()
    {
        world = FindObjectOfType<EndlessTerrain>().getGrid;
        worldRb = world.GetComponent<Rigidbody2D>();

        thisTransform = transform;
        
        sourceRenderer = GetComponent<SpriteRenderer>();
        targetTransform = GetComponentInParent<Transform>();
        Afterimage.Init(sourceRenderer, worldRb, thisTransform.localScale.x);

        for(int i = 0; i<startPoolSize; i++)
        {
            GameObject newItem = new GameObject("Afterimage");
            newItem.AddComponent<Afterimage>();
            newItem.transform.SetParent(thisTransform);
            newItem.SetActive(false);
            pool.Add(newItem);
        }
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if(playerMovement.isDashing || playerMovement.isBouncing)
    //    {
    //        if(afterimageTimer >= afterimageInterval || afterimageTimer == 0f)
    //        {
    //            CreateAfterimage();
    //        }
    //        afterimageTimer += Time.deltaTime;
    //    }
    //    else
    //    {
    //        afterimageTimer = 0f;
    //    }
    //}
    void FixedUpdate()
    {
        if (playerMovement.isDashing || playerMovement.isBouncing)
        {
            if (afterimageTimer >= afterimageInterval || afterimageTimer == 0f)
            {
                CreateAfterimage();
                afterimageTimer -= afterimageInterval;
            }
            afterimageTimer += Time.fixedDeltaTime;
        }
        else
        {
            afterimageTimer = 0f;
        }

    }

    private void CreateAfterimage()
    {
        GameObject newAfterimage = pool.Get();
        newAfterimage.transform.SetParent(thisTransform);
        newAfterimage.transform.position = Vector3.zero;
        newAfterimage.transform.localScale = Vector3.one;
        newAfterimage.SetActive(true);
    }

    public void SetPivot(GameObject pivot)
    {
        world = pivot;
    }
}
