using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EndlessTerrain : MonoBehaviour
{
    [Tooltip("Amount of tiles in a Tilemap per row")]
    public int width = 36;
    [Tooltip("Amount of tiles in a Tilemap per column")]
    public int height = 14;

    private TilesManager tManager;
    private GameObject Grid;
    private Grid grid;
    private Tmap Tmap1;
    private Tmap Tmap2;
    private Tmap Tmap3;
    private Queue<Tmap> tmapsOrder;
    private Rigidbody2D rb;
    private ControllerEventSubscriber controllerSubscription;

    public Tilemap getTmap1 { get => Tmap1.tilemap; }
    public Tilemap getTmap2 { get => Tmap2.tilemap; }
    public Tilemap getTmap3 { get => Tmap3.tilemap; }
    public GameObject getT1 { get => Tmap1.gameObject; }
    public GameObject getT2 { get => Tmap2.gameObject; }
    public GameObject getT3 { get => Tmap3.gameObject; }
    public GameObject getGrid { get => Grid; }
    

    private void Awake()
    {
        tManager = GetComponent<TilesManager>();

        Grid = new GameObject("Grid for Tmap");
        Tmap1 = new Tmap("TMap1", tManager, width, height);
        Tmap2 = new Tmap("TMap2", tManager, width, height);
        Tmap3 = new Tmap("TMap3", tManager, width, height);
        tmapsOrder = new Queue<Tmap>();
        tmapsOrder.Enqueue(Tmap3);  // starting order:   (left)Tmap3 -> (middle)Tmap1 -> (right)Tmap2
        tmapsOrder.Enqueue(Tmap1);  // Player starting in Tmap1
        tmapsOrder.Enqueue(Tmap2);

        Tmap1.gameObject.transform.SetParent(Grid.transform);
        Tmap2.gameObject.transform.SetParent(Grid.transform);
        Tmap3.gameObject.transform.SetParent(Grid.transform);


        grid = Grid.AddComponent<Grid>();
        rb = Grid.AddComponent<Rigidbody2D>();
        controllerSubscription = Grid.AddComponent<ControllerEventSubscriber>();

        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        Physics2D.IgnoreCollision(Tmap1.tilemapCollider, Tmap2.tilemapCollider);
        Physics2D.IgnoreCollision(Tmap1.tilemapCollider, Tmap3.tilemapCollider);
        Physics2D.IgnoreCollision(Tmap3.tilemapCollider, Tmap2.tilemapCollider);

        Tmap2.gameObject.transform.localPosition = new Vector3(width, 0, 0);
        Tmap3.gameObject.transform.localPosition = new Vector3(-width, 0, 0);
    }


    // Start is called before the first frame update
    void Start()
    {
        Tmap1.contentManager.AddFloor();
        Tmap1.contentManager.AddRandomPillar((int)width/2);
        Tmap1.contentManager.AddCeiling();

        Tmap2.contentManager.AddFloor();
        Tmap2.contentManager.AddCeiling();
        Tmap2.contentManager.AddRandomPillar();

        Tmap3.contentManager.AddFloor();
        Tmap3.contentManager.AddCeiling();
        Tmap3.contentManager.AddRandomPillar();

    }

    // Update is called once per frame
    void Update()
    {
        ScrollTilemap();
    }


    void ScrollTilemap()
    {
        if(Grid.transform.position.x < -width)
        {
            Tmap leftTmap = tmapsOrder.Dequeue();
            Tmap middleTmap = tmapsOrder.Dequeue();
            Tmap rightTmap = tmapsOrder.Dequeue();
            // scrolled: newLeft = middle, newMiddle = right, newRight = left
            tmapsOrder.Enqueue(middleTmap);
            tmapsOrder.Enqueue(rightTmap);
            tmapsOrder.Enqueue(leftTmap);

            Grid.transform.position = new Vector3(0, Grid.transform.position.y, Grid.transform.position.z);
            leftTmap.gameObject.transform.localPosition = new Vector3(leftTmap.gameObject.transform.localPosition.x + 2*width, 
                leftTmap.gameObject.transform.localPosition.y, leftTmap.gameObject.transform.localPosition.z);
            middleTmap.gameObject.transform.localPosition = new Vector3(middleTmap.gameObject.transform.localPosition.x - width,
                middleTmap.gameObject.transform.localPosition.y, middleTmap.gameObject.transform.localPosition.z);
            rightTmap.gameObject.transform.localPosition = new Vector3(rightTmap.gameObject.transform.localPosition.x - width,
                rightTmap.gameObject.transform.localPosition.y, rightTmap.gameObject.transform.localPosition.z);

            //generate new content for new right Tmap
            leftTmap.contentManager.ResetContent();
            leftTmap.contentManager.AddFloor();
            leftTmap.contentManager.AddCeiling();
            leftTmap.contentManager.AddRandomPillar();
        }
    }

    private class Tmap
    {
        public GameObject gameObject;
        public Tilemap tilemap;
        public TilemapRenderer tilemapRenderer;
        public TilemapCollider2D tilemapCollider;
        public TilemapContentManager contentManager;
        public Tmap(string name, TilesManager tilesManager, int width, int height)
        {
            gameObject = new GameObject(name);
            tilemap = gameObject.AddComponent<Tilemap>();
            tilemapRenderer = gameObject.AddComponent<TilemapRenderer>();
            tilemapCollider = gameObject.AddComponent<TilemapCollider2D>();
            contentManager = new TilemapContentManager(tilemap, tilesManager, width, height);

            gameObject.layer = LayerMask.NameToLayer("Ground");
        }
    }
}
