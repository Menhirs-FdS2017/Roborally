using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Flags]
public enum Direction {
	//None	= 0x00,
	North	= 0x01,
	East	= 0x02,
	South	= 0x04,
	West	= 0x08
}

[Serializable]
public enum Orientation {
	CW,
	CCW
}

[Serializable]
public enum BoardOrientation {
	None,
	CW,
	HalfTurn,
	CCW
}

[Serializable]
public enum ConveyorType {
	Straight,
	Turn,
	Join
}
