using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFactoryScript : MonoBehaviour {


	public Citizen citizenPrefab;
	public Police policePrefab;
	public Transform gravityFix;
	public Transform exitPosition;

	public void spawnNPC(SpawnPoint sp, int uuidCount) {
		if (sp.npcType == NPCType.CITIZEN) {			
			Citizen citizenInstance = Instantiate (citizenPrefab, sp.startPosition.position, sp.startPosition.rotation) as Citizen;
			citizenInstance.SetUuid (uuidCount);
			citizenInstance.targets = sp.waypoints;
			citizenInstance.transform.parent = gravityFix;
			citizenInstance.godScript = GetComponent<GodScript> ();
			citizenInstance.exitPosition = exitPosition;

			Debug.Log ("spawned citizen");
		} else if (sp.npcType == NPCType.POLICE) {
			Police policeInstance = Instantiate (policePrefab, sp.startPosition.position, sp.startPosition.rotation) as Police;
			policeInstance.SetUuid (uuidCount);
			policeInstance.targets = sp.waypoints;
			policeInstance.transform.parent = gravityFix;
			policeInstance.godScript = GetComponent<GodScript> ();
			Debug.Log ("spawned police");
		}
	}

	void Start() {
	}
	void Update() {
	}
}
