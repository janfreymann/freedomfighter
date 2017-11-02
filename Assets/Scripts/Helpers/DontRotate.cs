using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontRotate : MonoBehaviour {

	private Quaternion initRotation;

	// Use this for initialization
	void Start () {
		initRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = initRotation;
	}
}
