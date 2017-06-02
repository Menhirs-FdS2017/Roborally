using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameElementManager {
	static GameElementManager _instance;
	public static GameElementManager Instance {
		get {
			if (_instance == null)
				_instance = new GameElementManager ();
			return _instance;
		}
	}
	public uint NewID {
		get { return _currentID++; }
	}
	public GameElement this[uint id] {
		get {
			if (!_gameElements.ContainsKey (id))
				return null;
			return _gameElements [id];
		}
	}
	public void Add ( GameElement element) {
		_gameElements.Add (element.ID, element);
	}
	public const uint NoneID = 100000;
	private uint _currentID;
	private Dictionary<uint,GameElement> _gameElements;
	private GameElementManager () {
		_currentID = 0;
		_gameElements = new Dictionary<uint, GameElement> ();
	}
}
