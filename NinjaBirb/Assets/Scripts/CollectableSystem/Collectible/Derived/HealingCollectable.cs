using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingCollectable : Collectable<Collector>
{
    public int HealAmount = 1;
    public override void GettingCollectedAction(Collector collector)
    {
        collector.Health.AddHealth(HealAmount);
        DestroyCollectable();
    }

    public void DestroyCollectable()
    {
        Destroy(gameObject);
    }

}
