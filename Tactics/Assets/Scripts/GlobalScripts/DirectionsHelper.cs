using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DirectionsHelper
{
    // Get direction of t2 based of t1
    // Directions d = t1.GetDirection(t2);
    public static Directions GetDirection(this Tile t1, Tile t2)
    {
        //pos.y = Z axis
        if (t1.pos.y < t2.pos.y)
            return Directions.NORTH;
        if (t1.pos.y > t2.pos.y)
            return Directions.SOUTH;
        if (t1.pos.x < t2.pos.x)
            return Directions.EAST;
        return Directions.WEST;
    }

    // Direction to Vector3
    // NORTH 0, EAST 90, SOUTH 180, WEST 270
    public static Vector3 ToEuler(this Directions d)
    {
        return new Vector3(0, (int)d * 90, 0);
    }
}
