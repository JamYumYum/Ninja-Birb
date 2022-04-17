using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth<int>, ITakeDmg
{
    [SerializeField]
    private int Health = 3;
    [SerializeField]
    private int MaxHealth = 3;
    [SerializeField]
    private List<DamageType.Type> InvulnerableTo = new List<DamageType.Type>();

    private bool RecentlyDmgTaken = false;
    private float DmgCooldown = 0.2f;

    public int health => Health; 
    public int maxHealth { get => MaxHealth; set => MaxHealth = value; }
    public List<DamageType.Type> invulnerableTo { get => InvulnerableTo; set => InvulnerableTo = value; }

    public bool recentlyDmgTaken => RecentlyDmgTaken;

    public float dmgCooldown { get => DmgCooldown; set => DmgCooldown = value; }

    public void AddHealth(int toAdd)
    {
        if(Health + toAdd < MaxHealth)
        {
            Health += toAdd;
        }
        else
        {
            //overheal
            Health = MaxHealth;
        }
    }


    public void SubHealth(int toSub)
    {
        if (RecentlyDmgTaken) return;

        StartCoroutine("IgnoreDamage");

        if(Health - toSub > 0)
        {
            Health -= toSub;
        }
        else
        {
            //overkill and dead
            Health = 0;
        }
    }
    public void SetHealth(int newValue)
    {
        if(newValue < 0)
        {
            Health = 0;
        }
        else if(newValue < MaxHealth)
        {
            Health = newValue;
        }
        else
        {
            Health = MaxHealth;
        }
    }

    public void SetMaxHealth(int newMax)
    {
        if(Health > newMax)
        {
            Health = newMax;
        }
        MaxHealth = newMax;
    }

    IEnumerator IgnoreDamage()
    {
        RecentlyDmgTaken = true;
        yield return new WaitForSeconds(DmgCooldown);
        RecentlyDmgTaken = false;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
