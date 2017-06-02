using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Board : GameElement {
	[SerializeField]
	BoardPosition gridSize;
	uint[,] Map;
	public Board(JSONObject description) {
		gridSize.x = (uint) description ["size"] ["x"].n;
		gridSize.y = (uint) description ["size"] ["y"].n;
		Map = new uint[gridSize.y, gridSize.x];
		for (uint y = 1; y <= gridSize.y; y++) {
			for (uint x = 1; x <= gridSize.x; x++) {
				Map [y - 1, x - 1] = GameElementManager.NoneID;
			}
		}
		if (description.HasField ("gears")) {
			if (description ["gears"].HasField ("cw")) {
				foreach(JSONObject p in description ["gears"]["cw"].list){
					uint x = (uint) p ["x"].n;
					uint y = (uint) p ["y"].n;
					Gear elt = new Gear (Orientation.CW);
					if ( p.HasField("direction"))
						elt.Facing = DirectionParser.FromString (p ["direction"].str);
					Set (x, y, elt);
				}
			}
			if (description ["gears"].HasField ("ccw")) {
				foreach(JSONObject p in description ["gears"]["ccw"].list){
					uint x = (uint) p ["x"].n;
					uint y = (uint) p ["y"].n;
					Gear elt = new Gear (Orientation.CCW);
					if ( p.HasField("direction"))
						elt.Facing = DirectionParser.FromString (p ["direction"].str);
					Set (x, y, elt);
				}
			}
		}
		if (description.HasField ("holes")) {
			foreach(JSONObject p in description ["holes"].list){
				uint x = (uint) p ["x"].n;
				uint y = (uint) p ["y"].n;
				Hole elt = new Hole ();
				if ( p.HasField("direction"))
					elt.Facing = DirectionParser.FromString (p ["direction"].str);
				Set (x, y, elt);
			}
		}
		if (description.HasField ("equips")) {
			foreach(JSONObject p in description ["equips"].list){
				uint x = (uint) p ["x"].n;
				uint y = (uint) p ["y"].n;
				Equip elt = new Equip ();
				if ( p.HasField("direction"))
					elt.Facing = DirectionParser.FromString (p ["direction"].str);
				Set (x, y, elt);
			}
		}
		if (description.HasField ("wrenchs")) {
			foreach(JSONObject p in description ["wrenchs"].list){
				uint x = (uint) p ["x"].n;
				uint y = (uint) p ["y"].n;
				Repair elt = new Repair ();
				if ( p.HasField("direction"))
					elt.Facing = DirectionParser.FromString (p ["direction"].str);
				Set (x, y, elt);
			}
		}
		if (description.HasField ("conveyors")) {
			if (description ["conveyors"].HasField ("normal")) {
				if (description ["conveyors"] ["normal"].HasField ("straight")) {
					foreach (JSONObject p in description ["conveyors"]["normal"]["straight"].list) {
						uint x = (uint)p ["x"].n;
						uint y = (uint)p ["y"].n;
						ConveyorBeltStraight elt = new ConveyorBeltStraight ();
						if ( p.HasField("direction"))
							elt.Facing = DirectionParser.FromString (p ["direction"].str);
						Set (x, y, elt);
					}
				}
				if (description ["conveyors"] ["normal"].HasField ("joinCCW")) {
					foreach (JSONObject p in description ["conveyors"]["normal"]["joinCCW"].list) {
						uint x = (uint)p ["x"].n;
						uint y = (uint)p ["y"].n;
						ConveyorBeltJunction elt = new ConveyorBeltJunction ();
						if ( p.HasField("direction"))
							elt.Facing = DirectionParser.FromString (p ["direction"].str);
						Set (x, y, elt);
					}
				}
			}
		}
		for (uint y = 1; y <= gridSize.y; y++) {
			for (uint x = 1; x <= gridSize.x; x++) {
				if (Map [y - 1, x - 1] == GameElementManager.NoneID) {
					BoardElement elt = new OpenFloor ();
					Set (x, y, elt);
				}
			}
		}
		//
		if (description.HasField ("walls")) {
			if (description ["walls"].HasField ("north")) {
				foreach(JSONObject p in description ["walls"]["north"].list){
					uint x = (uint) p ["x"].n;
					uint y = (uint) p ["y"].n;
					BoardElement tmp = GameElementManager.Instance [Map [y - 1, x - 1]] as BoardElement;
					tmp.Walls = tmp.Walls | Direction.North;
				}
			}
			if (description ["walls"].HasField ("east")) {
				foreach(JSONObject p in description ["walls"]["east"].list){
					uint x = (uint) p ["x"].n;
					uint y = (uint) p ["y"].n;
					BoardElement tmp = GameElementManager.Instance [Map [y - 1, x - 1]] as BoardElement;
					tmp.Walls = tmp.Walls | Direction.East;
				}
			}
			if (description ["walls"].HasField ("south")) {
				foreach(JSONObject p in description ["walls"]["south"].list){
					uint x = (uint) p ["x"].n;
					uint y = (uint) p ["y"].n;
					BoardElement tmp = GameElementManager.Instance [Map [y - 1, x - 1]] as BoardElement;
					tmp.Walls = tmp.Walls | Direction.South;
				}
			}
			if (description ["walls"].HasField ("west")) {
				foreach(JSONObject p in description ["walls"]["west"].list){
					uint x = (uint) p ["x"].n;
					uint y = (uint) p ["y"].n;
					BoardElement tmp = GameElementManager.Instance [Map [y - 1, x - 1]] as BoardElement;
					tmp.Walls = tmp.Walls | Direction.West;
				}
			}
		}
	}
	public uint Width { get { return gridSize.x; } }
	public uint Height { get { return gridSize.y; } }
	void Set(uint x, uint y,BoardElement tile) {
		tile.Container = this;
		tile.Position = new BoardPosition (x, y);
		Map [y - 1, x - 1] = tile.ID;
	}
	public BoardElement this [uint y, uint x] {
		get {
			if ( x <= 0 || x > gridSize.x)
				return null;
			if ( y <= 0 || y > gridSize.y)
				return null;
			return GameElementManager.Instance [Map [y - 1, x - 1]] as BoardElement;
		}
	}
	public BoardElement this [BoardPosition position] {
		get {
			if ( position.x <= 0 || position.x > gridSize.x)
				return null;
			if ( position.y <= 0 || position.y > gridSize.y)
				return null;
			return GameElementManager.Instance [Map [position.y - 1, position.x - 1]] as BoardElement;
		}
	}
	public BoardElement this [BoardPosition position, Direction direction] {
		get {
			BoardPosition next = position.Neighboor (direction);
			if ( next.x <= 0 || next.x > gridSize.x)
				return null;
			if ( next.y <= 0 || next.y > gridSize.y)
				return null;
			return GameElementManager.Instance [Map [next.y - 1, next.x - 1]] as BoardElement;
		}
	}
	public void RotateCCW () {
		BoardPosition tmps = new BoardPosition (gridSize.y, gridSize.x);
		uint[,] old = Map;
		Map = new uint[gridSize.x, gridSize.y];
		for (uint y = 1; y <= gridSize.y; y++) {
			for (uint x = 1; x <= gridSize.x; x++) {
				uint nx = gridSize.x - y + 1;
				uint ny = x;
				BoardElement tmpe = GameElementManager.Instance [old [y - 1, x - 1]] as BoardElement;
				tmpe.RotateCCW ();
				Set (nx, ny, tmpe);
			}
		}
		gridSize = tmps;
	}
	public void RotateCW () {
		BoardPosition tmps = gridSize;
		gridSize = new BoardPosition (gridSize.y, gridSize.x);
		uint[,] old = Map;
		Map = new uint[gridSize.y, gridSize.x];
		for (uint y = 1; y <= tmps.y; y++) {
			for (uint x = 1; x <= tmps.x; x++) {
				uint nx = y;
				uint ny = tmps.x - x + 1;
				BoardElement tmpe = GameElementManager.Instance [old [y - 1, x - 1]] as BoardElement;
				tmpe.RotateCW ();
				Set (nx, ny, tmpe);
			}
		}
	}
}
