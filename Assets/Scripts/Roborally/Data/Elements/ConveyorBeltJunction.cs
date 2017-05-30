using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ConveyorBeltJunction : ConveyorBelt {
	public ConveyorBeltJunction () {
		ElementType = "ConveyorJoinCCW";
		Type = ConveyorType.Join;
		Fast = false;
	}
	public override void Activate ()
	{
	}
}
