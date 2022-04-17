using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Collectable : MonoBehaviour{}
public abstract class Collectable<T> : Collectable, ICollectable<T>
{
    public abstract void GettingCollectedAction(T collector);
}
