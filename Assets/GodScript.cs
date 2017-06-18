using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodScript : MonoBehaviour {
	public int score = 0;
	public const float arrestThreshold = 2.0f;
	public const int scoreToWin = 50;

	public DynamicText scoreText;

	public PlayerCharacter player;

	public Transform gravityFix;

	public List<Police> policePrefabs;
	//public List<List<Vector2>> policePaths;

	public List<Citizen> citizenPrefabs;
	//public List<List<Vector2>> citizenPaths;

	public GameObject[] finishObjects;

	public MusicControl mControl;

	public Button restartButton;
	private float buttonX;

	public Sprite winScreen;
	public Sprite loseScreen;
	private IEnumerator coroutine;

	private float waitingForButton;
	private bool willShowButton;

	private bool finished;

	public GameObject gameOver;

	void OnLevelWasLoaded() {
		//player.alive = true;
		Debug.Log("godscript: OnLevelWasLoaded()");
		Start ();
	}

	// Use this for initialization
	void Start () {
		Debug.Log ("godscript: Start()");
		willShowButton = false;
		finished = false;
		buttonX = restartButton.transform.position.x;
		Time.timeScale = 1;
		gameOver.SetActive (false);
		UpdateScore (0);


	/*	RectTransform rt = restartButton.GetComponent<RectTransform> ();
		Vector3 vec = new Vector3(10000f, rt.position.y, rt.position.z);
		rt.position = vec; */
		restartButton.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("godscript: Update() " + Time.timeScale + " : " + player.alive);
		if (Time.timeScale == 0 && player.alive == false) {
			ShowFinished ();
		} else {
			bool stillAlive = false;
			foreach (Citizen c in citizenPrefabs) {
				if (c != null)
					stillAlive = true;
			}
			if ((!stillAlive) && (score < scoreToWin)){
				Time.timeScale = 0;
				ShowFinished ();
			}
		}

		if (willShowButton) {
			waitingForButton += Time.unscaledDeltaTime;
			//Debug.Log ("waitingforbutton: " + waitingForButton);
			if (waitingForButton > 2.0f) {
				Debug.Log ("show button!");
				//RectTransform rt = restartButton.GetComponent<RectTransform> ();
				//Vector3 vecShow = new Vector3 (buttonX, rt.position.y, rt.position.z);
				//rt.position = vecShow;
				restartButton.gameObject.SetActive(true);
				waitingForButton = 0.0f;
				willShowButton = false;
			}
		}

	}
	private IEnumerator WaitAndShowButton(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
	}

	private void ShowFinished ()
	{
		if (finished)
			return;

		gameOver.GetComponent<SpriteRenderer> ().sprite = loseScreen;
		gameOver.SetActive (true);
		AkSoundEngine.PostEvent ("Stop_atmo_loop2", mControl.gameObject);
		AkSoundEngine.PostEvent ("Stop_RevolutionMusic", mControl.gameObject);
		willShowButton = true;
		waitingForButton = 0.0f;
		finished = true;
		//restartButton.transform.position = new Vector3 (buttonX, restartButton.transform.position.y, restartButton.transform.position.z);
	}
	private void WinGame() {
		gameOver.GetComponent<SpriteRenderer> ().sprite = winScreen;
		gameOver.SetActive (true);
		AkSoundEngine.PostEvent ("Stop_atmo_loop2", mControl.gameObject);
		AkSoundEngine.PostEvent ("Stop_RevolutionMusic", mControl.gameObject);
		AkSoundEngine.PostEvent ("Play_win", mControl.gameObject);
		Time.timeScale = 0;
	}
		

	private void UpdateScore(int d)
	{
		score += d;
		scoreText.SetText ("Score: " + score.ToString());

		if (score >= scoreToWin) {
			WinGame ();
		}
	}

	public void addToScore(int d) {
		UpdateScore (d);

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
