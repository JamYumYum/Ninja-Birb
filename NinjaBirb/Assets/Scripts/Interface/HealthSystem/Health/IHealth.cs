using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth<T>
{
    T health { get;}
    T maxHealth { get;}
    void AddHealth(T toAdd);
    void SubHealth(T toSub);
    void SetHealth(T newValue);
    void SetMaxHealth(T newMax);
}
