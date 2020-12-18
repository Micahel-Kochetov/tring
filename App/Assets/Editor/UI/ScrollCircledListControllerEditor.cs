using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ScrollCircledListController))]
public class ScrollCircledListControllerEditor : Editor
{
    private ScrollCircledListController data;

    private int index;

    private void OnEnable()
    {
        data = (ScrollCircledListController)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (Application.isPlaying)
        {
            DrawTestFunctionality();
        }
        else
        {
            DrawFastSetupGUI();
        }
    }

    private void DrawTestFunctionality()
    {
        GUILayout.Space(10);
        GUILayout.BeginVertical("box");
        GUILayout.Label("Test functionality:");
        GUILayout.BeginHorizontal();

        index = EditorGUILayout.IntField(index);
        if (GUILayout.Button("Set"))
        {
            data.ForceSelectIndex(index);
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
    }

    private void DrawFastSetupGUI()
    {
        GUILayout.Space(10);

        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("Fast config helper", EditorStyles.boldLabel);
        GUILayout.Space(5);
        if (GUILayout.Button("Configure List with Rings Sprites"))
        {
            data.UpdateList();
            return;
        }
        EditorGUILayout.EndVertical();
    }
}
