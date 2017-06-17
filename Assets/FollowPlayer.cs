using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	public Transform player;

	// Use this for initialization
	void Start () {
		followPlayer ();
	}
	
	// Update is called once per frame
	void Update () {
		followPlayer ();
	}
	void followPlayer() {
		Vector3 followPlayerPos = new Vector3 (player.localPosition.x, player.localPosition.y, -16.0f);
		transform.localPosition = followPlayerPos;
	}
}
