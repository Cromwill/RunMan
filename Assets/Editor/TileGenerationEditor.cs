using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileGeneration))]
public class TileGenerationEditor : Editor
{
    protected virtual void OnSceneGUI()
    {
        var t = (TileGeneration)target;

        EditorGUI.BeginChangeCheck();


        for(int i = 0; i < t._notDestroyObjectPositions.Length; i++)
        {
            t._notDestroyObjectPositions[i] = Handles.PositionHandle(t.transform.position + t._notDestroyObjectPositions[i], Quaternion.identity) - t.transform.position;
        }

        for (int i = 0; i < t._destroyPositions.Length; i++)
        {
            t._destroyPositions[i] = Handles.PositionHandle(t.transform.position + t._destroyPositions[i], Quaternion.identity) - t.transform.position;
        }

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(t, "Change points");
        }
    }
}
