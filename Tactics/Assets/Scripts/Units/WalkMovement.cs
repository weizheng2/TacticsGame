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
}
