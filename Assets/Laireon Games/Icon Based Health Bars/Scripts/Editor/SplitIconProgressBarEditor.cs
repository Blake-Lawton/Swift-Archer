using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

[CustomEditor(typeof(SplitIconProgressBar))]
public class SplitIconProgressBarEditor : IconProgressBarEditor
{
    SerializedProperty splitSpritePrefab, delayBetweenSegments;

    new void OnEnable()
    {
        splitSpritePrefab = serializedObject.FindProperty("splitSpritePrefab");
        delayBetweenSegments = serializedObject.FindProperty("delayBetweenSegments");

        base.OnEnable();
    }

    protected override void ShowMainImage()
    {
        EditorGUILayout.PropertyField(splitSpritePrefab, new GUIContent("Split Sprite Prefab"));
    }

    protected override void ShowDelay()
    {
        EditorGUILayout.PropertyField(delayBetweenSegments);
    }
}
