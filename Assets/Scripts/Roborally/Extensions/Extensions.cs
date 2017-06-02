using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public static class EnumExtensions
{
	public static Direction Opposite(this Direction direction)
	{
		switch (direction) {
		case Direction.North:
			return Direction.South;
		case Direction.East:
			return Direction.West;
		case Direction.South:
			return Direction.North;
		case Direction.West:
			return Direction.East;
		}
		return 0;
	}
	public static bool IsSet(this Direction flags, Direction flag)
	{
		return (flags & flag) != 0;
	}
	public static Direction RotateCCW(this Direction value)
	{
		byte b = (byte)value;
		byte b1 = (byte)(b >> 1);
		byte b2 = (byte)((b << 3)&0x0F);
		return (Direction)(b1|b2);
	}
	public static Direction RotateCW(this Direction value)
	{
		byte b = (byte)value;
		byte b1 = (byte)((b << 1)&0x0F);
		byte b2 = (byte)(b >> 3);
		return (Direction)(b1|b2);
	}
}

public static class DirectionParser {
	public static Direction FromString(string flag)
	{
		Direction flags = 0;
		if (flag == "north")
			flags = flags | Direction.North;
		else if (flag == "east")
			flags = flags | Direction.East;
		else if (flag == "south")
			flags = flags | Direction.South;
		else if (flag == "west")
			flags = flags | Direction.West;
		return flags;
	}
}