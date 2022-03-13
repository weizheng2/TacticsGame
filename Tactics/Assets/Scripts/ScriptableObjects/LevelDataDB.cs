using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Scripts/ScriptableObjects/LevelData", order = 1)]
public class LevelDataDB : ScriptableObject
{
    public List<Vector3> tiles;
}