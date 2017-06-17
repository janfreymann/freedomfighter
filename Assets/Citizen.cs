using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : Person {
	private const int pointsForExit = 50;
	private const int pointsForFlyer = 10;

	public Sprite citizen_turned;
	public Transform exitPosition;

	private bool turned = false;

	public GodScript godScript;

	// Use this for initialization
	void Start () {
		base.Start ();
		charSpeed = 1.5f;
		selectNextTarget ();
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();

		float distance2target = Vector3.Distance (transform.position, currentTarget);
		Debug.Log (distance2target);

		if (distance2target < 1.5f) {

			if (turned)  // near exit
			{
				Debug.Log ("citizen reached exit");
				godScript.score += pointsForExit;
				//godScript.scoreText.text = godScript.score.ToString();
				Destroy(gameObject);
			} else
			{
				Debug.Log ("citizen select next target");
				selectNextTarget ();
			}
		}
	}
	public void OnTriggerEnter(Collider collision) {		
		if (!turned) {
			turned = true;
			Debug.Log ("citizen found flyer");
			godScript.score += pointsForFlyer;
			//godScript.scoreText.text = godScript.score.ToString();

			transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = citizen_turned;
			this.tag = "Turned";
			currentTarget = exitPosition.position;
			agent.SetDestination (currentTarget);
		}
	}
}
