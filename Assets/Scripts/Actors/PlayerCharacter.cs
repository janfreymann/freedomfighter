using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : Person {

	public Transform flyerPrefab;
	public bool alive; 

	bool running = false;
	float lastRunningEvent = 100.0f;
	float runningEventInterval = 0.5f;

	// Use this for initialization
	new void Start () {
		base.Start ();
		alive = true;
		charSpeed = 5f; 
		godScript.miniMap.addActor (this);
		this.uuid = 99999;

	}
		
	// Update is called once per frame
	new void Update () {
		base.Update ();
	
		if (Input.GetKey (KeyCode.UpArrow)) {	
			running = true;
			if (Input.GetKey (KeyCode.RightArrow)) {
				MoveUpRight ();
			} else if (Input.GetKey (KeyCode.LeftArrow)) {
				MoveUpLeft ();
			} else {
				MoveUp ();
			}
		} else if (Input.GetKey (KeyCode.DownArrow)) {			
			running = true;
			if (Input.GetKey (KeyCode.RightArrow)) {
				MoveDownRight ();
			} else if (Input.GetKey (KeyCode.LeftArrow)) {
				MoveDownLeft ();
			} else {
				MoveDown ();
			}
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

		//collider quick fix, keep in bounds!
		float localX = transform.localPosition.x;
		float localY = transform.localPosition.y;
		float localZ = transform.localPosition.z;



        if (transform.localPosition.x < GodScript.boundsXmin)
        {
            transform.localPosition = new Vector3(GodScript.boundsXmin, localY, localZ);
        }
        else if (transform.localPosition.x > GodScript.boundsXmax)
        {
            transform.localPosition = new Vector3(GodScript.boundsXmax, localY, localZ);
        }

        if (transform.localPosition.y < GodScript.boundsYmin)
        {
            transform.localPosition = new Vector3(localX, GodScript.boundsYmin, localZ);
        }
        else if (transform.localPosition.y > GodScript.boundsYmax)
        {
            transform.localPosition = new Vector3(localX, GodScript.boundsYmax, localZ);
        }
	}
	private void dropFlyer() {
		Debug.Log ("dropFlyer()");
		GameMaster gm = GameMaster.getInstance ();
		if (gm.CheckAndDropFlyer ()) {
			Transform nFlyer = Instantiate (flyerPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
			AkSoundEngine.PostEvent ("Play_FlyerDrop", gameObject);
		}
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
	protected void MoveUpRight() {
		_rigidbody.velocity = new Vector3 (charSpeed/1.4f, 0.0f, charSpeed/1.4f);
	}
	protected void MoveUpLeft() {
		_rigidbody.velocity = new Vector3 (-charSpeed/1.4f, 0.0f, charSpeed/1.4f);
	}
	protected void MoveDownRight() {
		_rigidbody.velocity = new Vector3 (charSpeed/1.4f, 0.0f, -charSpeed/1.4f);
	}
	protected void MoveDownLeft() {
		_rigidbody.velocity = new Vector3 (-charSpeed/1.4f, 0.0f, -charSpeed/1.4f);
	}
}
