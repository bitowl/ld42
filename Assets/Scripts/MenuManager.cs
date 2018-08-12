using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public void OnStartGame() {
		SceneManager.LoadSceneAsync("Tutorial1");
	}

	public void OnQuitGame() {
		Application.Quit();
	}

	public void OnMainMenu() {
		SceneManager.LoadSceneAsync("MainMenu");
	}

	public void OnNextLevel() {
		GameObject.Find("LevelSettings").GetComponent<LevelSettings>().StartNextLevel();
	}

	public void OnRetryLevel() {
		GameObject.Find("LevelSettings").GetComponent<LevelSettings>().RestartLevel();
	}
}
