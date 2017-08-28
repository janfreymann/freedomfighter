﻿using System.Collections;
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
		if ((Input.GetKeyUp (KeyCode.Space)) ||  (Input.GetMouseButtonUp (0))) {
			LoadCity ();
		}
	}

	public void LoadCity() {
		//load city scene:
		Debug.Log("LoadCity()");
		SceneManager.LoadScene ("CityScene");
	}
}
