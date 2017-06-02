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
public class BoardElement : GameElement
{
	[SerializeField]
	private string _elementType;
	public string ElementType { get { return _elementType; } set { _elementType = value; } }
	public BoardPosition Position;
	[EnumFlags] public Direction Walls;
	public Direction Facing;

	public Board Board {
		get { return Container as Board; }
		set { Container = value; }
	}

	public Bot Bot {
		get { return Contained as Bot; }
		set { Contained = value; }
	}

	public BoardElement () {
		ElementType = "None";
		Facing = Direction.North;
	}
	public BoardElement this[Direction direction]
	{
		get {
			if (Board == null)
				return null;
			return Board [Position, direction];
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
		if (Bot != null) {
			Board [Position, from.Opposite ()].Enters (Bot, from);
		}
		bot.Tile.Bot = null;
		bot._position = Position;
		bot.Tile = this;
		Bot = bot;
	}
	public bool CanEnter (Bot bot, Direction from)
	{
		if (Walls.IsSet(from))
			return false;
		if (Bot == null)
			return true;
		return CanExit (Bot, from.Opposite ());
	}
	public bool CanExit (Bot bot, Direction to)
	{
		if (Walls.IsSet(to))
			return false;
		return Board [Position, to].CanEnter(Bot,to.Opposite () );
	}
}
