using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : Person {

	public Sprite citizen_turned;
	public Transform exitPosition;
	public bool turned = false;
	private bool cameraFollowing = false; //true if citizen near exit to win


	private GameObject walkingSprite;
	private GameObject bustedSprite;

	private float timeSinceLastPlayerCollision = 100.0f;

	// Use this for initialization
	void Start () {
		base.Start ();
		charSpeed = 1.5f;
		walkingSprite = transform.GetChild (0).gameObject;
		bustedSprite = transform.GetChild (1).gameObject;
		selectNextTarget ();
		godScript.miniMap.addActor (this);
	}

	public void bust() {
		walkingSprite.GetComponent<SpriteRenderer> ().enabled = false;
		bustedSprite.GetComponent<SpriteRenderer> ().enabled = true;
		GameMaster.getInstance ().notifyTurnedCitizenDied ();
	}
	// Update is called once per frame
	void Update () {
		base.Update ();
		timeSinceLastPlayerCollision += Time.deltaTime;

		if (tag.Equals ("Busted")) {
			agent.isStopped = true;
			return;
		}


		float distance2target = Vector3.Distance (transform.position, currentTarget);
		//Debug.Log (distance2target);

		if ((turned) && (!cameraFollowing) && (distance2target < 6.0f)) {
			Debug.Log ("camera follows citizen");
			cameraFollowing = true;
			Camera.current.gameObject.GetComponent<FollowPlayer> ().setPlayer (transform);
		}
		if (distance2target < 1.5f) {
			if (turned)  // close enough to exit to win
			{
				Debug.Log ("citizen reached exit");
				GameMaster.getInstance ().notifyTurnedCitizenLeft ();
				AkSoundEngine.PostEvent("Play_score", gameObject);
				godScript.miniMap.removeActor (this);

				Destroy(gameObject);
			} else
			{
				Debug.Log ("citizen select next target");
				selectNextTarget ();
			}
		}
	}
	public void OnTriggerEnter(Collider collision) {		
		if (collision.gameObject.tag.Equals("Flyer"))
		{
			if (!turned) {
				turned = true;
				Debug.Log ("citizen found flyer");
				GameMaster.getInstance ().notifiyCitizenTurned ();
				AkSoundEngine.PostEvent ("Play_pickupFlyer", gameObject);
				AkSoundEngine.PostEvent ("Play_shh", gameObject);

				godScript.miniMap.removeActor (this); //quickly remove and add to change icon
				godScript.miniMap.addActor (this); //new actor will be loaded with citizen turned icon on mini map

				transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = citizen_turned;
				this.tag = "Turned";
				currentTarget = exitPosition.position;
				agent.SetDestination (currentTarget);

				Destroy (collision.gameObject);
			}		
		}
	}
	/*public void handlePlayerCollision(Collision collision) {
		Debug.Log ("handlePlayerCollision()");
		stopBecausePlayer = true;
		// Calculate Angle Between the collision point and the player
		Vector3 dir = collision.contacts[0].point - transform.position;
		// We then get the opposite (-Vector3) and normalize it
		dir = -dir.normalized;
		// And finally we add force in the direction of dir and multiply it by force. 
		// This will push back the player
		GetComponent<Rigidbody>().velocity = dir*10f;
		agent.isStopped = true;
	}*/
	public void OnCollisionStay(Collision collision) {
		if (collision.gameObject.tag.Equals ("Player")) {
			timeSinceLastPlayerCollision = 0.0f;
		}
	}
	public void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag.Equals("Player")) {
			if(timeSinceLastPlayerCollision > 1.0f) {
				if (tag.Equals ("Citizen")) { //not for busted or turned citizens
					Debug.Log("collision with player - select previous target");
					selectPreviousTarget ();
				}
			}
			timeSinceLastPlayerCollision = 0.0f;
		}
	}
}
