using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : GameElement {
	public enum Type {
		TurnLeft,
		TurnRight,
		HalfTurn,
		Backward,
		Forward1,
		Forward2,
		Forward3
	}
	public Type type;
	public uint speed;
}
