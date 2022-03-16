using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkMovement : Movement
{
    protected override List<Tile> FilterSearch(List<Tile> tiles)
    {
        foreach (Tile tile in tiles)
        {
            // Since we are walking, if the tile is too
            bool canJump = tile.parentTile && jumpHeight > Mathf.Abs(tile.height - tile.parentTile.height);

            if (tile.content != null || !canJump)
                tiles.Remove(tile);
        }
        return tiles;
    }
}
