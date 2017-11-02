using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToStartMenu : MonoBehaviour {

	float timeWaiting = 0.0f;
	float restartTime = 5.0f;

	// Use this for initialization
	void Start () {
		Time.timeScale = GodScript.timeScale;
	}
	
	// Update is called once per frame
	void Update () {
		timeWaiting += Time.deltaTime;
		if (timeWaiting > restartTime) {
			timeWaiting = 0.0f;
			BackToMenu ();
		}

		if ((Input.anyKey) && (timeWaiting > 1.0f)) {
			BackToMenu ();
		}
	}
	private void BackToMenu() {
		SceneManager.LoadScene ("StartMenuScene");
	}
}
