﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

	public GameObject[] livingObjects;
	public Image tutScreen;

	private bool skipStartScreen = false;

	void OnLevelWasLoaded() {
		Debug.Log ("living object count: " + livingObjects.Length);
		skipStartScreen = true;
		startGame ();
	}

	// Use this for initialization
	void Start () {
		if (skipStartScreen)
			return;

		//else:
		foreach (GameObject go in livingObjects) {
			go.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void startGame() {
		foreach (GameObject go in livingObjects) {
			go.SetActive (true);
		}
		RectTransform rt = GetComponent<RectTransform> ();
		Vector3 away = new Vector3(10000.0f, rt.localPosition.y, rt.localPosition.z);
		rt.position = away;
		tutScreen.enabled = false;
	}
}