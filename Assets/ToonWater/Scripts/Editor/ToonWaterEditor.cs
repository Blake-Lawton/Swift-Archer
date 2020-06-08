using UnityEngine;
using System;
using UnityEditor;
//	Copyright 2013 Unluck Software	
//	www.chemicalbliss.com																									

[CustomEditor(typeof(ToonWater))]

[System.Serializable]
public class ToonWaterEditor : Editor {
	public bool defaultInspector;
	public override void OnInspectorGUI() {
		var target_cs = (ToonWater)target;
		Texture tex = (Texture)Resources.Load("ToonWaterTitle");
		GUILayout.Label(tex);
		if (GUILayout.Button("Toggle Editor")) {
			defaultInspector = !defaultInspector;
		}
		if (defaultInspector) {
			DrawDefaultInspector();
		} else {

			target_cs.ripplePS = (GameObject)EditorGUILayout.ObjectField("Ripple Particle System", target_cs.ripplePS, typeof(GameObject), false);
			target_cs.splashPS = (GameObject)EditorGUILayout.ObjectField("Splash Particle System", target_cs.splashPS, typeof(GameObject), false);
			target_cs.tileMaterial1 = EditorGUILayout.Slider("Material 1 Tile", target_cs.tileMaterial1, 1.0f, 100.0f);

			Renderer renderer = target_cs.gameObject.GetComponent<Renderer>();

			if (renderer.sharedMaterials.Length == 2) {
				target_cs.tileMaterial2 = EditorGUILayout.Slider("Material 2 Tile", target_cs.tileMaterial2, 1.0f, 100.0f);
			}


			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Automaticly Add Floating Script To Objects that fall in the water");

			target_cs.autoAddFloat = EditorGUILayout.Toggle("Auto Float", target_cs.autoAddFloat);

			EditorGUILayout.Space();
			EditorGUILayout.LabelField("How much water flow force applied to floating objects");
			target_cs.currentMultiplier = EditorGUILayout.Slider("Flow Multiplier", target_cs.currentMultiplier, 0.0f, 1.0f);

			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Max flow speed (randomized)");
			target_cs.maxCurrent = EditorGUILayout.Slider("Flow Speed", target_cs.maxCurrent, 0.0f, 25.0f);

			EditorGUILayout.Space();
			EditorGUILayout.LabelField("Seconds to wait before changing flow direction");
			target_cs._randomizeCurrent = (int)EditorGUILayout.Slider("Flow Timer", (float)target_cs._randomizeCurrent, 0.0f, 60.0f);

			EditorGUILayout.Space();
			target_cs.wave = EditorGUILayout.Toggle("Enable Waves", target_cs.wave);

			if (target_cs.wave) {
				EditorGUILayout.Space();
				EditorGUILayout.LabelField("Height movement of waves bobing up and down");
				target_cs.waveForceHeight = EditorGUILayout.Slider("Wave Height", target_cs.waveForceHeight, 0.0f, 5.0f);

				EditorGUILayout.Space();
				EditorGUILayout.LabelField("Height scaling of waves bobing up and down");
				target_cs.waveScale = EditorGUILayout.Slider("Wave Scale Multiplier", target_cs.waveScale, 0.0f, 1.0f);

				EditorGUILayout.Space();
				EditorGUILayout.LabelField("Speed of waves bobing up and down");
				target_cs.waveForceSpeed = EditorGUILayout.Slider("Wave Speed", target_cs.waveForceSpeed, 0.1f, 10.0f);
			}

		}
		if (GUI.changed)
			EditorUtility.SetDirty(target_cs);
	}
}