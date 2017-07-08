using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFactoryScript : MonoBehaviour {


	public Citizen citizenPrefab;
	public Police policePrefab;
	public Transform gravityFix;
	public Transform exitPosition;

	public void spawnNPC(SpawnPoint sp) {
		if (sp.npcType == NPCType.CITIZEN) {			
			Citizen citizenInstance = Instantiate (citizenPrefab, sp.startPosition.position, sp.startPosition.rotation) as Citizen;
			citizenInstance.targets = sp.waypoints;
			citizenInstance.transform.parent = gravityFix;
			citizenInstance.godScript = GetComponent<GodScript> ();
			citizenInstance.exitPosition = exitPosition;

			Debug.Log ("spawned citizen");
		} else if (sp.npcType == NPCType.POLICE) {
			Police policeInstance = Instantiate (policePrefab, sp.startPosition.position, sp.startPosition.rotation) as Police;
			policeInstance.targets = sp.waypoints;
			policeInstance.godScript = GetComponent<GodScript> ();
			Debug.Log ("spawned police");
		}
	}

	void Start() {
	}
	void Update() {
	}
}
