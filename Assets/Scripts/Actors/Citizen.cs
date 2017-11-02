using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Citizen : Person {

	public Sprite citizen_turned;
	public Transform exitPosition;
	public bool turned = false;
	private bool cameraFollowing = false; //true if citizen near exit to win


	private GameObject walkingSprite;
	private GameObject bustedSprite;

	private float timeSinceLastPlayerCollision = 100.0f;

    // Use this for initialization
    new void Start () {
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
	new void Update () {
		base.Update ();
		timeSinceLastPlayerCollision += Time.deltaTime;

		if (tag.Equals ("Busted")) {
			agent.isStopped = true;
			return;
		}


		float distance2target = Vector3.Distance (transform.position, currentTarget);
		//Debug.Log (distance2target);

		if ((turned) && (!cameraFollowing) && (distance2target < 6.0f)) {
			Debug.Log ("camera follows citizen - currently disabled");
			//cameraFollowing = true;
			//Camera.current.gameObject.GetComponent<FollowPlayer> ().setPlayer (transform);
		}
		if (distance2target < 2f) {
			if (turned)  // close enough to exit to win
			{
				Debug.Log ("citizen reached exit");
				GameMaster.getInstance ().notifyTurnedCitizenLeft ();
				AkSoundEngine.PostEvent("Play_score", gameObject);
				godScript.miniMap.removeActor (this);

				/*if (cameraFollowing) {
					cameraFollowing = false;
					Camera.current.gameObject.GetComponent<FollowPlayer> ().resetPlayer ();
				}*/

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

                Debug.Log("citizen found flyer");

                this.tag = "Turned";

				agent.speed = 3.0f;

                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = citizen_turned;
                
				GameMaster.getInstance ().notifyCitizenTurned();
				AkSoundEngine.PostEvent ("Play_pickupFlyer", gameObject);

				godScript.miniMap.removeActor (this); //quickly remove and add to change icon
                godScript.miniMap.addActor (this); //new actor will be loaded with citizen turned icon on mini map
			
				currentTarget = exitPosition.position;
				agent.SetDestination (currentTarget);

				Destroy (collision.gameObject);
			}		
		}
	}
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
