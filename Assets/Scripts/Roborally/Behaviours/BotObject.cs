using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BotObject : MonoBehaviour {
	public BoardObject _onBoard;
	public BoardPosition _onTile;
	public Direction _facing;
	public Bot _bot;
	void Update () {
		uint x = _bot.Tile.Position.x;
		uint y = _bot.Tile.Position.y;
		transform.position = _onBoard.transform.localToWorldMatrix.MultiplyPoint (new Vector3 (x - 0.5f - _onBoard._theBoard.Width / 2, 0, y - 0.5f - _onBoard._theBoard.Height / 2));
		//_bot._board = _onBoard._theBoard;
		//_bot._position = _onTile._element.Position;
		switch (_bot._facing) {
		case Direction.North:
			transform.localEulerAngles = new Vector3 (0, 0, 0);
			break;
		case Direction.East:
			transform.localEulerAngles = new Vector3 (0, 90, 0);
			break;
		case Direction.South:
			transform.localEulerAngles = new Vector3 (0, 180, 0);
			break;
		case Direction.West:
			transform.localEulerAngles = new Vector3 (0, 270, 0);
			break;
		}
	}
	void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawCube (transform.position+new Vector3(0,0.5f,0), new Vector3 (0.5f, 0.5f, 0.5f));
		Gizmos.DrawLine (transform.localToWorldMatrix.MultiplyPoint3x4(new Vector3 (0, 0.5f, 0)), transform.localToWorldMatrix.MultiplyPoint3x4(new Vector3 (0, 0.5f, 1)));
	}
}
