using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoardObject))]
public class BoardInspectorWindow : Editor {
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		BoardObject myScript = (BoardObject)target;
		if(GUILayout.Button("Build Object"))
		{
			myScript.Initialize();
		}
		if(GUILayout.Button("Clear Object"))
		{
			myScript.Clear();
		}
	}
}
