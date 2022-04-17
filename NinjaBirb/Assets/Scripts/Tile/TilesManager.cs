using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilesManager : MonoBehaviour
{
    [Header("Tiles")]
    public List<Tile> tiles;

    public List<Tile> GetTilesOfType(System.Type type)
    {
        List<Tile> result = new List<Tile>();
        tiles.ForEach(t => 
        {
            if (t.GetType() == type) result.Add(t);
        }
        );

        return result;
    }

    public List<Tile> GetTilesDerivedOf(params System.Type[] types)
    {
        List<Tile> result = new List<Tile>();
        tiles.ForEach(t =>
        {
            foreach (System.Type type in types)
            {
                if (t.GetType().IsSubclassOf(type) || t.GetType() == type)
                {
                    result.Add(t);
                    break;
                }
            }
        }
        );

        return result;
    }

    
}
