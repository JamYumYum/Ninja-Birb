using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollector<T>
{
    void Collect(ICollectable<T> collectable);
}
