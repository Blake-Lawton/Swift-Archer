using UnityEngine;
using System.Collections;
using UnityEditor;
using System;

[CustomEditor(typeof(IconProgressBar))]
public class IconProgressBarEditor : Editor
{
    protected SerializedProperty backingImage, mainImage;
    IconProgressBar current;

    protected void OnEnable()
    {
        backingImage = serializedObject.FindProperty("backingImage");
        mainImage = serializedObject.FindProperty("mainImage");
    }

    public override void OnInspectorGUI()
    {
        #region Initialise
        serializedObject.Update();
        current = (IconProgressBar)target;
        #endregion

        GUILayout.Space(5);

        EditorGUILayout.PropertyField(backingImage, new GUIContent("Backing Image"));

        ShowMainImage();

        if(current.SetupFinished())
        {
            current.MaxIcons = EditorGUILayout.IntField(new GUIContent("Max Icons"), current.MaxIcons);
            current.Padding = EditorGUILayout.FloatField(new GUIContent("Padding"), current.Padding);

            GUILayout.Space(10);

            current.maxValue = EditorGUILayout.FloatField(new GUIContent("Max Value"), current.maxValue);
            current.CurrentValue = EditorGUILayout.FloatField(new GUIContent("Current Value"), current.CurrentValue);

            ShowDelay();

            GUILayout.Space(10);

            if(current.GetType().Equals(typeof(IconProgressBar)))//don't bother asking to show fractions for the split progress bars
                current.showFractions = EditorGUILayout.Toggle(new GUIContent("Show Fractions", "If true then you will see the last icon being clipped. Otherwise only shows full icons"), current.showFractions);

            if(Application.isEditor)
                if(current.HasInactiveImages())
                    if(GUILayout.Button("Clear Un-Used Images"))
                        current.ClearInactive();
        }

        if(GUI.changed)
            EditorUtility.SetDirty(target);

        serializedObject.ApplyModifiedProperties();// Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
    }

    protected virtual void ShowMainImage()
    {
        EditorGUILayout.PropertyField(mainImage, new GUIContent("Main Image"));
    }

    protected virtual void ShowDelay()
    {
    }
}
