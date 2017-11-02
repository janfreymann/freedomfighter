using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowPlayer : MonoBehaviour {

	public Transform player;
	public Transform originalPlayer = null;
	private const float cameraSpeed = 15.0f;
	float xBound = 5.0f;
	float yBound = 8.0f;
	float cameraHeight = -16.0f;

	// Use this for initialization
	void Start () {
		followPlayer (false);

		Scene scene = SceneManager.GetActiveScene();
		if (scene.name.Equals ("LargeScene")) {
			xBound = 24.0f;
			yBound = 30.0f;
			cameraHeight = -24.0f;
		} 
	}
	
	// Update is called once per frame
	void LateUpdate () {
		followPlayer (true);
	}
	void followPlayer(bool animate) {
		Vector3 followPlayerPos = new Vector3 (player.localPosition.x, player.localPosition.y, cameraHeight);
		if (followPlayerPos.x < -xBound) {
			followPlayerPos.x = -xBound;
		} else if (followPlayerPos.x > xBound) {
			followPlayerPos.x = xBound;
		}

		if (followPlayerPos.y < -yBound) {
			followPlayerPos.y = -yBound;
		} else if (followPlayerPos.y > yBound) {
			followPlayerPos.y = yBound;
		}

		if (animate) {
			Vector3 currentPosition = transform.localPosition;
			transform.localPosition = Vector3.MoveTowards (currentPosition, followPlayerPos, cameraSpeed * Time.deltaTime);
		} else { //no animation, jump to camera position
			transform.localPosition = followPlayerPos;
		}

	}
	public void setPlayer(Transform p) {
		if (originalPlayer == null) {
			originalPlayer = p;
		}
		player = p;
	}
	public void resetPlayer() {
		player = originalPlayer;
	}
}
