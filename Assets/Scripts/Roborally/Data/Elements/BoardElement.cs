using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct BoardPosition {
	public uint x, y;
	public BoardPosition(uint xx, uint yy) {
		x = xx;
		y = yy;
	}
	public BoardPosition Neighboor(Direction direction) {
		BoardPosition result = this;
		switch (direction) {
		case Direction.North:
			result.y++;
			break;
		case Direction.East:
			result.x++;
			break;
		case Direction.South:
			result.y--;
			break;
		case Direction.West:
			result.x--;
			break;
		}
		return result;
	}
	public string ToString () {
		return "<" + x + "," + y + ">";
	}
}

public interface IBoardElement {}

[Serializable]
public class BoardElement
{
	[SerializeField]
	private string _elementType;
	public string ElementType { get { return _elementType; } set { _elementType = value; } }
	public Bot Contained;
	[HideInInspector] public Board Container;
	public BoardPosition Position;
	[EnumFlags] public Direction Walls;
	public Direction Facing;
	public BoardElement () {
		ElementType = "None";
		Facing = Direction.North;
	}
	public BoardElement this[Direction direction]
	{
		get {
			if (Container == null)
				return null;
			return Container [Position, direction];
		}
	}
	public void RotateCW () {
		Walls = Walls.RotateCW ();
		Facing = Facing.RotateCW ();
	}
	public void RotateCCW () {
		Walls = Walls.RotateCCW ();
		Facing = Facing.RotateCCW ();
	}
	public virtual void Activate ()
	{
	}
	public virtual void Enters (Bot bot, Direction from)
	{
		if (Contained != null) {
			Container [Position, from.Opposite ()].Enters (Contained, from);
		}
		bot._tile.Contained = null;
		bot._position = Position;
		bot._tile = this;
		Contained = bot;
	}
	public bool CanEnter (Bot bot, Direction from)
	{
		Debug.Log ("CanEnter : " + Position.ToString () + " in: " + from);
		if (Walls.IsSet(from))
			return false;
		if (Contained == null)
			return true;
		return CanExit (Contained, from.Opposite ());
	}
	public bool CanExit (Bot bot, Direction to)
	{
		Debug.Log ("CanExit : " + Position.ToString () + " out: " + to);
		if (Walls.IsSet(to))
			return false;
		return Container[Position, to].CanEnter(Contained,to.Opposite () );
	}
}
