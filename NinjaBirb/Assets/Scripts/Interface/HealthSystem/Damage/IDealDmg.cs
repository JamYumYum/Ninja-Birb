using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDealDmg<T>
{
    T damageValue { get; set; }
    List<DamageType.Type> types { get; set; }
    void DealDmg(IHealth<T> target, T dmg);
}   
    