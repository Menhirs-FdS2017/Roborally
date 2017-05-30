using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ConveyorBeltStraight : ConveyorBelt {
	public ConveyorBeltStraight () {
		ElementType = "Conveyor";
		Type = ConveyorType.Straight;
		Fast = false;
	}
	public override void Activate ()
	{
	}
}
