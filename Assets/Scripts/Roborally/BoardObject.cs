using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[ExecuteInEditMode]
public class BoardObject : MonoBehaviour {
	public string _mapName;
	public Board _theBoard;
	public BoardOrientation _theBoardOrientation;
	void LoadFromJSON ( string jsonfile ) {
		TextAsset bindata= Resources.Load(jsonfile) as TextAsset;
		JSONObject j = new JSONObject(bindata.text);
		_theBoard = new Board (j);
		switch (_theBoardOrientation) {
		case BoardOrientation.CW:
			_theBoard.RotateCW ();
			break;
		case BoardOrientation.HalfTurn:
			_theBoard.RotateCW ();
			_theBoard.RotateCW ();
			break;
		case BoardOrientation.CCW:
			_theBoard.RotateCCW ();
			break;
		}
		for (uint y = 1; y <= _theBoard.Height; y++) {
			for (uint x = 1; x <= _theBoard.Width; x++) {
				GameObject go = Resources.Load <GameObject> ("Tileset/"+_theBoard [y, x].ElementType);
				GameObject tile = Instantiate(go);
				tile.name = "Tile [" + x + "," + y + "]";
				tile.GetComponent<TileObject> ()._element = _theBoard [y, x];
				tile.transform.parent = this.transform;
				switch (_theBoard [y, x].Facing) {
				case Direction.North:
					break;
				case Direction.East:
					tile.transform.FindChild ("default").Rotate (0, 90, 0);
					break;
				case Direction.South:
					tile.transform.FindChild ("default").Rotate (0, 180, 0);
					break;
				case Direction.West:
					tile.transform.FindChild ("default").Rotate (0, 270, 0);
					break;
				}
				if (_theBoard [y, x].Walls > 0) {
					string name = ("Wall-" + Reverse (System.Convert.ToString ((int)(_theBoard [y, x].Walls), 2).PadLeft (4, '0')));
					GameObject wgo = Resources.Load <GameObject> ("Tileset/"+name);
					GameObject wtile = Instantiate(wgo);
					wtile.name = "walls";
					wtile.transform.parent = tile.transform;
				}
				tile.transform.localPosition = new Vector3 (x-0.5f-_theBoard.Width/2, 0, y-0.5f-_theBoard.Height/2);
			}
		}
	}
	public string Reverse(string text)
	{
		if (text == null) return null;

		// this was posted by petebob as well 
		char[] array = text.ToCharArray();
		Array.Reverse(array);
		return new String(array);
	}
	public void Initialize () {
		LoadFromJSON (_mapName+".board");
	}
	public void Clear () {
		List<Transform> l = new List<Transform> ();
		foreach(Transform child in transform) {
			//child.parent = null;
			l.Add (child);
		}
		while (l.Count > 0) {
			Transform child = l [0];
			l.RemoveAt (0);
			DestroyImmediate(child.gameObject);
		}
		_theBoard = null;
	}
}
