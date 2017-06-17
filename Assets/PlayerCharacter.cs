using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCharacter : Person {

	public Transform flyerPrefab;

	bool running = false;
	float lastRunningEvent = 100.0f;
	float runningEventInterval = 0.5f;

	// Use this for initialization
	void Start () {
		base.Start ();
		charSpeed = 5f;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	
		if (Input.GetKey (KeyCode.UpArrow)) {			
			running = true;
			MoveUp();
		} else if (Input.GetKey (KeyCode.DownArrow)) {			
			running = true;
			MoveDown();
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			running = true;
			MoveLeft ();
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			running = true;
			MoveRight ();
		} else {
			running = false;
			Stop ();
		}			
		if(Input.GetKeyUp(KeyCode.Space)) {
			dropFlyer ();
		}

		if (running) {
			lastRunningEvent += Time.deltaTime;
			if (lastRunningEvent > runningEventInterval) {
				AkSoundEngine.PostEvent ("Play_ShoeRun", gameObject);
				lastRunningEvent = 0.0f;
			}
		}
	}
	private void dropFlyer() {
		Transform nFlyer = Instantiate (flyerPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
		AkSoundEngine.PostEvent ("Play_FlyerDrop", gameObject);
	}
	protected void MoveLeft() {
		_rigidbody.velocity = new Vector3 (-charSpeed, 0.0f,  0.0f);
	}
	protected void MoveRight() {
		_rigidbody.velocity = new Vector3 (charSpeed, 0.0f, 0.0f);
	}
	protected void MoveUp() {
		_rigidbody.velocity = new Vector3 (0.0f, 0.0f, charSpeed);
	}
	protected void MoveDown() {
		_rigidbody.velocity = new Vector3 (0.0f, 0.0f, -charSpeed);
	}
	protected void Stop() {
		_rigidbody.velocity = new Vector3 (0.0f, 0.0f, 0.0f);
	}
}
