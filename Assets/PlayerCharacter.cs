using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Person {

	public Transform flyerPrefab;

	// Use this for initialization
	void Start () {
		base.Start ();
		charSpeed = 5f;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	
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
			
		//if (Input.GetKey (KeyCode.Space)) {
		if(Input.GetKeyUp(KeyCode.Space)) {
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
	protected void MoveLeft() {
		_rigidbody.velocity = new Vector3 (-charSpeed, 0.0f,  0.0f);
		lastDirection = "left";
		//Debug.Log ("moveleft");
	}
	protected void MoveRight() {
		_rigidbody.velocity = new Vector3 (charSpeed, 0.0f, 0.0f);
		lastDirection = "right";
		//Debug.Log ("moveright");
	}
	protected void MoveUp() {
		_rigidbody.velocity = new Vector3 (0.0f, 0.0f, charSpeed);
		lastDirection = "up";
		//Debug.Log ("moveup");
	}
	protected void MoveDown() {
		_rigidbody.velocity = new Vector3 (0.0f, 0.0f, -charSpeed);
		lastDirection = "down";
		//Debug.Log ("movedown");
	}
	protected void Stop() {
		_rigidbody.velocity = new Vector3 (0.0f, 0.0f, 0.0f);
	}
}
