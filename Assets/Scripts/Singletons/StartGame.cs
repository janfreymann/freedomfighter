using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void LoadTutorial() {
		//load tutorial scene
		Debug.Log("LoadTutorial()");
		SceneManager.LoadScene ("TutorialScene");
	}

	public void LoadLargeMap() {
		//todo
	}


	public void LoadCity() {
		//load city scene:
		Debug.Log("LoadCity()");
		SceneManager.LoadScene ("CityScene");
	}
}
