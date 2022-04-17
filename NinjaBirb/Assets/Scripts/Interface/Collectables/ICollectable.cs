using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable<T>
{
    void GettingCollectedAction(T collector);

}
