using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakeDmg
{
    bool recentlyDmgTaken { get;}
    float dmgCooldown { get; set; }
    List<DamageType.Type> invulnerableTo { get; set; }
}
