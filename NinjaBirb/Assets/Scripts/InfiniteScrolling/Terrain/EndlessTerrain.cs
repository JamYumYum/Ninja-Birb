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

    
    

    private int offsetT1X; //Offsets determine the end position of a Tilemap
    private int offsetT1Y;
    private int offsetT2X;
    private int offsetT2Y;

    

    private TilesManager tManager;

    //Grid and Tilemaps with all need components
    private GameObject Grid;
    private GameObject TMap1;
    private GameObject TMap2;
    private Grid grid;
    private Tilemap tMap1;
    private Tilemap tMap2;
    private TilemapRenderer tMRenderer1;
    private TilemapRenderer tMRenderer2;
    private TilemapCollider2D tMCollider1;
    private TilemapCollider2D tMCollider2;
    private TilemapContentManager t1Manager;
    private TilemapContentManager t2Manager;
    
    private Rigidbody2D rb;
    private ControllerEventSubscriber controllerSubscription;

    public Tilemap getTmap1 { get => tMap1; }
    public Tilemap getTmap2 { get => tMap2; }

    public GameObject getT1 { get => TMap1; }
    public GameObject getT2 { get => TMap2; }

    public GameObject getGrid { get => Grid; }
    

    private void Awake()
    {
        Grid = new GameObject("Grid for Tmap");
        TMap1 = new GameObject("TMap1");
        TMap2 = new GameObject("TMap2");

        TMap1.transform.SetParent(Grid.transform);
        TMap2.transform.SetParent(Grid.transform);

        tManager = GetComponent<TilesManager>();

        grid = Grid.AddComponent<Grid>();
        tMap1 = TMap1.AddComponent<Tilemap>();
        tMap2 = TMap2.AddComponent<Tilemap>();
        tMRenderer1 = TMap1.AddComponent<TilemapRenderer>();
        tMRenderer2 = TMap2.AddComponent<TilemapRenderer>();
        tMCollider1 = TMap1.AddComponent<TilemapCollider2D>();
        tMCollider2 = TMap2.AddComponent<TilemapCollider2D>();
        rb = Grid.AddComponent<Rigidbody2D>();
        controllerSubscription = Grid.AddComponent<ControllerEventSubscriber>();

        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        Physics2D.IgnoreCollision(tMCollider1, tMCollider2);

        TMap1.layer = LayerMask.NameToLayer("Ground");
        TMap2.layer = LayerMask.NameToLayer("Ground");

        TMap2.transform.localPosition = new Vector3(width, 0, 0);

        t1Manager = new TilemapContentManager(tMap1, tManager, width, height);
        t2Manager = new TilemapContentManager(tMap2, tManager, width, height);

        
    }


    // Start is called before the first frame update
    void Start()
    {
        /*
        GenerateDefaultTilemap(tMap1, 0, 0);
        GenerateDefaultTilemap(tMap2, 0, 0);

        //testing
        FillBreakableTest(tMap1, 0, 0);
        FillBreakableTest(tMap2, 0, 0);

        FillGravityUpTest(tMap1, 0, 0);
        */
        t1Manager.AddFloor();
        t1Manager.AddRandomPillar((int)width/2);
        t1Manager.AddCeiling();
        t2Manager.AddFloor();
        t2Manager.AddCeiling();
        t2Manager.AddRandomPillar();
        
    }

    // Update is called once per frame
    void Update()
    {
        ScrollTilemap();
        
    }


    void ScrollTilemap()
    {
        if(Grid.transform.position.x < -width && TMap1.transform.localPosition.x == 0)
        {
            Grid.transform.position = new Vector3(0, Grid.transform.position.y, Grid.transform.position.z);
            TMap2.transform.localPosition = new Vector3(TMap2.transform.localPosition.x - width, TMap2.transform.localPosition.y, TMap2.transform.localPosition.z);
            TMap1.transform.localPosition = new Vector3(TMap1.transform.localPosition.x + width, TMap1.transform.localPosition.y, TMap1.transform.localPosition.z);

            t1Manager.ResetContent();
            t1Manager.AddFloor();
            t1Manager.AddCeiling();
            t1Manager.AddRandomPillar();
        }

        if(Grid.transform.position.x < -width && TMap1.transform.localPosition.x != 0)
        {
            Grid.transform.position = new Vector3(0, Grid.transform.position.y, Grid.transform.position.z);
            TMap1.transform.localPosition = new Vector3(0, TMap1.transform.localPosition.y, TMap1.transform.localPosition.z);
            TMap2.transform.localPosition = new Vector3(width, TMap2.transform.localPosition.y, TMap2.transform.localPosition.z);

            t2Manager.ResetContent();
            t2Manager.AddFloor();
            t2Manager.AddCeiling();
            t2Manager.AddRandomPillar();
        }


    }


    void GenerateDefaultTilemap(Tilemap tilemap, int offsetX, int offsetY)
    {
        for(int i = -Mathf.RoundToInt(width/4); i < Mathf.RoundToInt(3*width/4); i++)
        {
            tilemap.SetTile(new Vector3Int(i + offsetX, -Mathf.RoundToInt(height / 2) + offsetY, 0), tManager.tiles[0]);
            tilemap.SetTile(new Vector3Int(i + offsetX, Mathf.RoundToInt(height / 2) - 1 + offsetY, 0), tManager.tiles[0]);

            //tilemap.transform.localPosition = new Vector3()
            //tilemap.SetTile(new Vector3Int(i, 1, 0), tManager.tiles[0]);
            //Debug.Log(i);
        }
    }

    void FillBreakableTest(Tilemap tilemap, int offsetX, int offsetY)
    {
        for (int i = -Mathf.RoundToInt(width / 4); i < Mathf.RoundToInt(3 * width / 4); i++)
        {
            tilemap.SetTile(new Vector3Int(i + offsetX, -Mathf.RoundToInt(height / 2) + offsetY + 2, 0), tManager.tiles[1]);
            tilemap.SetTile(new Vector3Int(i + offsetX, Mathf.RoundToInt(height / 2) - 1 + offsetY -2, 0), tManager.tiles[1]);

            //tilemap.transform.localPosition = new Vector3()
            //tilemap.SetTile(new Vector3Int(i, 1, 0), tManager.tiles[0]);
            //Debug.Log(i);
        }
    }

    void FillGravityUpTest(Tilemap tilemap, int offsetX, int offsetY)
    {
        for (int i = -Mathf.RoundToInt(width / 4); i < Mathf.RoundToInt(3 * width / 4); i++)
        {
            tilemap.SetTile(new Vector3Int(i + offsetX, -Mathf.RoundToInt(height / 2) + offsetY + 1, 0), tManager.tiles[2]);
            tilemap.SetTile(new Vector3Int(i + offsetX, Mathf.RoundToInt(height / 2) - 1 + offsetY - 1, 0), tManager.tiles[3]);

            //tilemap.transform.localPosition = new Vector3()
            //tilemap.SetTile(new Vector3Int(i, 1, 0), tManager.tiles[0]);
            //Debug.Log(i);
        }
    }







}
