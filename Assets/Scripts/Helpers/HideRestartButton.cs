using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideRestartButton : MonoBehaviour {
	public Button restartButton;
	float timeHidden;
	const float delayTime = 3.0f;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1.0f;
		Debug.Log ("Start(): GameOverScene");
		timeHidden = 0.0f;
		restartButton.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		timeHidden += Time.deltaTime;
		if (timeHidden > delayTime) {
			restartButton.gameObject.SetActive (true);
		}
	}
}
