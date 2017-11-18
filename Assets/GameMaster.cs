using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster {

	private const int pointsForExit = 50;
	private const int pointsForFlyer = 10;
	int scoreToWin = 150;

	//cross-level game master
	//observes GodScript of each level
	//receives notifications of score events
	//triggers game over / game win

	private GodScript godScript; //current god script running in level
	private FlyerAmmoManager flyerAmmoManager;
	private int currentScore;

	private static GameMaster gmInstance;

	private int heckerChaseCount = 0;
	private int turnedCitizenCount = 0;
	private int goodCitizenCount = 0;

	private bool policeSpawn;

    private List<bool> flyerAlreadyShown = new List<bool>{ false, false, false };

	public void registerGodScript(GodScript gs) {
		godScript = gs;
	}
	public void registerFlyerAmmoManager(FlyerAmmoManager fam) {
		flyerAmmoManager = fam;
	}
	public bool CheckAndDropFlyer() {
        bool result = false;
        if (!godScript.showingFlyer)
        {
            result = flyerAmmoManager.checkAndDropFlyer();
        }
        return result;
    }

	public void setScoreTowin(int toWin) { //todo: dirty - fix!
		scoreToWin = toWin;
	}
	public string getScoreToWin() {
		return scoreToWin.ToString();
	}
	public void setPoliceSpawn(bool spawn) {
		policeSpawn = spawn;
	}

	public void notifyLevelStarted() {
		currentScore = 0;
		updateScoreRoutine ();
	}
	public void notifyTurnedCitizenLeft() {
		if (policeSpawn) { // make sure that new citizens arrive in third level
			godScript.respawnRandomCitizen ();
		}
		currentScore += pointsForExit;
		updateScoreRoutine ();
		turnedCitizenCount--;
		updateCitizenTurnedState ();

		if (policeSpawn) { // spawn two new police men for every recruited citizen
			godScript.spawnRandomPolice ();
			godScript.spawnRandomPolice ();
		}
	}

	public void notifyTurnedCitizenDied() {
		godScript.respawnRandomCitizen ();
		turnedCitizenCount--;
		updateCitizenTurnedState ();
	}
	public void notifyCitizenTurned() {
		currentScore += pointsForFlyer;
		updateScoreRoutine ();
		turnedCitizenCount++;
		updateCitizenTurnedState ();
	}

	public void notifyHeckerDied() {
		godScript.LoseGame ();
	}
	public void notifyHeckerChased() {
		heckerChaseCount++;
		updateHeckerChaseState ();
	}
	public void notifyHeckerChaseStopped() {
		heckerChaseCount--;
		updateHeckerChaseState ();
	}

    public void notifyFlyerShown(int levelIndex)
    {
        flyerAlreadyShown[levelIndex] = true;
    }

    public bool checkIfFlyerAlreadyShown(int levelIndex)
    {
        return flyerAlreadyShown[levelIndex];
    }

	private void updateCitizenTurnedState() { //for game music
		if (turnedCitizenCount <= 0) {
			turnedCitizenCount = 0;
			AkSoundEngine.SetState ("CitizenState", "Normal");
		} else {
			AkSoundEngine.SetState ("CitizenState", "Turned");
		}
	}
	private void updateHeckerChaseState() { //for game music
		if (heckerChaseCount <= 0) {
			heckerChaseCount = 0;
			AkSoundEngine.SetState ("PlayerState", "Normal");
		} else {
			AkSoundEngine.SetState ("PlayerState", "Chased");
		}
	}

	public static GameMaster getInstance() { //singleton
		if (gmInstance == null) {
			gmInstance = new GameMaster ();
		}
		return gmInstance;
	}
	private void updateScoreRoutine() { //called after every change of current score
		godScript.updateScoreLabel (currentScore);
		checkWin ();
	}
	private void checkWin() {
		if (currentScore > scoreToWin)
			godScript.WinGame ();
			
	}



}
