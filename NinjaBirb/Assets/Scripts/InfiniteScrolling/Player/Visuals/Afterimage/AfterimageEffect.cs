using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterimageEffect : MonoBehaviour
{
    private static GameObject world; //the world, which moves relative to the player in scrollbased mode
    private SpriteRenderer sourceRenderer;
    private GameObjectPool<Afterimage> pool;
    private PlayerMovementFinite playerMovement;
    private Transform targetTransform;


    public float afterimageInterval = 0.05f;
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
        if (world == null)
        {
            world = FindObjectOfType<EndlessTerrain>().getGrid;
        }
        sourceRenderer = GetComponent<SpriteRenderer>();
        targetTransform = GetComponentInParent<Transform>();
        Afterimage.SetTarget(sourceRenderer);

        for(int i = 0; i<startPoolSize; i++)
        {
            GameObject newItem = new GameObject("Afterimage");
            newItem.SetActive(false);
            newItem.AddComponent<Afterimage>();
            newItem.transform.SetParent(world.transform);
            pool.Add(newItem);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerMovement.isDashing || playerMovement.isBouncing)
        {
            if(afterimageTimer >= afterimageInterval || afterimageTimer == 0f)
            {
                CreateAfterimage();
            }
            afterimageTimer += Time.deltaTime;
        }
        else
        {
            afterimageTimer = 0f;
        }
    }

    private void CreateAfterimage()
    {
        GameObject newAfterimage = pool.Get();
        newAfterimage.transform.SetParent(world.transform);
        newAfterimage.transform.position = Vector3.zero;
        newAfterimage.transform.localScale = targetTransform.localScale;
        newAfterimage.SetActive(true);
    }

    public static void SetPivot(GameObject pivot)
    {
        world = pivot;
    }
}
