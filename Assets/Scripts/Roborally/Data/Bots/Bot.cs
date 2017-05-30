using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bot
{
	public Direction _facing;
	public BoardPosition _position;
	public Board _board;
	public BoardElement _tile;
	public uint _hits;
	public string _name;
	public Bot(Board b, BoardPosition p, Direction f) {
		_board = b;
		_position = p;
		_tile = _board [_position];
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
		//BoardElement _tile = _board [_position];
		if (_tile==null) return;
		if (_tile.CanExit (this, _facing)) {
			_board [_position, _facing].Enters (this, _facing.Opposite ());
		}
	}
	public void Backward () {
		//BoardElement _tile = _board [_position];
		if (_tile==null) return;
		if (_tile.CanExit (this, _facing.Opposite())) {
			_board [_position, _facing.Opposite ()].Enters (this, _facing);
		}
	}
}
