using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : Person {

	//public PlayerCharacter player;
	public GameObject fugitive;

	private float distanceToLose = 3f;

	private bool followingFugitive;

	// Use this for initialization
	void Start () {
		base.Start ();
		charSpeed = 1.5f;
		followingFugitive = false;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
		if (followingFugitive) {
			Debug.Log ("Following");
			currentTarget = fugitive.transform.position;
			if (Vector2.Distance (currentTarget, transform.position) > distanceToLose) {
				followingFugitive = false;
				selectNextTarget ();
			} else {
				MoveToTarget ();
			}
		} else {
			MoveToTarget ();
		}
	}

	//public void OnTriggerEnter2D(Collider2D collision) {
	void OnCollisionEnter2D(Collision2D  collision){
		//base.OnCollisionEnter2D ();
		string t = collision.gameObject.tag;


		 if ((t.Equals("Turned") || (t.Equals("Player")))){
			followingFugitive = true;
			fugitive = collision.gameObject;
			Debug.Log ("Gotcha!");
		}
	}

	public void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag.Equals ("Flyer")) { // it is a flyer!
			Destroy (collision.gameObject);
			Debug.Log ("Policeman found flyer");
		}
	}
}
