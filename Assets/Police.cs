using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : Person {

	public PlayerCharacter player;

	// Use this for initialization
	void Start () {
		base.Start ();
		charSpeed = 1.5f;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
		targetX = player.transform.position.x;
		targetY = player.transform.position.y;
		MoveToTarget ();
	}

	public void OnTriggerEnter2D(Collider2D collision) {
		Debug.Log("found flyer!");
		Destroy (collision.gameObject);
	}
}
