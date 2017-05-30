using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class BoardEditorWindow : EditorWindow
{
	// Add menu item named "My Window" to the Window menu
	[MenuItem("Roborally/Board Editor")]
	public static void ShowWindow()
	{
		EditorWindow.GetWindow(typeof(BoardEditorWindow));
	}
}
