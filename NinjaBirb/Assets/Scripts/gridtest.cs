using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class gridtest : MonoBehaviour
{
    public Sprite sprite;
    GridLayout grid;
    Tile tile;
    Tilemap tilemap;
    
    // Start is called before the first frame update
    void Start()
    {
        grid = GetComponentInParent<GridLayout>();
        tile = ScriptableObject.CreateInstance<Tile>();
        tile.sprite = sprite;
        tilemap = GetComponent<Tilemap>();
        tilemap.SetTile(new Vector3Int(1, 0, 0), tile);
        //Debug.Log(tilemap.GetTile(new Vector3Int(1, 0, 0)));

        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(grid.WorldToCell(Vector3.zero - transform.position));
        //Debug.Log((Vector3.zero - transform.position));
    }
}
