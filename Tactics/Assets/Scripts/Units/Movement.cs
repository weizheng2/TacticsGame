using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Movement : MonoBehaviour
{
    public int range;
    public int jumpHeight;
    protected Unit unit;
    protected Transform model;

    // Virtual methods have an implementation and provide the derived classes with the option of overriding it.
    // Abstract methods do not provide an implementation and force the derived classes to override the method.
    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
        model = transform.GetChild(0);
    }

    public virtual List<Tile> GetTilesInRange()
    {
        List<Tile> tilesInRange = Board.GetInstance().Search(unit.tile, ExpandSearch);
        return FilterSearch(tilesInRange);
    }
    
    protected virtual bool ExpandSearch(Tile from, Tile to)
    {
        return (from.distance + 1) <= range;
    }

    protected virtual List<Tile> FilterSearch(List<Tile> tiles)
    {
        // From the absolute list we get, remove the tiles that already have something in them (unit, prop, etc)
        foreach(Tile tile in tiles)
        {
            if(tile.content != null)
                tiles.Remove(tile);           
        }
        return tiles;
    }

    protected virtual void Turn(Directions direction)
    {
        LeanTween.rotate(unit.gameObject, direction.ToEuler(), 0.5f).setEase(LeanTweenType.easeInQuad);
        unit.direction = direction;
    }

    public abstract IEnumerator Move(Tile targetTile);
}
