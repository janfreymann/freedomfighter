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
		AkSoundEngine.PostEvent ("Play_buttonclick", gameObject);
		//load tutorial scene
		Debug.Log("LoadTutorial()");
		SceneManager.LoadScene ("TutorialScene");
	}

	public void LoadLargeMap() {
		AkSoundEngine.PostEvent ("Play_buttonclick", gameObject);
		//load large city scene:
		Debug.Log("LoadLargeMap()");
		SceneManager.LoadScene ("LargeScene");
	}


	public void LoadCity() {
		AkSoundEngine.PostEvent ("Play_buttonclick", gameObject);
		//load city scene:
		Debug.Log("LoadCity()");
		SceneManager.LoadScene ("CityScene");
	}
}
