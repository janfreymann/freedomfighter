﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : Person {

	private const float distanceToFollow = 5f;
	//public PlayerCharacter player;
	public Person fugitive;

	private const float distanceToLose = 10f;
	private const float distanceToBust = 2f;

	public float distanceToFugitive = 0.0f;
	public bool followingFugitive;

	private float lastWhistle = 999.0f;

	public GodScript godObject;

	private GameObject patrolSprite;
	private GameObject chaseSprite;

	// Use this for initialization
	void Start () {
		base.Start ();
		chaseSprite = transform.GetChild (0).gameObject;
		patrolSprite = transform.GetChild (1).gameObject;
		followingFugitive = false;
		selectNextTarget ();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
		lastWhistle += Time.deltaTime;

		followingFugitive = false;
		float distanceToClosestFugitive = 1000f;
		Person closestFugitive = null;
		foreach (Citizen citizen in godObject.citizenPrefabs) {
			if (citizen!= null && citizen.turned) {
				float distanceToCitizen = Vector3.Distance (transform.position, citizen.transform.position);
				Debug.Log ("Distance to citizen " + distanceToCitizen.ToString ());
				if ((distanceToCitizen < distanceToFollow) && (distanceToCitizen <= distanceToClosestFugitive)) {
					closestFugitive = citizen;
					distanceToClosestFugitive = distanceToCitizen;
					followingFugitive = true;
				}	
			}
		}

		float distanceToPlayer = Vector3.Distance (transform.position, godObject.player.transform.position);
		if ((distanceToPlayer < distanceToFollow) && (distanceToPlayer <= distanceToClosestFugitive)) {
			Debug.Log ("Distance to player " + distanceToPlayer.ToString ());
			closestFugitive = godObject.player;
			distanceToClosestFugitive = distanceToPlayer;
			if ((!followingFugitive) && (lastWhistle > 25.0f)) {
				AkSoundEngine.PostEvent ("Play_WhistlePlayer", gameObject);
				lastWhistle = 0.0f;
			}
				
			followingFugitive = true;
		}

		fugitive = closestFugitive;



		if (followingFugitive) { //chasing mode
			patrolSprite.GetComponent<SpriteRenderer>().enabled = false;
			chaseSprite.GetComponent<SpriteRenderer> ().enabled = true;
			Debug.Log ("closest: " + fugitive.tag);
			currentTarget = fugitive.transform.position;
			agent.SetDestination (currentTarget);
			distanceToFugitive = Vector2.Distance (currentTarget, transform.position);
			if (Vector3.Distance (currentTarget, transform.position) > distanceToLose) {
				followingFugitive = false;
				Debug.Log ("police lost fugitive, select patrol target");
				selectNextTarget ();
			} else if (Vector3.Distance (currentTarget, transform.position) < distanceToBust) {
				//Debug.Log ("busted! " + fugitive.tag + " distance " + );
				if (fugitive.tag.Equals ("Turned")) {
					fugitive.tag = "Busted"; //prevent multiple busts
					AkSoundEngine.PostEvent ("Play_BustCititzen", gameObject);
					Destroy (fugitive.gameObject, 2.0f);
					Citizen cit = (Citizen) fugitive;
					cit.bust ();
				} else if (fugitive.tag.Equals ("Player")) {
					AkSoundEngine.PostEvent ("Play_bust_player", gameObject);
					//todo animation
					Destroy (fugitive.gameObject);
				}
			}
		} else { //patrol mode
			patrolSprite.GetComponent<SpriteRenderer>().enabled = true;
			chaseSprite.GetComponent<SpriteRenderer> ().enabled = false;
			Debug.Log ("Continue patrolling");
			float distance2target = Vector3.Distance (transform.position, currentTarget);
			if (distance2target < 1.0f) {
				Debug.Log ("police select next target");
				selectNextTarget ();
			}
		}

	}
		
	public void OnTriggerEnter(Collider collision) {
		if (collision.gameObject.tag.Equals ("Flyer")) { // it is a flyer!
			Destroy (collision.gameObject);
			Debug.Log ("Policeman found flyer");
			AkSoundEngine.PostEvent ("Play_tearFlyer", gameObject);
		}
	}
}
