using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MultiDimElements
{
	[SerializeField]
	public List<List<BoardElement>> datas;
	public MultiDimElements(uint w, uint h) {
		datas = new List<List<BoardElement>> ();
		for (uint y = 0; y < h; y++) {
			List<BoardElement> tmp = new List<BoardElement> ();
			for (uint x = 0; x < w; x++) {
				tmp.Add (null);
			}
			datas.Add (tmp);
		}
	}
	public BoardElement this [uint y, uint x] {
		get {
			if ( y < 0 || y >= datas.Count)
				return null;
			if ( x < 0 || x >= datas[(int)y].Count)
				return null;
			return datas[(int)y][(int)x];
		}
		set {
			if ( y < 0 || y >= datas.Count)
				return;
			if ( x < 0 || x >= datas[(int)y].Count)
				return;
			datas[(int)y][(int)x] = value;
		}
	}
}

[Serializable]
public class Board {
	[SerializeField]
	BoardPosition gridSize;
	[SerializeField]
	MultiDimElements elements;
	public Board(JSONObject description) {
		gridSize.x = (uint) description ["size"] ["x"].n;
		gridSize.y = (uint) description ["size"] ["y"].n;
		elements = new MultiDimElements(gridSize.x, gridSize.y);
		for (uint y = 1; y <= gridSize.y; y++) {
			for (uint x = 1; x <= gridSize.x; x++) {
				BoardElement elt = new OpenFloor ();
				elt.Position = new BoardPosition(x,y);
				elt.Contained = null;
				elt.Container = this;
				elements [y - 1, x - 1] = elt;
			}
		}
		if (description.HasField ("gears")) {
			if (description ["gears"].HasField ("cw")) {
				foreach(JSONObject p in description ["gears"]["cw"].list){
					uint x = (uint) p ["x"].n;
					uint y = (uint) p ["y"].n;
					Gear elt = new Gear (Orientation.CW);
					elt.Position = new BoardPosition(x,y);
					elt.Contained = null;
					elt.Container = this;
					if ( p.HasField("direction"))
						elt.Facing = DirectionParser.FromString (p ["direction"].str);
					elements [y - 1, x - 1] = elt;
				}
			}
			if (description ["gears"].HasField ("ccw")) {
				foreach(JSONObject p in description ["gears"]["ccw"].list){
					uint x = (uint) p ["x"].n;
					uint y = (uint) p ["y"].n;
					Gear elt = new Gear (Orientation.CCW);
					elt.Position = new BoardPosition(x,y);
					elt.Contained = null;
					elt.Container = this;
					if ( p.HasField("direction"))
						elt.Facing = DirectionParser.FromString (p ["direction"].str);
					elements [y - 1, x - 1] = elt;
				}
			}
		}
		if (description.HasField ("holes")) {
			foreach(JSONObject p in description ["holes"].list){
				uint x = (uint) p ["x"].n;
				uint y = (uint) p ["y"].n;
				Hole elt = new Hole ();
				elt.Position = new BoardPosition(x,y);
				elt.Contained = null;
				elt.Container = this;
				if ( p.HasField("direction"))
					elt.Facing = DirectionParser.FromString (p ["direction"].str);
				elements [y - 1, x - 1] = elt;
			}
		}
		if (description.HasField ("equips")) {
			foreach(JSONObject p in description ["equips"].list){
				uint x = (uint) p ["x"].n;
				uint y = (uint) p ["y"].n;
				Equip elt = new Equip ();
				elt.Position = new BoardPosition(x,y);
				elt.Contained = null;
				elt.Container = this;
				if ( p.HasField("direction"))
					elt.Facing = DirectionParser.FromString (p ["direction"].str);
				elements [y - 1, x - 1] = elt;
			}
		}
		if (description.HasField ("wrenchs")) {
			foreach(JSONObject p in description ["wrenchs"].list){
				uint x = (uint) p ["x"].n;
				uint y = (uint) p ["y"].n;
				Repair elt = new Repair ();
				elt.Position = new BoardPosition(x,y);
				elt.Contained = null;
				elt.Container = this;
				if ( p.HasField("direction"))
					elt.Facing = DirectionParser.FromString (p ["direction"].str);
				elements [y - 1, x - 1] = elt;
			}
		}
		if (description.HasField ("conveyors")) {
			if (description ["conveyors"].HasField ("normal")) {
				if (description ["conveyors"] ["normal"].HasField ("straight")) {
					foreach (JSONObject p in description ["conveyors"]["normal"]["straight"].list) {
						uint x = (uint)p ["x"].n;
						uint y = (uint)p ["y"].n;
						ConveyorBeltStraight elt = new ConveyorBeltStraight ();
						elt.Position = new BoardPosition (x, y);
						elt.Contained = null;
						elt.Container = this;
						if ( p.HasField("direction"))
							elt.Facing = DirectionParser.FromString (p ["direction"].str);
						elements [y - 1, x - 1] = elt;
					}
				}
				if (description ["conveyors"] ["normal"].HasField ("joinCCW")) {
					foreach (JSONObject p in description ["conveyors"]["normal"]["joinCCW"].list) {
						uint x = (uint)p ["x"].n;
						uint y = (uint)p ["y"].n;
						ConveyorBeltJunction elt = new ConveyorBeltJunction ();
						elt.Position = new BoardPosition (x, y);
						elt.Contained = null;
						elt.Container = this;
						if ( p.HasField("direction"))
							elt.Facing = DirectionParser.FromString (p ["direction"].str);
						elements [y - 1, x - 1] = elt;
					}
				}
			}
		}
		if (description.HasField ("walls")) {
			if (description ["walls"].HasField ("north")) {
				foreach(JSONObject p in description ["walls"]["north"].list){
					uint x = (uint) p ["x"].n;
					uint y = (uint) p ["y"].n;
					elements [y - 1, x - 1].Walls = elements [y - 1, x - 1].Walls | Direction.North;
				}
			}
			if (description ["walls"].HasField ("east")) {
				foreach(JSONObject p in description ["walls"]["east"].list){
					uint x = (uint) p ["x"].n;
					uint y = (uint) p ["y"].n;
					elements [y - 1, x - 1].Walls = elements [y - 1, x - 1].Walls | Direction.East;
				}
			}
			if (description ["walls"].HasField ("south")) {
				foreach(JSONObject p in description ["walls"]["south"].list){
					uint x = (uint) p ["x"].n;
					uint y = (uint) p ["y"].n;
					elements [y - 1, x - 1].Walls = elements [y - 1, x - 1].Walls | Direction.South;
				}
			}
			if (description ["walls"].HasField ("west")) {
				foreach(JSONObject p in description ["walls"]["west"].list){
					uint x = (uint) p ["x"].n;
					uint y = (uint) p ["y"].n;
					elements [y - 1, x - 1].Walls = elements [y - 1, x - 1].Walls | Direction.West;
				}
			}
		}
	}
	public uint Width { get { return gridSize.x; } }
	public uint Height { get { return gridSize.y; } }
	public BoardElement this [uint y, uint x] {
		get {
			if ( x <= 0 || x > gridSize.x)
				return null;
			if ( y <= 0 || y > gridSize.y)
				return null;
			return elements [y-1, x-1];
		}
	}
	public BoardElement this [BoardPosition position] {
		get {
			if ( position.x <= 0 || position.x > gridSize.x)
				return null;
			if ( position.y <= 0 || position.y > gridSize.y)
				return null;
			Debug.Log (position.ToString () + " : " + elements);//elements.GetLength(0)+" "+elements.GetLength(1));
			return elements [position.y-1, position.x-1];
		}
	}
	public BoardElement this [BoardPosition position, Direction direction] {
		get {
			BoardPosition next = position.Neighboor (direction);
			if ( next.x <= 0 || next.x > gridSize.x)
				return null;
			if ( next.y <= 0 || next.y > gridSize.y)
				return null;
			return elements [next.y-1, next.x-1];
		}
	}
	public void RotateCCW () {
		MultiDimElements elts = new MultiDimElements(gridSize.x, gridSize.y);
		for (uint y = 1; y <= gridSize.y; y++) {
			for (uint x = 1; x <= gridSize.x; x++) {
				uint nx = gridSize.x - y + 1;
				uint ny = x;
				elts [ny-1, nx-1] = elements [y-1, x-1];
				elts [ny - 1, nx - 1].Position = new BoardPosition (nx, ny);
				elts [ny - 1, nx - 1].RotateCCW ();
			}
		}
		elements = elts;
	}
	public void RotateCW () {
		MultiDimElements elts = new MultiDimElements(gridSize.x, gridSize.y);
		for (uint y = 1; y <= gridSize.y; y++) {
			for (uint x = 1; x <= gridSize.x; x++) {
				uint nx = y;
				uint ny = gridSize.x - x + 1;
				elts [ny-1, nx-1] = elements [y-1, x-1];
				elts [ny - 1, nx - 1].Position = new BoardPosition (nx, ny);
				elts [ny - 1, nx - 1].RotateCW ();
			}
		}
		elements = elts;
	}
}
