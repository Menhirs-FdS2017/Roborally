using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameElement {
	uint _ID;
	public uint ID { get { return _ID; } }
	/// <summary>
	/// The container (board/tile/...) ID in GameElementManager
	/// </summary>
	public uint ContainerID;
	public GameElement Container {
		get {
			if (ContainerID == GameElementManager.NoneID)
				return null;
			return GameElementManager.Instance [ContainerID];
		}
		set {
			if (value == null)
				ContainerID = GameElementManager.NoneID;
			else
				ContainerID = value.ID;
		}
	}
	/// <summary>
	/// The contained element (bot) ID in GameElementManager
	/// </summary>
	public uint ContainedID;
	public Bot Contained {
		get {
			if (ContainedID == GameElementManager.NoneID)
				return null;
			return GameElementManager.Instance [ContainedID] as Bot;
		}
		set {
			if (value == null)
				ContainedID = GameElementManager.NoneID;
			else
				ContainedID = value.ID;
		}
	}
	public GameElement () {
		_ID = GameElementManager.Instance.NewID;
		Container = null;
		Contained = null;
		GameElementManager.Instance.Add (this);
	}
}
