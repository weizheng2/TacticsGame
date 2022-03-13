using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    Queue<Tile> currentQueue = new Queue<Tile>();
    Queue<Tile> nextQueue = new Queue<Tile>();

    [SerializeField] GameObject tilePrefab;
    public Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();

    [SerializeField] LevelDataDB levelData;


    [ContextMenu("ClearTiles")]
    public void ClearTiles() 
    {
        tiles.Clear();
    }

    [ContextMenu("LoadBoardData")]
    public void LoadBoardData()//(LevelDataDB data)
    {
        for (int i = 0; i < levelData.tiles.Count; ++i)
        {
            GameObject instance = Instantiate(tilePrefab) as GameObject;
            instance.transform.parent = this.transform;
            instance.name = levelData.tiles[i].x + "," + levelData.tiles[i].z;

            Tile t = instance.GetComponent<Tile>();
            t.Load(levelData.tiles[i]);
            tiles.Add(t.pos, t);
        }
    }

    // After every search the tiles need to be reseted
    public void RestartTiles()
    {
        foreach (var item in tiles.Values)
        {
            item.distance = int.MaxValue;
            item.parentTile = null;
        }
    }

    Point[] directions =
    {
        new Point(-1,0), // North
        new Point(0,1), // East
        new Point(1,0), // South
        new Point(0,-1) // West
    };

    public List<Tile> Search(Tile initTile, int distance)
    {
        RestartTiles();

        List<Tile> posibleTiles = new List<Tile>();

        // First tile
        initTile.distance = 0;
        initTile.parentTile = null;
        posibleTiles.Add(initTile);

        currentQueue.Enqueue(initTile);

        while(currentQueue.Count > 0)
        {
            Tile currentTile = currentQueue.Dequeue();

            // Add the 4 surrounding Tiles
            for (int i = 0; i < 4; i++)
            {
                tiles.TryGetValue(currentTile.pos + directions[i], out Tile nextTile);

                if (nextTile != null && currentTile.distance + 1 <= distance && nextTile.parentTile == null) 
                {
                    nextTile.distance = currentTile.distance + 1;
                    nextTile.parentTile = currentTile;

                    posibleTiles.Add(nextTile);
                    nextQueue.Enqueue(nextTile);
                }
            }

            if(currentQueue.Count == 0)
                SwapRef(ref currentQueue, ref nextQueue);
        }


        foreach (Tile auxTile in posibleTiles)
        {
            auxTile.SetSelectedColor(true);
            Debug.LogError(auxTile.name);
        }

        return posibleTiles;
    }

    // This method now changes its arguments. 
    public void SwapRef(ref Queue<Tile> ob1, ref Queue<Tile> ob2)
    {
        Queue<Tile> t;

        t = ob1;
        ob1 = ob2;
        ob2 = t;
    }
}
