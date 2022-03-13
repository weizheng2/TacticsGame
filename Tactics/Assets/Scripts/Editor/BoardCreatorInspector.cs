using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoardCreator))]
public class BoardCreatorInspector : Editor
{
    public BoardCreator boardCreator
    {
        get
        {
            return (BoardCreator)target;
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Grow"))
            boardCreator.Grow();
        if (GUILayout.Button("Shrink"))
            boardCreator.Shrink();
        if (GUILayout.Button("Grow Area"))
            boardCreator.GrowArea();
        if (GUILayout.Button("Shrink Area"))
            boardCreator.ShrinkArea();
        if (GUILayout.Button("Clear"))
            boardCreator.Clear();
        if (GUILayout.Button("Save"))
            boardCreator.Save();
        if (GUILayout.Button("Load"))
            boardCreator.Load();
        if (GUI.changed)
            boardCreator.UpdateMarker();
    }
}
