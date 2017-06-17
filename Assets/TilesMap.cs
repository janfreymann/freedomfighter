using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesMap : MonoBehaviour {
	public int mapWidth = 16;
	public int mapHeight = 16;
	public bool[,] tilesmap;
	public PathFind.Grid grid;
	//simple: assume tiles grid bottom left corner is at 0,0
	public float tileSize = 1f; //size of one tile

	void Awake() {
		
		// create the tiles map
		tilesmap = new bool[,] {
			{true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true},
			{true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true},
			{true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true},
			{true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true}
		};

		// set values here....
		// every float in the array represent the cost of passing the tile at that position.
		// use 0.0f for blocking tiles.

		// create a grid
		grid = new PathFind.Grid(mapWidth, mapHeight, tilesmap);
	}
	public PathFind.Point getPointForPos(float x, float y) {
		int tx = Mathf.RoundToInt (x / tileSize);
		int ty = Mathf.RoundToInt (y / tileSize);
		if (tx >= mapWidth) {
			tx = mapWidth - 1;
		} else if (tx < 0) {
			tx = 0;
		}

		if (ty >= mapHeight) {
			ty = mapHeight - 1;
		} else if (ty < 0) {
			ty = 0;
		}
		return new PathFind.Point (tx, ty);
	}
	public Vector2 getCoordsForTile(PathFind.Point p) {
		int tx = p.x;
		int ty = p.y;
		float x = (float)tx * tileSize + (0.5f * tileSize);
		float y = (float)ty * tileSize + (0.5f * tileSize);
		return new Vector2 (x, y);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
