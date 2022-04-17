using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public interface ISpecialCollidingTile
{
    void OnCollisionAction(Collision2D collision, Tilemap tilemap, Vector3Int position);
}
