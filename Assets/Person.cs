using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour {
	protected float targetX;
	protected float targetY;

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
		
		float diffX = Mathf.Abs (targetX - transform.position.x);
		float diffY = Mathf.Abs (targetY - transform.position.y);

	//	Debug.Log ("forbidden: " + forbiddenAxis);

		if ( (((forbiddenAxis.Equals ("x")) && (diffY < targetEpsilon))) || (((forbiddenAxis.Equals ("y")) && (diffX < targetEpsilon))) ) {
			followLastDirection = true;	
		} 

		if (runfreeCount > 0) {
			//Debug.Log ("run free!");
			runfreeCount--;
			switch (freeDirection) {
			case "up":
				MoveUp ();
				break;
			case "down":
				MoveDown ();
				break;
			case "left":
				MoveLeft ();
				break;
			case "right":
				MoveRight ();
				break;
			}
		}
		else if (followLastDirection) {
			if (lastDirection == "up")
				MoveUp ();
			else if (lastDirection == "down")
				MoveDown ();
			else if (lastDirection == "left")
				MoveLeft ();
			else if (lastDirection == "right")
				MoveRight ();

			if (forbiddenAxis.Equals ("0"))
				followLastDirection = false;
		} else {
			if ((diffX < targetEpsilon) && (diffY < targetEpsilon)) {
				Stop ();
				return true;

			} else {
			//	Debug.Log ("forbidden: " + forbiddenAxis);
				if ( (!forbiddenAxis.Equals ("x")) && (diffX > diffY) || (forbiddenAxis.Equals("y"))) { //walk in X axis direction
				//	Debug.Log("walkX" + diffX);
					if (targetX < transform.position.x) {
						MoveLeft ();
					} else {
						MoveRight ();
					}
				} else { //walk in Y axis direction
				//	Debug.Log("walkY " + diffY + " " + Time.renderedFrameCount);
					if (targetY > transform.position.y) {
						MoveUp ();
					} else {
						MoveDown ();
					}
				}
			}
		}
		return false;

	}
	protected void selectNextTarget() {		
		targetX = targets [targetIndx].x;
		targetY = targets [targetIndx].y;
		targetIndx = (targetIndx + 1) % targets.Length;
	}
	public void OnCollisionExit2D(Collision2D collision) {
		forbiddenAxis = "0";
		collisionFrameCount = 0;
		//Debug.Log ("exit");
	}
	public void OnCollisionStay2D(Collision2D collision) {
		OnCollisionEnter2D (collision);
		collisionFrameCount++;
		if ((collisionFrameCount > 15) && (Vector3.Distance(lastPosition, transform.position) < targetEpsilon/Time.deltaTime)) {
		//	Debug.Log ("run free." + Vector3.Distance(lastPosition, transform.position));
			runfreeCount = 10;
			collisionFrameCount = 0;
			//switch (lastDirection) {

			/*case "up":
				freeDirection = "right";
				break;
			case "down":
				freeDirection = "left";
				break;
			case "left":
				freeDirection = "up";
				break;
			case "right":
				freeDirection = "down";
				break;*/
			//}
			int dir = Mathf.RoundToInt(Random.Range(0.0f, 4.0f));
			if (dir == 0) {
				freeDirection = "left";
			} else if (dir == 1) {
				freeDirection = "right";
			} else if (dir == 2) {
				freeDirection = "up";
			} else if (dir == 3) {
				freeDirection = "down";
			}


		}
		lastPosition = transform.position;
	}
	public void OnCollisionEnter2D(Collision2D  collision) 
	{
		//collisionFrameCount = 0;
		//Debug.Log ("enter");
		if (collision.gameObject.tag.Equals ("Wall")) {
			Collider2D collider = collision.collider;
			bool collideFromLeft = false;
			bool collideFromTop = false;
			bool collideFromRight = false;
			bool collideFromBottom = false;
			float RectWidth = this.GetComponent<Collider2D> ().bounds.size.x;
			float RectHeight = this.GetComponent<Collider2D> ().bounds.size.y; 	

			Vector3 center = collider.bounds.center;

			float px = 0.0f;
			float py = 0.0f;
			for (int i = 0; i < collision.contacts.Length; i++) {
				px += collision.contacts [i].point.x;
				py += collision.contacts [i].point.y;
			}
			px /= (float) collision.contacts.Length;
			py /= (float) collision.contacts.Length;
			Vector3 contactPoint = new Vector3 (px, py, 0.0f);

			if (contactPoint.y > center.y && //checks that circle is on top of rectangle
				(contactPoint.x < center.x + RectWidth / 2 && contactPoint.x > center.x - RectWidth / 2)) {
				collideFromTop = true;
			}
			else if (contactPoint.y < center.y &&
				(contactPoint.x < center.x + RectWidth / 2 && contactPoint.x > center.x - RectWidth / 2)) {
				collideFromBottom = true;
			}
			else if (contactPoint.x > center.x &&
				(contactPoint.y < center.y + RectHeight / 2 && contactPoint.y > center.y - RectHeight / 2)) {
				collideFromRight = true;
			}
			else if (contactPoint.x < center.x &&
				(contactPoint.y < center.y + RectHeight / 2 && contactPoint.y > center.y - RectHeight / 2)) {
				collideFromLeft = true;
			}

			//		cp.position = contactPoint;
			if (collideFromLeft || collideFromRight) {
				lastFaxis = forbiddenAxis = "x";
				//Debug.Log ("set to x");
			} else if (collideFromTop || collideFromBottom) {
				lastFaxis = forbiddenAxis = "y";
				//Debug.Log ("set to y");
			}

			//Debug.Log ("set to " + forbiddenAxis);
		}


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
