using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour {

	// Use this for initialization
	void OnLevelWasLoaded() {
		Start ();
	}
	void Start () {
		Debug.Log("start() music control");
		AkSoundEngine.SetState ("RevoState", "level1");
		AkSoundEngine.PostEvent ("Play_RevolutionMusic", gameObject);
		AkSoundEngine.PostEvent ("Play_Atmo", gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addCitizen() {
		AkSoundEngine.SetState ("RevoState", "level2");
	}
}
