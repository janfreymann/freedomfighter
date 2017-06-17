using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : Person {
	public Sprite citizen_turned;
	public Transform exitPosition;

	private bool turned = false;


	// Use this for initialization
	void Start () {
		base.Start ();
		charSpeed = 1.5f;
		selectNextTarget ();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
		MoveToTarget ();

		Vector2 tv = tm.getCoordsForTile (new PathFind.Point(targetX, targetY));

		float diffX = Mathf.Abs (tv.x - transform.position.x);
		float diffY = Mathf.Abs (tv.y - transform.position.y);
		if ((!turned) && (diffX < targetEpsilon) && (diffY < targetEpsilon)) {
			selectNextTarget ();
		} else {
			Debug.Log ("diffX/diffY: " + diffX + "/" + diffY);
		}
	}
	public void OnTriggerEnter2D(Collider2D collision) {
		
		if (!turned) {
			turned = true;
			Debug.Log ("citizen found flyer");
			Destroy (collision.gameObject);
			GetComponent<SpriteRenderer> ().sprite = citizen_turned;
			this.tag = "Turned";
			PathFind.Point p = tm.getPointForPos (exitPosition.position.x, exitPosition.position.y);
			targetX = p.x;
			targetY = p.y;
		}
	}
}
