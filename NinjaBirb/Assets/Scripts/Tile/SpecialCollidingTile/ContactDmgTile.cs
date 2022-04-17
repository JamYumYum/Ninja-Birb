using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "newContactDmgTile", menuName = "CustomTile/ContactDmgTile")]
public class ContactDmgTile : Tile, ISpecialCollidingTile, IDealDmg<int>
{
    public int damage = 1;
    public float knockBackForce = 100f;
    public bool usingKnockBack = true;
    public List<DamageType.Type> Types = new List<DamageType.Type>
    {
        DamageType.Type.TileDmg, 
        DamageType.Type.TerrainDmg
    };

    public int damageValue { get => damage; set => damage = value; }
    public List<DamageType.Type> types { get => Types; set => Types = value; }

    public void DealDmg(IHealth<int> target, int dmg)
    {
        target.SubHealth(dmg);
    }

    public void OnCollisionAction(Collision2D collision, Tilemap tilemap, Vector3Int position)
    {
        IHealth<int> target = collision.otherCollider.gameObject.GetComponent<IHealth<int>>();
        ITakeDmg targetChecklist = target as ITakeDmg;
        bool immuneToDmg = false;
        if(targetChecklist != null)
        {
            foreach(DamageType.Type Type in Types)
            {
                if (targetChecklist.invulnerableTo.Contains(Type))
                {
                    immuneToDmg = true;
                    break;
                }
            }
            if (!immuneToDmg)
            {
                DealDmg(target, damage);
            }
        }
    }
}
