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

		float diffX = Mathf.Abs (targetX - transform.position.x);
		float diffY = Mathf.Abs (targetY - transform.position.y);
		if ((!turned) && (diffX < targetEpsilon) && (diffY < targetEpsilon)) {
			selectNextTarget ();
		}
	}
	public void OnTriggerEnter2D(Collider2D collision) {
		
		if (!turned) {
			turned = true;
			Debug.Log ("citizen found flyer");
			Destroy (collision.gameObject);
			GetComponent<SpriteRenderer> ().sprite = citizen_turned;
			tag = "Turned";
			targetX = exitPosition.position.x;
			targetY = exitPosition.position.y;
		}
	}
}
