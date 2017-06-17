using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodScript : MonoBehaviour {

	public Police police;
	public Citizen citizen;

	public List<Person> policePrefabs;
	public List<List<Vector2>> policePaths;

	public List<Person> citizenPrefabs;
	public List<List<Vector2>> citizenPaths;

	// Use this for initialization
	void Start () {

		policePaths = new List<List<Vector2>>{ };
		policePaths.Add(new List<Vector2>{ new Vector2 (1.0f, -1.25f) });
		//SpawnNPCs (policePaths, policePrefabs);
		SpawnNPCs (policePaths, "p");

		citizenPaths = new List<List<Vector2>>{ };
		citizenPaths.Add(new List<Vector2>{ new Vector2 (1.0f, -1.5f), new Vector2(-1.0f, -1.5f) });
		//SpawnNPCs (citizenPaths, citizenPrefabs);
		SpawnNPCs (citizenPaths, "c");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnNPCs(List<List<Vector2>> paths, string npcType){
		//void SpawnNPCs(List<List<Vector2>> paths, out List<Person> people){
		for(int i = 0; i < paths.Count; i++)		{
			
			if (npcType.Equals ("p")) {
				var policeInstance = Instantiate (police, new Vector3 (paths [i] [0].x, paths [i] [0].y, 0.0f), Quaternion.identity) as Police;		
				policeInstance.targets = paths [i].ToArray ();
			} else if (npcType.Equals("c")) {
				var citizenInstance = Instantiate (citizen, new Vector3 (paths [i] [0].x, paths [i] [0].y, 0.0f), Quaternion.identity) as Citizen;		
				citizenInstance.targets = paths [i].ToArray ();
			}

			//people.Add(p);
		}
	}
}
