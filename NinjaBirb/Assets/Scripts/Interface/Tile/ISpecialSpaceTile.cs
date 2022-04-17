using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpecialSpaceTile
{
    void OnEnter(CharacterController controller);
    void OnExit(CharacterController controller);
    
}
