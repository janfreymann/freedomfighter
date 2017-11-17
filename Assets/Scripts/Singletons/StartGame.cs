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
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKey(KeyCode.Keypad1) || (Input.GetKey(KeyCode.Alpha1)))
        {
            LoadTutorial();
        }
        else if (Input.GetKey(KeyCode.Keypad2) || (Input.GetKey(KeyCode.Alpha2)))
        {
            LoadCity();
        }
        else if (Input.GetKey(KeyCode.Keypad3) || (Input.GetKey(KeyCode.Alpha3)))
        {
            LoadLargeMap();
        }
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
