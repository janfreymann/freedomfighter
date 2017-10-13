using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Reload : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Space)) { //works only when game object (restart button) is active.
			reloadScene ();
		}
	}

	public void reloadScene() {
		SceneManager.LoadScene ("CityScene");
	}
}
