using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : Person {
	private const int pointsForExit = 50;
	private const int pointsForFlyer = 10;

	public Sprite citizen_turned;
	public Transform exitPosition;
	public bool turned = false;
	private bool cameraFollowing = false; //true if citizen near exit to win

	public GodScript godScript;

	private GameObject walkingSprite;
	private GameObject bustedSprite;

	// Use this for initialization
	void Start () {
		base.Start ();
		charSpeed = 1.5f;
		walkingSprite = transform.GetChild (0).gameObject;
		bustedSprite = transform.GetChild (1).gameObject;
		selectNextTarget ();
	}

	public void bust() {
		walkingSprite.GetComponent<SpriteRenderer> ().enabled = false;
		bustedSprite.GetComponent<SpriteRenderer> ().enabled = true;
	}
	// Update is called once per frame
	void Update () {
		base.Update ();

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
				godScript.addToScore(pointsForExit);
				AkSoundEngine.PostEvent("Play_score", gameObject);

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
				godScript.addToScore(pointsForFlyer);
				AkSoundEngine.PostEvent ("Play_pickupFlyer", gameObject);
				AkSoundEngine.PostEvent ("Play_shh", gameObject);

				transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = citizen_turned;
				this.tag = "Turned";
				currentTarget = exitPosition.position;
				agent.SetDestination (currentTarget);

				Destroy (collision.gameObject);
			}		
		}
	}
}
