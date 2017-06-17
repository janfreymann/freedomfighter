using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour {
	protected int targetX;
	protected int targetY;

	protected float tfX;
	protected float tfY;

	protected float targetEpsilon = 0.05f;
	protected float charSpeed = 2f;

	protected Rigidbody2D _rigidbody;
	protected Collider2D _collider;

	protected string lastAxis;
	protected string lastDirection;
	protected bool followLastDirection;
	protected string forbiddenAxis;
	protected string lastFaxis;

	protected int collisionFrameCount;
	protected int runfreeCount;
	protected string freeDirection;

	protected Vector3 lastPosition;

	public Vector2[] targets;
	public int targetIndx;

	public TilesMap tm;

	//public Transform cp;

	// Use this for initialization
	public void Start () {
		_rigidbody = GetComponent<Rigidbody2D> ();
		_collider = GetComponent<Collider2D> ();
		lastAxis = "x";
		forbiddenAxis = "0";
		lastFaxis = "x";
		lastDirection = "up";
		followLastDirection = false;
		runfreeCount = 0;
	}
	
	// Update is called once per frame
	public void Update () {
		
		transform.rotation = Quaternion.identity;
	}

	protected bool MoveToTarget() {
		PathFind.Point _from = tm.getPointForPos(transform.position.x, transform.position.y); //current position
		PathFind.Point _to = new PathFind.Point(targetX, targetY); //target
		List<PathFind.Point> path = PathFind.Pathfinding.FindPath(tm.grid, _from, _to);
		if (path.Count > 0) {
			PathFind.Point nextPoint = path [0];
			Vector2 coords = tm.getCoordsForTile (nextPoint);
			tfX = coords.x;
			tfY = coords.y;

			//can walk:

			float x = transform.position.x;
			float y = transform.position.y;
			float diffX = Mathf.Abs (x - tfX);
			float diffY = Mathf.Abs (y - tfY);
			if ((diffX < targetEpsilon) && (diffY < targetEpsilon)) {
				Stop ();
				return true;
			} else {
				if (diffX > diffY) {
					if (x < tfX)
						MoveRight ();
					else
						MoveLeft ();
				} else {
					if (y < tfY)
						MoveUp ();
					else
						MoveDown ();
				}
			}

		} else {
			Debug.Log ("pathcount zero: " + _from.x + "/" + _from.y + " to " + _to.x + "/" + _to.y);
		}


		return false;
	}
	protected void selectNextTarget() {		
		PathFind.Point p = tm.getPointForPos (targets [targetIndx].x, targets [targetIndx].y);
		targetX = p.x;
		targetY = p.y;
		targetIndx = (targetIndx + 1) % targets.Length;
	}
	protected void MoveLeft() {
		_rigidbody.velocity = new Vector2 (-charSpeed, 0.0f);
		lastAxis = "x";
		lastDirection = "left";
		//Debug.Log ("moveleft");
	}
	protected void MoveRight() {
		_rigidbody.velocity = new Vector2 (charSpeed, 0.0f);
		lastAxis = "x";
		lastDirection = "right";
		//Debug.Log ("moveright");
	}
	protected void MoveUp() {
		_rigidbody.velocity = new Vector2 (0.0f, charSpeed);
		lastAxis = "y";
		lastDirection = "up";
		//Debug.Log ("moveup");
	}
	protected void MoveDown() {
		_rigidbody.velocity = new Vector2 (0.0f, -charSpeed);
		lastAxis = "y";
		lastDirection = "down";
		//Debug.Log ("movedown");
	}
	protected void Stop() {
		_rigidbody.velocity = new Vector2 (0.0f, 0.0f);
	}


}
