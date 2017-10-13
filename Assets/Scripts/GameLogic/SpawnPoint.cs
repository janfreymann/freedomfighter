using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

	public Transform[] waypoints;
	public Transform startPosition;
	public NPCType npcType;

	void Awake() {
		startPosition = transform;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
public enum NPCType {
	CITIZEN,
	POLICE
}
