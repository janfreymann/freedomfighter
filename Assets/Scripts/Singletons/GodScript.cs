﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GodScript : MonoBehaviour {
	public const float arrestThreshold = 2.0f;

	public DynamicText scoreText;

	public PlayerCharacter player;

	public Transform gravityFix;

	public GameObject[] finishObjects;

	public MusicControl mControl;

	private IEnumerator coroutine;

	public SpawnPoint[] spawnPoints;

	public MiniMapControl miniMap;
	public FlyerAmmoManager flyerAmmo;

	static public float boundsXmin = -16.5f;
	static public float boundsXmax = 12.3f;
	static public float boundsYmin = -15.0f;
	static public float boundsYmax = 14.7f;

	public int uuidCount = 0;

	void OnLevelWasLoaded() {

	}

	// Use this for initialization
	void Start () {
		Debug.Log ("godscript: Start()");
		Time.timeScale = 1;
		uuidCount = 0;
		//spawn NPCs:
		foreach(SpawnPoint sp in spawnPoints) {
			GetComponent<NPCFactoryScript> ().spawnNPC (sp, uuidCount);
			uuidCount++;
		}

		GameMaster gm = GameMaster.getInstance ();
		gm.registerGodScript (this);
		gm.notifyLevelStarted ();
		flyerAmmo.AddAmmo (4);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void LoseGame ()
	{
		SceneManager.LoadScene ("GameOverScene");	
		AkSoundEngine.PostEvent ("Stop_atmo_loop2", mControl.gameObject);
		AkSoundEngine.PostEvent ("Stop_RevolutionMusic", mControl.gameObject);
	}
	public void WinGame() {
		SceneManager.LoadScene ("WinGameScene");
		AkSoundEngine.PostEvent ("Stop_atmo_loop2", mControl.gameObject);
		AkSoundEngine.PostEvent ("Stop_RevolutionMusic", mControl.gameObject);
		AkSoundEngine.PostEvent ("Play_win", mControl.gameObject);
		Time.timeScale = 0;
	}
	public void respawnRandomCitizen() {
		int k = Random.Range (0, spawnPoints.Length-1);
		while (spawnPoints [k].npcType != NPCType.CITIZEN) { //assume at leat one citizen in spawn points, otherwise inifinite loop - todo: fix!
			k = (k + 1) % spawnPoints.Length;
		}
		GetComponent<NPCFactoryScript> ().spawnNPC (spawnPoints [k], uuidCount);
		uuidCount++;

	}

	public void updateScoreLabel(int sc) {
		scoreText.SetText ("Score: " + sc.ToString ());
	}


}