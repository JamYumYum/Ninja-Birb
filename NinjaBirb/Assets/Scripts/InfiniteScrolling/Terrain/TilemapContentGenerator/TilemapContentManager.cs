using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapContentManager
{
    private List<List<Vector3Int>> tilePosition;
    private List<List<Tile>> tiles;
    private int width;
    private int height;
    private Tilemap tilemap;
    private TilesManager tilesManager;

    //borders of the generated tilemaps 
    private int left;
    private int right;
    private int up;
    private int down;

    //random range numbers
    //Pillar
    public int minPillarHeight = 2;
    public int maxPillarHeight = 6;
    public int minPillarWidth = 2;
    public int maxPillarWidth = 5;
    public int minPillarGap = 4;
    public int maxPillarGap = 7;

    public TilemapContentManager(Tilemap tilemap, TilesManager tilesManager, int width, int height)
    {
        this.width = width;
        this.height = height;
        this.tilemap = tilemap;
        this.tilesManager = tilesManager;

        tilePosition = new List<List<Vector3Int>>();
        tiles = new List<List<Tile>>();

        left = -Mathf.RoundToInt(width / 4);
        right = Mathf.RoundToInt(3 * width / 4) -1;
        down = -Mathf.RoundToInt(height / 2);
        up = Mathf.RoundToInt(height / 2) - 1;

        for(int i = 0; i < width; i++)
        {
            List<Vector3Int> currentX = new List<Vector3Int>();
            List<Tile> currentXTile = new List<Tile>();
            for(int j = 0; j < height; j++)
            {
                currentX.Add(new Vector3Int(left+i, down+j,0));
                currentXTile.Add(null);
            }
            tilePosition.Add(new List<Vector3Int>(currentX));
            tiles.Add(new List<Tile>(currentXTile));
        }
    }
    public void GenerateContent()
    {

    }

    public void AddFloor()
    {
        List<Tile> normalTiles = tilesManager.GetTilesOfType(typeof(Tile));
        if (normalTiles.Count == 0) return;

        List<Vector3Int> position = new List<Vector3Int>();
        List<Tile> newTiles = new List<Tile>();

        for(int i = 0; i<width; i++)
        {
            if(tiles[i][0] != normalTiles[0])
            {
                tiles[i][0] = normalTiles[0];
                position.Add(new Vector3Int(left + i, down, 0));
                newTiles.Add(normalTiles[0]);
            }
        }

        tilemap.SetTiles(position.ToArray(), newTiles.ToArray());


    }

    public void AddCeiling()
    {
        List<Tile> normalTiles = tilesManager.GetTilesOfType(typeof(Tile));
        if (normalTiles.Count == 0) return;

        List<Vector3Int> position = new List<Vector3Int>();
        List<Tile> newTiles = new List<Tile>();

        for (int i = 0; i < width; i++)
        {
            if (tiles[i][height - 1] != normalTiles[0])
            {
                tiles[i][height - 1] = normalTiles[0];
                position.Add(new Vector3Int(left + i, up, 0));
                newTiles.Add(normalTiles[0]);
            }
        }

        tilemap.SetTiles(position.ToArray(), newTiles.ToArray());

    }

    public void AddRandomPillar(int startLate = 0, int endEarly = 0)
    {
        int randomPillarHeight = 0;
        int randomPillarWidth = 0;
        List<Tile> normalTiles = tilesManager.GetTilesOfType(typeof(Tile));
        if (normalTiles.Count == 0) return;

        List<Vector3Int> position = new List<Vector3Int>();
        List<Tile> newTiles = new List<Tile>();

        for(int i = startLate; i < width-endEarly; i++)
        {
            if(randomPillarWidth <= 0)
            {
                randomPillarWidth = Random.Range(minPillarWidth,maxPillarWidth+1);
                randomPillarHeight = Random.Range(minPillarHeight, maxPillarHeight + 1);
                i += Random.Range(minPillarGap, maxPillarGap+1);
            }
            else
            {
                randomPillarWidth -= 1;

                for(int j = 0; j < randomPillarHeight; j++)
                {
                    tiles[i][j+1] = normalTiles[0];
                    position.Add(new Vector3Int(left + i, down + 1 + j, 0));
                    newTiles.Add(normalTiles[0]);
                }
            }
            

        }
        tilemap.SetTiles(position.ToArray(), newTiles.ToArray());

    }

    public void ResetContent()
    {
        List<Vector3Int> position = new List<Vector3Int>();
        List<Tile> newTiles = new List<Tile>();

        for (int i=0; i< tiles.Count; i++)
        {
            for(int j=0; j< tiles[i].Count; j++)
            {
                if(tiles[i][j] != null)
                {
                    tiles[i][j] = null;
                    position.Add(tilePosition[i][j]);
                    newTiles.Add(null);

                }
            }
        }

        tilemap.SetTiles(position.ToArray(), newTiles.ToArray());

    }

    
    
}
