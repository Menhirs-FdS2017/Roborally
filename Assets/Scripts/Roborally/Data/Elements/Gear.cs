using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : BoardElement {
	public Orientation direction;
	public Gear (Orientation d) {
		direction = d;
		ElementType = "Turn";
		if (d == Orientation.CW)
			ElementType += "CW";
		else
			ElementType += "CCW";
	}
}
