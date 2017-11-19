using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerAmmoAnimation : MonoBehaviour {

	private bool flying = false;
	private Vector3 target;
	private Vector3 worldpos;
	private PlayerCharacter character;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (flying) {
			//Transform rt = GetComponent<Transform> ();
			//Vector3 pos = rt.position;
			//rt.position = Vector3.MoveTowards (pos, target, 10.0f * Time.deltaTime);
			RectTransform rt = GetComponent<RectTransform>();
			rt.position = Vector3.MoveTowards (rt.position, target, Screen.height * 2f * Time.deltaTime);

			if (Vector3.Distance (rt.position, target) < 50.0f) {
				character.showDroppedFlyer (worldpos);
				Destroy (this.gameObject);
			}
		}
	}
	public void startFlyingTo(Vector3 target, PlayerCharacter character) {
		flying = true;
		worldpos = target;
		this.character = character;
		this.target = Camera.main.WorldToScreenPoint (target);
	}
}
