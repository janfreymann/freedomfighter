using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : Person {

	private const float distanceToFollow = 7f;
	//public PlayerCharacter player;
	public Person fugitive = null;

	private const float distanceToLose = 8f;
	private const float distanceToBust = 2.5f;

	public float distanceToFugitive = 0.0f;
	public bool followingFugitive = false;
	private bool chasingHecker = false;

	private float lastWhistle = 999.0f;

	private GameObject patrolSprite;
	private GameObject chaseSprite;

	// Use this for initialization
	new void Start () {
		base.Start ();
		chaseSprite = transform.GetChild (0).gameObject;
		patrolSprite = transform.GetChild (1).gameObject;
		godScript.miniMap.addActor (this);
		selectNextTarget ();
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
		lastWhistle += Time.deltaTime;

		float distanceToClosestFugitive = 1000f;
		Person closestFugitive = null;
		Citizen[] citizens = FindObjectsOfType<Citizen> (); //todo: efficient??

        if (followingFugitive && fugitive == null) // followed citizen escaped
        {
            followingFugitive = false;
        }

        if (!chasingHecker) // only look for citizens when not chasing player
        {
            foreach (Citizen citizen in citizens)
            {
                if (citizen != null && citizen.turned)
                {
                    float distanceToCitizen = Vector3.Distance(transform.position, citizen.transform.position);
                    //	Debug.Log ("Distance to citizen " + distanceToCitizen.ToString ());
                    if ((distanceToCitizen < distanceToFollow) && (distanceToCitizen <= distanceToClosestFugitive))
                    {
                        closestFugitive = citizen;
                        distanceToClosestFugitive = distanceToCitizen;
                    }
                }
            }
        }

		float distanceToPlayer = Vector3.Distance (transform.position, godScript.player.transform.position);

		if (distanceToPlayer < distanceToFollow) {
		//	Debug.Log ("Distance to player " + distanceToPlayer.ToString ());
			closestFugitive = godScript.player;
			if (!chasingHecker) {
				GameMaster.getInstance ().notifyHeckerChased ();
				chasingHecker = true;
			}
			distanceToClosestFugitive = distanceToPlayer;
			if ((!followingFugitive) && (lastWhistle > 25.0f)) {
				AkSoundEngine.PostEvent ("Play_WhistlePlayer", gameObject);
				lastWhistle = 0.0f;
			}
		}
        
        if (closestFugitive != null)
        {
            fugitive = closestFugitive; // only override when another one found
            followingFugitive = true;
        }

		if (followingFugitive) { //chasing mode
			patrolSprite.GetComponent<SpriteRenderer>().enabled = false;
			chaseSprite.GetComponent<SpriteRenderer> ().enabled = true;
			//Debug.Log ("closest: " + fugitive.tag);
			currentTarget = fugitive.transform.position;
			agent.SetDestination (currentTarget);
			distanceToFugitive = Vector3.Distance (currentTarget, transform.position);

			if (distanceToFugitive > distanceToLose) {
				followingFugitive = false;
                fugitive = null;
				Debug.Log ("police lost fugitive, select patrol target");
				selectNextTarget ();
			} else if (distanceToFugitive < distanceToBust) {
				//Debug.Log ("busted! " + fugitive.tag + " distance " + );
				if (fugitive.tag.Equals ("Turned")) {
					fugitive.tag = "Busted"; //prevent multiple busts
					AkSoundEngine.PostEvent ("Play_BustCititzen", gameObject);
					Destroy (fugitive.gameObject, 2.0f);
					Citizen cit = (Citizen) fugitive;
					godScript.miniMap.removeActor (cit);
					cit.bust ();
                    followingFugitive = false;
                    fugitive = null;
				} else if (fugitive.tag.Equals ("Player")) {
					fugitive.tag = "Busted";
					AkSoundEngine.PostEvent ("Play_bust_player", gameObject);
					PlayerCharacter pl = fugitive as PlayerCharacter;
					pl.alive = false;
					Time.timeScale = 0;
                    followingFugitive = false;
                    fugitive = null;
                    GameMaster.getInstance ().notifyHeckerDied ();
				}
			}
		} else { //patrol mode
			if (chasingHecker) {
				GameMaster.getInstance ().notifyHeckerChaseStopped ();
				chasingHecker = false;
			}
			patrolSprite.GetComponent<SpriteRenderer>().enabled = true;
			chaseSprite.GetComponent<SpriteRenderer> ().enabled = false;
			float distance2target = Vector3.Distance (transform.position, currentTarget);
			if (distance2target < 2f) {
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
