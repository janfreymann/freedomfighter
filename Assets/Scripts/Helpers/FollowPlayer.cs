using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	public Transform player;
	private const float cameraSpeed = 10.0f;

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
		if (followPlayerPos.x < -5.0f) {
			followPlayerPos.x = -5.0f;
		} else if (followPlayerPos.x > 5.0f) {
			followPlayerPos.x = 5.0f;
		}

		if (followPlayerPos.y < -8.0f) {
			followPlayerPos.y = -8.0f;
		} else if (followPlayerPos.y > 8.0f) {
			followPlayerPos.y = 8.0f;
		}

		Vector3 currentPosition = transform.localPosition;
		transform.localPosition = Vector3.MoveTowards (currentPosition, followPlayerPos, cameraSpeed * Time.deltaTime);
	}
	public void setPlayer(Transform p) {
		player = p;
	}
}
