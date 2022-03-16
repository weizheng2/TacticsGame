using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [field: SerializeField]
    public Tile tile { get; protected set; }
    public Directions direction;
    public Movement movement;

    public void SetPlace(Tile target)
    {
        // Remove old tile reference to this unit
        if (tile != null && tile.content == this.gameObject)
            tile.content = null;

        // Link unit and new tile references
        tile = target;
        if (target != null)
            target.content = this.gameObject;

        UpdateTransform();
    }

    public void UpdateTransform()
    {
        transform.localPosition = tile.CenterPos;
        transform.localEulerAngles = direction.ToEuler();
    }
}
