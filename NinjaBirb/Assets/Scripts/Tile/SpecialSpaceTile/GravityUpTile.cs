using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "newGravityTile", menuName = "CustomTile/GravityUpTile")]
public class GravityUpTile : Tile, ISpecialSpaceTile
{
    

    public void OnEnter(CharacterController controller)
    {
        controller.SetGravityDirection(Vector2.up);
    }

    public void OnExit(CharacterController controller)
    {
        controller.SetGravityDirection(Vector2.down);
    }

}
