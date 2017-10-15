using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster {

	private const int pointsForExit = 50;
	private const int pointsForFlyer = 10;
	private const int scoreToWin = 50;

	//cross-level game master
	//observes GodScript of each level
	//receives notifications of score events
	//triggers game over / game win

	private GodScript godScript; //current god script running in level
	private FlyerAmmoManager flyerAmmoManager;
	private int currentScore;

	private static GameMaster gmInstance;


	public void registerGodScript(GodScript gs) {
		godScript = gs;
	}
	public void registerFlyerAmmoManager(FlyerAmmoManager fam) {
		flyerAmmoManager = fam;
	}
	public bool CheckAndDropFlyer() {
		bool result = flyerAmmoManager.checkAndDropFlyer ();
		Debug.Log ("checkAndDropFlyer(): " + result);
		return result;
	}

	public void notifyLevelStarted() {
		currentScore = 0;
		updateScoreRoutine ();
	}
	public void notifyTurnedCitizenLeft() {
		currentScore += pointsForExit;
		updateScoreRoutine ();
	}

	public void notifyTurnedCitizenDied() {
		godScript.respawnRandomCitizen ();
	}
	public void notifyCitizenTurned() {
		currentScore += pointsForFlyer;
		updateScoreRoutine ();
	}

	public void notifyHeckerDied() {
		godScript.LoseGame ();
	}

	public static GameMaster getInstance() { //singleton
		if (gmInstance == null) {
			gmInstance = new GameMaster ();
		}
		return gmInstance;
	}
	private void updateScoreRoutine() { //called after every change of current score
		updateMusic (currentScore);
		godScript.updateScoreLabel (currentScore);
		checkWin ();
	}
	private void checkWin() {
		if (currentScore > scoreToWin)
			godScript.WinGame ();
			
	}

	private void updateMusic(int score) {
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

}
