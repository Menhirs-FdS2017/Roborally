using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bot : GameElement
{
	public Direction _facing;
	public BoardPosition _position;
	public uint _hits;
	public string _name;

	public BoardElement Tile {
		get { return Container as BoardElement; }
		set { Container = value; }
	}

	public Board Board {
		get { if (Tile == null)
				return null;
			return Tile.Board;
		}
	}

	public Bot(Board b, BoardPosition p, Direction f) {
		//_board = b;
		_position = p;
		Tile = b [_position];
		_facing = f;
	}
	public void RotateLeft () {
		_facing = _facing.RotateCCW ();
	}
	public void RotateRight () {
		_facing = _facing.RotateCW ();
	}
	public void HalfTurn () {
		_facing = _facing.RotateCCW ();
		_facing = _facing.RotateCCW ();
	}
	public void Forward () {
		if (Tile==null) return;
		if (Tile.CanExit (this, _facing)) {
			Board [_position, _facing].Enters (this, _facing.Opposite ());
		}
	}
	public void Backward () {
		if (Tile==null) return;
		if (Tile.CanExit (this, _facing.Opposite())) {
			Board [_position, _facing.Opposite ()].Enters (this, _facing);
		}
	}
}
