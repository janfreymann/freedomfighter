using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCFactoryScript : MonoBehaviour {


	public Citizen citizenPrefab;
	public Police policePrefab;
	public Transform gravityFix;
	public Transform exitPosition;

	public int goodCitizens = 0;

	public void spawnNPC(SpawnPoint sp, int uuidCount, bool spawnStupidCitizens) {
		if (sp.npcType == NPCType.CITIZEN) {			
			Citizen citizenInstance = Instantiate (citizenPrefab, sp.startPosition.position, sp.startPosition.rotation) as Citizen;
			citizenInstance.SetUuid (uuidCount);
			citizenInstance.targets = sp.waypoints;
			citizenInstance.transform.parent = gravityFix;
			citizenInstance.godScript = GetComponent<GodScript> ();
			citizenInstance.exitPosition = exitPosition;
			citizenInstance.GetComponent<NavMeshAgent> ().Warp (sp.startPosition.position);
			StartCoroutine(HoldNavAgent(citizenInstance.GetComponent<NavMeshAgent>()));
			if ((spawnStupidCitizens && goodCitizens > 0)) { // always keep between one
				if ((Random.Range (0, 100) > 30) || (goodCitizens >= 3)) { // and three good citizens on the map
					citizenInstance.tag = "Stupid";
				}
			} else {
				goodCitizens++;
			}
			citizenInstance.GetComponent<NavMeshAgent> ().enabled = true;

			Debug.Log ("spawned citizen");
		} else if (sp.npcType == NPCType.POLICE) {
			Police policeInstance = Instantiate (policePrefab, sp.startPosition.position, sp.startPosition.rotation) as Police;
			policeInstance.SetUuid (uuidCount);
			policeInstance.targets = sp.waypoints;
			policeInstance.transform.parent = gravityFix;
			policeInstance.godScript = GetComponent<GodScript> ();
			policeInstance.GetComponent<NavMeshAgent> ().Warp (sp.startPosition.position);
			StartCoroutine(HoldNavAgent(policeInstance.GetComponent<NavMeshAgent>()));
			Debug.Log ("spawned police");
		}
	}
	public IEnumerator HoldNavAgent(NavMeshAgent pathFinder) { 
		yield return new WaitForSeconds(0.1f); 
		pathFinder.enabled = true;
	}

	public void goodCitizenLeft() { // or died...
		goodCitizens--;
	}

	void Start() {
	}
	void Update() {
	}
}
