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

    public BigFlyer bigFlyer;

	static public float boundsXmin = -16.5f;
	static public float boundsXmax = 12.3f;
	static public float boundsYmin = -15.0f;
	static public float boundsYmax = 14.7f;

	private bool spawnStupidCitizens;

	public int uuidCount = 0;

    public bool showingFlyer = false;

    private GameMaster gm;

    private int levelIndex; // zero-based

    void OnLevelWasLoaded() {

	}

	// Use this for initialization
	void Start () {
		Debug.Log ("godscript: Start()");
		Time.timeScale = timeScale;
		uuidCount = 0;
		spawnStupidCitizens = false;
		bool policeSpawn = false;
		int scoreToWin = 0;
		int ammo = 6;

		Scene scene = SceneManager.GetActiveScene ();
		if (scene.name.Equals ("TutorialScene")) {
			scoreToWin = 50;
            levelIndex = 0;
			GodScript.boundsXmin = -16.5f;
			GodScript.boundsXmax = 12.3f;
			GodScript.boundsYmin = -15.0f;
			GodScript.boundsYmax = 14.7f;
		} else if (scene.name.Equals ("CityScene")) {
			scoreToWin = 100;
			ammo = 8;
            levelIndex = 1;
			GodScript.boundsXmin = -16.5f;
			GodScript.boundsXmax = 12.3f;
			GodScript.boundsYmin = -15.0f;
			GodScript.boundsYmax = 14.7f;
		} else if (scene.name.Equals ("LargeScene")) {
			scoreToWin = 250;
			ammo = 21;
            levelIndex = 2;
			GodScript.boundsXmin = -45.21f;
			GodScript.boundsXmax = 34.2f;
			GodScript.boundsYmax = 41.4f;
			GodScript.boundsYmin = -39.6f;
			spawnStupidCitizens = true;
			policeSpawn = true;
		}

		// randomize order of spawn points:
		reshuffle(spawnPoints);
		//spawn NPCs:

		foreach(SpawnPoint sp in spawnPoints) {
			GetComponent<NPCFactoryScript> ().spawnNPC (sp, uuidCount, spawnStupidCitizens);
			uuidCount++;
		}

		gm = GameMaster.getInstance ();
		gm.registerGodScript (this);

		gm.setScoreTowin (scoreToWin);
		gm.setPoliceSpawn (policeSpawn);

		gm.notifyLevelStarted ();
		flyerAmmo.setCharacter (player);
		flyerAmmo.AddAmmo (ammo);

        pauseOverlay.gameObject.SetActive(false);
    }

	void reshuffle(SpawnPoint[] points)
	{
		// Knuth shuffle algorithm :: courtesy of Wikipedia :)
		for (int t = 0; t < points.Length; t++ )
		{
			SpawnPoint tmp = points[t];
			int r = Random.Range(t, points.Length);
			points[t] = points[r];
			points[r] = tmp;
		}
	}
	
	// Update is called once per frame
	void Update () {
        if (!showingFlyer)
        {
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
        else
        {
            if (Input.anyKeyDown || Input.GetMouseButton(1))
            {
                bigFlyer.HideFlyer();
                Time.timeScale = timeScale;
                showingFlyer = false;
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
		int k = getRandomSpawnPoint (NPCType.CITIZEN);
		GetComponent<NPCFactoryScript> ().goodCitizenLeft (); // we know a good citizen left city or died, NPCFactory needs to keep track
		// of good citizens to make sure that there is at least one good citizen present in the city
		GetComponent<NPCFactoryScript> ().spawnNPC (spawnPoints [k], uuidCount, spawnStupidCitizens);
		uuidCount++;
	}
	public void spawnRandomPolice() {
		int k = getRandomSpawnPoint (NPCType.POLICE);
		GetComponent<NPCFactoryScript> ().spawnNPC (spawnPoints [k], uuidCount, false);
		uuidCount++;

	}
	private int getRandomSpawnPoint(NPCType npcType) {
		int k = Random.Range (0, spawnPoints.Length-1);
		while (spawnPoints [k].npcType != npcType) { //assume at least one fitting type in spawn points array, otherwise infinite loop.
			k = (k + 1) % spawnPoints.Length;
		}
		return k;
	}

	public void updateScoreLabel(int sc) {
		scoreText.text = "Score: " + sc.ToString () + " / " + GameMaster.getInstance ().getScoreToWin ();
	}

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EndGame()
    {
		AkSoundEngine.PostEvent ("Stop_Atmo", mControl.gameObject);
		AkSoundEngine.PostEvent ("Stop_RevolutionMusic", mControl.gameObject);
        SceneManager.LoadScene("StartMenuScene");
    }

    public void TryShowFlyer()
    {
        if (!gm.checkIfFlyerAlreadyShown(levelIndex))
        {
            bigFlyer.ShowFlyer();
            Time.timeScale = 0;
            StartCoroutine(Wait()); // small timeout so that the flyer does not go away too quickly
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(1);
        showingFlyer = true;
        gm.notifyFlyerShown(levelIndex);
    }
}
