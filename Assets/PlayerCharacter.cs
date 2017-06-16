using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Person {

	public Transform flyerPrefab;

	// Use this for initialization
	void Start () {
		base.Start ();
		charSpeed = 2f;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
		float x = transform.position.x;
		float y = transform.position.y;
	
		if (Input.GetKey (KeyCode.UpArrow)) {			
			MoveUp();
		} else if (Input.GetKey (KeyCode.DownArrow)) {			
			MoveDown();
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			MoveLeft ();
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			MoveRight ();
		} else {
			Stop ();
		}

		if (Input.GetKey (KeyCode.Space)) {
			dropFlyer ();
		}
	}
	public new void onCollisionEnter2D() {
	}
	public new void onCollisionExit2D() {
	}
	private void dropFlyer() {
		Transform nFlyer = Instantiate (flyerPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(0.0f, 0.0f, 0.0f));
	}
}
