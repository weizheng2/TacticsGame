using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkMovement : Movement
{
    protected override bool ExpandSearch(Tile from, Tile to)
    {
        // Skip if the tile is occupied by an enemy
        // We are checking if the content is null here even though FilterSearch is doing the same
        // Because this function returns a path or if we can go from one tile to another
        // And FilterSearch is just to check what tiles we can stop or finish at.
        if (to.content != null)
            return false;

        return ((from.distance + 1) <= range && Mathf.Abs(from.height - to.height) <= jumpHeight);

    }

    protected override List<Tile> FilterSearch(List<Tile> tiles)
    {
        List<Tile> newTiles = new List<Tile>();
        foreach (Tile tile in tiles)
        {
            if (tile.content == null)
                newTiles.Add(tile);
        }
        return newTiles;
    }

    public override IEnumerator Move(Tile targetTile)
    {
        List<Tile> pathTiles = new List<Tile>();

        // Fill path of tiles
        while (targetTile != null)
        {
            // First in last out, could also use a Stack
            pathTiles.Insert(0, targetTile);
            targetTile = targetTile.parentTile;
        }

        // For every next tile, turn if necessary and walk/jump
        for (int i = 1; i < pathTiles.Count; ++i)
        {
            Tile from = pathTiles[i - 1];
            Tile to = pathTiles[i];

            Directions dir = from.GetDirection(to);
            if (dir != unit.direction)
                Turn(dir);

            //if (from.height == to.height)
                //yield return StartCoroutine(Walk(to));
            //else
               // yield return StartCoroutine(Jump(to));
        }


        yield return null;
    }

    LTDescr moveTween;
    public IEnumerator Walk(Tile targetTile)
    {
        moveTween = LeanTween.move(unit.gameObject, targetTile.CenterPos, 1f).setEase(LeanTweenType.easeInQuad).setOnComplete(OnCompleteStep);
        
        while (moveTween != null)
            yield return null;
    }

    void OnCompleteStep() => moveTween = null;


    public IEnumerator Jump(Tile targetTile)
    {
        moveTween = LeanTween.move(unit.gameObject, targetTile.CenterPos, 1f).setEase(LeanTweenType.easeInQuad).setOnComplete(OnCompleteStep);

        while (moveTween != null)
            yield return null;
    }
}
