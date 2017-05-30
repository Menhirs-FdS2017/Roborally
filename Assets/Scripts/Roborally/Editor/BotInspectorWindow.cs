using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BotObject))]
public class BotInspectorWindow : Editor {
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		BotObject myScript = (BotObject)target;
		if(GUILayout.Button("Initialize Object"))
		{
			myScript._bot = new Bot (myScript._onBoard._theBoard, myScript._onTile, myScript._facing);
			myScript._onBoard._theBoard[myScript._onTile].Contained = myScript._bot;
			EditorUtility.SetDirty (myScript);
		}
		if(GUILayout.Button("Rotate Left"))
		{
			myScript._bot.RotateLeft ();
			EditorUtility.SetDirty (myScript);
		}
		if(GUILayout.Button("Rotate Right"))
		{
			myScript._bot.RotateRight ();
			EditorUtility.SetDirty (myScript);
		}
		if(GUILayout.Button("Half Turn"))
		{
			myScript._bot.HalfTurn ();
			EditorUtility.SetDirty (myScript);
		}
		if(GUILayout.Button("Forward"))
		{
			myScript._bot.Forward ();
			EditorUtility.SetDirty (myScript);
		}
		if(GUILayout.Button("Backward"))
		{
			myScript._bot.Backward ();
			EditorUtility.SetDirty (myScript);
		}
	}
}
