using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitMusicStates : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AkSoundEngine.SetState("CitizenState", "normal");
		AkSoundEngine.SetState ("PlayerState", "normal");
		AkSoundEngine.SetState("LevelState", "SmallCity");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
