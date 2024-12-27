using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TableData), true)]
public class TableDataEditor : Editor
{
    TableData t = null;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        t = (TableData)target;
        if (GUILayout.Button("Load Table"))
        {
            //t.LoadTable();
        }
    }
}
