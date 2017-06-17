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

		float distance2target = Vector3.Distance (transform.position, currentTarget);
		if ((!turned) && (distance2target < 1.5f)) {
			Debug.Log ("citizen select next target");
			selectNextTarget ();
		}
	}
	public void OnTriggerEnter(Collider collision) {		
		if (!turned) {
			turned = true;
			Debug.Log ("citizen found flyer");
			Destroy (collision.gameObject);
		//	GetComponent<SpriteRenderer> ().sprite = citizen_turned;
			transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = citizen_turned;
			this.tag = "Turned";
			currentTarget = exitPosition.position;
			agent.SetDestination (currentTarget);
		}
	}
}
