using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodScript : MonoBehaviour {

	public Transform policePrefab;

	// Use this for initialization
	void Start () {
		SpawnPolicemen ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnPolicemen(){
		Transform p = Instantiate (policePrefab, new Vector3 (0.0f, 0.0f, 0.0f), Quaternion.identity);
	}
}
