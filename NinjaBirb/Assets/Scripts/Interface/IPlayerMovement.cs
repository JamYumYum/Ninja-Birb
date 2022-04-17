using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerMovement
{
    float dashDuration { get; set;}
    bool isDashing { get; set; }
    void Dash(Vector2 direction, float intensity);


}
