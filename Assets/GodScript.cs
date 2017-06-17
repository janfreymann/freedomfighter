using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodScript : MonoBehaviour {
	public int score;

	public DynamicText scoreText;

	public Police police;
	public Citizen citizen;

	public Transform gravityFix;

	//public List<Person> policePrefabs;
	//public List<List<Vector2>> policePaths;

	//public List<Person> citizenPrefabs;
	//public List<List<Vector2>> citizenPaths;

	// Use this for initialization
	void Start () {
		score = 0;
		//policePaths = new List<List<Vector2>>{ };
		//policePaths.Add(new List<Vector2>{ new Vector2 (3.0f, 3.0f) });
		//SpawnNPCs (policePaths, policePrefabs);
	//	SpawnNPCs (policePaths, "p");

		//citizenPaths = new List<List<Vector2>>{ };
		//citizenPaths.Add(new List<Vector2>{ new Vector2 (12.0f, 12.0f), new Vector2(5.0f, 5.0f) });
		//SpawnNPCs (citizenPaths, citizenPrefabs);
	//	SpawnNPCs (citizenPaths, "c");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addToScore(int d) {
		score += d;
		scoreText.SetText (score.ToString ());

		if (score > 70) {
			AkSoundEngine.SetState ("RevoState", "level5");
		}
		else if (score > 50) {
			AkSoundEngine.SetState ("RevoState", "level4");
		}
		else if (score > 20) {
			AkSoundEngine.SetState ("RevoState", "level3");
		}
		else if (score > 0) {
			AkSoundEngine.SetState ("RevoState", "level2");
		}

	}

	/*void SpawnNPCs(List<List<Vector2>> paths, string npcType){
		//void SpawnNPCs(List<List<Vector2>> paths, out List<Person> people){
		for(int i = 0; i < paths.Count; i++)		{	
			if (npcType.Equals ("p")) {				
				var policeInstance = Instantiate (police, new Vector3 (0.0f, 0.0f, 0.0f), Quaternion.identity) as Police;		
				policeInstance.transform.parent = gravityFix;
				policeInstance.transform.localPosition = new Vector3 (paths [i] [0].x, paths [i] [0].y, 1.0f);
				policeInstance.targets = paths [i].ToArray ();
			} else if (npcType.Equals("c")) {
				var citizenInstance = Instantiate (citizen, new Vector3 (0.0f, 0.0f, 0.0f), Quaternion.identity) as Citizen;		
				citizenInstance.transform.parent = gravityFix;
				citizenInstance.transform.localPosition = new Vector3 (paths [i] [0].x, paths [i] [0].y, 0.1f);
				citizenInstance.targets = paths [i].ToArray ();
			}

			//people.Add(p);
		}
	}*/
}
