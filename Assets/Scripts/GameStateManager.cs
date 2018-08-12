using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour {
	public float LevelTimeInSeconds = 10;
	public FloatVariable RemainingTimeInSeconds;

	// Use this for initialization
	void Start () {
		RemainingTimeInSeconds.value = LevelTimeInSeconds;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit(); // TODO: break screen?
		}

		RemainingTimeInSeconds.value -= Time.deltaTime;
		if (RemainingTimeInSeconds.value <= 0) {
			OnWin();
		}
	}

	public void OnGameOver() {
		SceneManager.LoadSceneAsync("GameOver");
	}

	public void OnWin() {
		// TODO play animation
		SceneManager.LoadSceneAsync("Win");
	}
}
