using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GodScript : MonoBehaviour {

    public const float timeScale = 1;
    
	public const float arrestThreshold = 2.0f;

	public Text scoreText;

	public PlayerCharacter player;

	public Transform gravityFix;

	public GameObject[] finishObjects;

	public MusicControl mControl;

	public SpawnPoint[] spawnPoints;

	public MiniMapControl miniMap;

	public FlyerAmmoManager flyerAmmo;

    public GameObject pauseOverlay;

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
		Time.timeScale = timeScale;
		uuidCount = 0;
		//spawn NPCs:
		foreach(SpawnPoint sp in spawnPoints) {
			GetComponent<NPCFactoryScript> ().spawnNPC (sp, uuidCount);
			uuidCount++;
		}

		GameMaster gm = GameMaster.getInstance ();
		gm.registerGodScript (this);
		Scene scene = SceneManager.GetActiveScene ();
		if (scene.name.Equals ("TutorialScene")) {
			gm.setScoreTowin (50);
            GodScript.boundsXmin = -16.5f;
            GodScript.boundsXmax = 12.3f;
            GodScript.boundsYmin = -15.0f;
            GodScript.boundsYmax = 14.7f;
        } else if (scene.name.Equals ("CityScene")) {
			gm.setScoreTowin (100);
            GodScript.boundsXmin = -16.5f;
            GodScript.boundsXmax = 12.3f;
            GodScript.boundsYmin = -15.0f;
            GodScript.boundsYmax = 14.7f;
        } else if (scene.name.Equals ("LargeScene")) {
			gm.setScoreTowin (150);
            GodScript.boundsXmin = -45.21f;
            GodScript.boundsXmax = 35.63f;
            GodScript.boundsYmax = 41.4f;
            GodScript.boundsYmin = -39.6f;
        }

		gm.notifyLevelStarted ();
		flyerAmmo.AddAmmo (6);

        pauseOverlay.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (Time.timeScale > 0)
            {
                Time.timeScale = 0;
                pauseOverlay.gameObject.SetActive(true);
            }
            else
            {
                Time.timeScale = timeScale;
                pauseOverlay.gameObject.SetActive(false);
            }           
        }
    }

	public void LoseGame ()
	{
		SceneManager.LoadScene ("GameOverScene");	
		AkSoundEngine.PostEvent ("Stop_Atmo", mControl.gameObject);
		AkSoundEngine.PostEvent ("Stop_RevolutionMusic", mControl.gameObject);
	}
	public void WinGame() {
		SceneManager.LoadScene ("WinGameScene");
		AkSoundEngine.PostEvent ("Stop_Atmo", mControl.gameObject);
		AkSoundEngine.PostEvent ("Stop_RevolutionMusic", mControl.gameObject);
		AkSoundEngine.PostEvent ("Play_win", mControl.gameObject);
		Time.timeScale = 0;
	}
	public void respawnRandomCitizen() {
		//AkSoundEngine.PostEvent ("Play_shh", mControl.gameObject);
		int k = Random.Range (0, spawnPoints.Length-1);
		while (spawnPoints [k].npcType != NPCType.CITIZEN) { //assume at least one citizen in spawn points, otherwise inifinite loop - todo: fix!
			k = (k + 1) % spawnPoints.Length;
		}
		GetComponent<NPCFactoryScript> ().spawnNPC (spawnPoints [k], uuidCount);
		uuidCount++;
	}

	public void updateScoreLabel(int sc) {
		scoreText.text = "Score: " + sc.ToString ();
	}

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EndGame()
    {
        SceneManager.LoadScene("StartMenuScene");
    }
}
