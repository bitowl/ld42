using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public void OnStartGame() {
		SceneManager.LoadSceneAsync("SampleScene");
	}

	public void OnQuitGame() {
		Application.Quit();
	}

	public void OnMainMenu() {
		SceneManager.LoadSceneAsync("MainMenu");
	}
}
