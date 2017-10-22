using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicControl : MonoBehaviour {

	// Use this for initialization
	void OnLevelWasLoaded() {
		Start ();
	}
	void Start () {
		Debug.Log("start() music control");
		//AkSoundEngine.SetState ("RevoState", "level1");
		AkSoundEngine.SetState("CitizenState", "normal");
		AkSoundEngine.SetState ("PlayerState", "normal");

		Scene scene = SceneManager.GetActiveScene();
		Debug.Log ("active scene is :" + scene.name);
		if (scene.name.Equals ("CityScene")) { //todo: add case distinction for third map
			AkSoundEngine.PostEvent ("Play_RevolutionMusic", gameObject);
			AkSoundEngine.SetRTPCValue ("AtmoSize", 100); //large map
		} else {
			AkSoundEngine.SetRTPCValue ("AtmoSize", 20); //small map
		}


		AkSoundEngine.PostEvent ("Play_Atmo", gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addCitizen() {
		AkSoundEngine.SetState ("RevoState", "level2"); //deprecated
	}
}