using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "newBreakable", menuName = "CustomTile/BreakableTile")]
public class BreakableTile : Tile, ISpecialCollidingTile
{
    public void OnCollisionAction(Collision2D collision, Tilemap tilemap, Vector3Int position)
    {
        tilemap.SetTile(position, null);
    }

    
}
