using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : Person {

	//public PlayerCharacter player;
	public GameObject fugitive;

	private float distanceToLose = 3f;

	public bool followingFugitive;

	public GodScript godObject;

	// Use this for initialization
	void Start () {
		base.Start ();
		followingFugitive = false;
		selectNextTarget ();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();

//		float distanceToCitizen = Vector3.Distance (police.transform.position, citizen.transform.position);
//
//		if (citizen.turned && distanceToCitizen < arrestThreshold) {
//			followingFugitive = true;
//
//		}
//
//		if (distanceToPlayer < arrestThreshold) {
//			followPlayer = true;
//		}
//
//


		if (followingFugitive) { //chasing mode
			Debug.Log ("Following");
			currentTarget = fugitive.transform.position;
			if (Vector2.Distance (currentTarget, transform.position) > distanceToLose) {
				followingFugitive = false;
				Debug.Log ("police lost fugitive, select patrol target");
				selectNextTarget ();
			}
		} else { //patrol mode
			float distance2target = Vector3.Distance (transform.position, currentTarget);
			if (distance2target < 1.5f) {
				Debug.Log ("police select next target");
				selectNextTarget ();
			}
		}

	}

	//public void OnTriggerEnter2D(Collider2D collision) {
//	void OnCollisionEnter(Collision  collision){
//		//base.OnCollisionEnter2D ();
//		string t = collision.gameObject.tag;
//
//		 if ((t.Equals("Turned") || (t.Equals("Player")))){
//			followingFugitive = true;
//			fugitive = collision.gameObject;
//			Debug.Log ("Gotcha!");
//		}
//	}
//
	public void OnTriggerEnter(Collider collision) {
		if (collision.gameObject.tag.Equals ("Flyer")) { // it is a flyer!
			Destroy (collision.gameObject);
			Debug.Log ("Policeman found flyer");
		}
	}
}
