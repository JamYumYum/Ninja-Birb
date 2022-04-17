using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TriggerSpecialTiles : MonoBehaviour
{
    private EndlessTerrain endless;
    private GameObject t1;
    private GameObject t2;
    private Tilemap tilemap1;
    private Tilemap tilemap2;
    private CharacterController controller;

    //SpecialSpaceTile, which the player currently/last time is/was in
    private ISpecialSpaceTile currentTile;
    private ISpecialSpaceTile lastTile;

    // Start is called before the first frame update
    void Start()
    {
        endless = FindObjectOfType<EndlessTerrain>();
        if(endless != null)
        {
            t1 = endless.getT1;
            t2 = endless.getT2;

            tilemap1 = endless.getTmap1;
            tilemap2 = endless.getTmap2;

        }

        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForSpecialSpaceTile();
    }
    void CheckForSpecialSpaceTile()
    {
        ISpecialSpaceTile tileInT1 = tilemap1.GetTile(tilemap1.WorldToCell(Vector3Int.zero)) as ISpecialSpaceTile;
        ISpecialSpaceTile tileInT2 = tilemap2.GetTile(tilemap2.WorldToCell(Vector3Int.zero)) as ISpecialSpaceTile;
        lastTile = currentTile;
        if (tileInT1 != null)
        {
            currentTile = tileInT1;
        }
        else if (tileInT2 != null)
        {
            currentTile = tileInT2;
        }
        else
        {
            currentTile = null;
        }

        //??? -> specialTile
        if (currentTile != null)
        {
            //specialTile -> specialTile
            if (lastTile != null)
            {
                //specialTileA -> specialTileB
                if (currentTile.GetType() != lastTile.GetType())
                {
                    lastTile.OnExit(controller);
                    currentTile.OnEnter(controller);
                }
                //specialTileA -> specialTileA (else)
            }

            //null -> specialTile
            else
            {
                currentTile.OnEnter(controller);
            }

        }
        //??? -> null
        else
        {
            //specialTile -> null
            if (lastTile != null)
            {
                lastTile.OnExit(controller);
            }
            //null -> null (else)
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Tilemap tilemap;
        Vector3 hitPosition = Vector3.zero;
        if (collision.GetContact(0).collider.gameObject == t1) tilemap = tilemap1;
        else if (collision.GetContact(0).collider.gameObject == t2) tilemap = tilemap2;
        else tilemap = null;

        if (tilemap != null)
        {
            for(int i = 0; i< collision.contactCount; i++)
            {
                ContactPoint2D hit = collision.GetContact(i);
                hitPosition.x = hit.point.x - 0.05f * hit.normal.normalized.x;
                hitPosition.y = hit.point.y - 0.05f * hit.normal.normalized.y;
                //tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
                Vector3Int pos = tilemap.WorldToCell(hitPosition);
                ISpecialCollidingTile tile = tilemap.GetTile(pos) as ISpecialCollidingTile;
                if(tile != null)
                {
                    tile.OnCollisionAction(collision, tilemap, pos);
                }
                
            }
        }
        
    }


}
