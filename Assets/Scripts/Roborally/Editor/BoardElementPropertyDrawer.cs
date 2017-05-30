using UnityEditor;
using UnityEngine;

/*
[CustomPropertyDrawer(typeof(BoardElement), true)]
public class BoardElementPropertyDrawer : PropertyDrawer {
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		Debug.Log ("BoardElement");
		//base.OnGUI (position, property, label);
		//position.height = 16;
		//EditorGUI.PropertyField(position, property.FindPropertyRelative("ElementType"),new GUIContent("Type"));  position.y+=16;
		//EditorGUI.PropertyField(position, property.FindPropertyRelative("Position"),new GUIContent("Position"));  position.y+=16; 

		SerializedProperty p = property.GetEndProperty ();
		SerializedProperty c = property.Copy ();
		while (property != p) {
			Debug.Log (property.objectReferenceValue.ToString ());
			if (!c.Next (true)) break;
		}
	}
	public override float GetPropertyHeight (SerializedProperty prop,GUIContent label) {
		return 4 * 16;
	}
}

[CustomPropertyDrawer(typeof(Gear), true)]
public class GearPropertyDrawer : PropertyDrawer {
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		Debug.Log ("Gear");
		//base.OnGUI (position, property, label);
	}
}
*/
