using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BoardCreator : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    [SerializeField] Transform tileIndicator;
    [SerializeField] Transform tilesParent;

    // Limits
    [SerializeField] int width = 10; // x
    [SerializeField] int depth = 10; // z 
    [SerializeField] int height = 8; // height * Tile.stepHeight
    [SerializeField] Point selectedPos;
    [SerializeField] LevelDataDB levelData;

    // Point Tile so we can check if a pos has a tile, etc
    Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();

    // Random rect somewhere in the map
    Rect RandomRect()
    {
        int x = Random.Range(0, width);
        int y = Random.Range(0, depth);
        int w = Random.Range(1, width - x + 1);
        int h = Random.Range(1, depth - y + 1);
        return new Rect(x, y, w, h);
    }

    public void GrowArea()
    {
        Rect rect = RandomRect();

        for (int i = (int)rect.xMin; i < (int)rect.xMax; i++)
        {
            for (int j = (int)rect.yMin; j < (int)rect.yMax; j++)
            {
                Point p = new Point(i, j);
                GrowSingle(p);
            }
        }
    }

    public void ShrinkArea()
    {
        Rect rect = RandomRect();
        for (int i = (int)rect.xMin; i < (int)rect.xMax; i++)
        {
            for (int j = (int)rect.yMin; j < (int)rect.yMax; j++)
            {
                Point p = new Point(i, j);
                ShrinkSingle(p);
            }
        }
    }

    public void Grow() => GrowSingle(selectedPos);
    public void Shrink() => ShrinkSingle(selectedPos);

    void GrowSingle(Point pos)
    {
        Tile t = GetOrCreateTile(pos);

        // Dont exit map limits
        if (t.height < height) 
            t.Grow();
    }

    void ShrinkSingle(Point pos)
    {
        // Only shrink if Tile exists
        if (!tiles.ContainsKey(pos)) return;

        Tile t = tiles[pos];
        t.Shrink();

        if (t.height <= 0)
        {
            tiles.Remove(pos);
            DestroyImmediate(t.gameObject);
        }        
    }

    Tile GetOrCreateTile(Point pos)
    {
        if (tiles.ContainsKey(pos))
            return tiles[pos];

        Tile t = CreateTile();
        t.Load(pos, 0);
        tiles.Add(pos,t);

        return t;
    }

    Tile CreateTile()
    {
        GameObject instance = Instantiate(tilePrefab) as GameObject;
        instance.transform.parent = tilesParent;
        return instance.GetComponent<Tile>();
    }

    public void UpdateMarker()
    {
        Tile t = tiles.ContainsKey(selectedPos) ? tiles[selectedPos] : null;
        tileIndicator.localPosition = t != null ? t.CenterPos : new Vector3(selectedPos.x, 0, selectedPos.y);
    }

    public void Clear()
    { 
        for (int i = tilesParent.childCount - 1; i >= 0; --i)
            DestroyImmediate(tilesParent.GetChild(i).gameObject);
        tiles.Clear();
    }

    public void Save()
    {
        if (levelData == null) return;

        levelData.tiles = new List<Vector3>(tiles.Count);
        foreach (Tile t in tiles.Values)
            levelData.tiles.Add(new Vector3(t.pos.x, t.height, t.pos.y));
    }

    public void Load()
    {
        Clear();
        if (levelData == null) return;

        foreach (Vector3 v in levelData.tiles)
        {
            Tile t = CreateTile();
            t.Load(v);
            tiles.Add(t.pos, t);
        }
    } 
}
