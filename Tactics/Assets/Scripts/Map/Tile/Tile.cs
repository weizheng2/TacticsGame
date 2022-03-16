using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Height value is represented as how many stepHeights it has, stepHeight * height
    public const float stepHeight = 0.25f;
    public Point pos;
    public int height;

    // Get the center position so we can place other objects on top of the tile
    // pos.y represents the Z axis
    public Vector3 CenterPos { get { return new Vector3(pos.x, height * stepHeight, pos.y); } }
    
    public Tile parentTile;
    public int distance;

    public Color normalColor = Color.white;
    public Color selectedColor = Color.green;
    [SerializeField] Renderer renderel;

    // The unit/prop that is in this tile
    public GameObject content; 

    public void SetSelectedColor(bool selected)
    {
        if(selected)
            renderel.material.color = Color.green;
        else
            renderel.material.color = Color.white;
    }

    void UpdateTransform()
    {
        transform.localPosition = new Vector3(pos.x, height * stepHeight / 2f, pos.y);
        transform.localScale = new Vector3(1, height * stepHeight, 1);
    }

    public void Grow()
    {
        height++;
        UpdateTransform();
    }

    public void Shrink()
    {
        height--;
        UpdateTransform();
    }

    public void Load(Point p, int h)
    {
        pos = p;
        height = h;
        UpdateTransform();
    }

    public void Load(Vector3 v)
    {
        Load(new Point((int)v.x, (int)v.z), (int)v.y);
    }

}
