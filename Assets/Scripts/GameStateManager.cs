using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour {
	public FloatVariable RemainingTimeInSeconds;
	public GameEvent GameWonEvent;

	private LevelSettings levelSettings;
	// Use this for initialization
	void Start () {
		levelSettings = GameObject.Find("LevelSettings").GetComponent<LevelSettings>();
		RemainingTimeInSeconds.value = levelSettings.LevelTimeInSeconds;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit(); // TODO: break screen?
		}
		
		RemainingTimeInSeconds.value -= Time.deltaTime;

		if (levelSettings.LevelTimeAsSuccessTrigger) {	
			if (RemainingTimeInSeconds.value <= 0) {
				GameWonEvent.Raise();
			}
		}
	}

	public void OnGameOver() {
		// SceneManager.LoadSceneAsync("GameOver");
	}

	public void OnWin() {
		// TODO play animation
		// SceneManager.LoadSceneAsync("Win");
	}
}
