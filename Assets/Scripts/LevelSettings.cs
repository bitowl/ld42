using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSettings : MonoBehaviour {
	[Header("General")]
	public string LevelName = "";
	public string NextLevelName = "";

	[Header("Box Probabilities")]
	public bool SpawnBoxesRandomly = true;
	public float SpawnBoxProbability = 0.01f;
	[Space(10)]
	public bool ChangeReferenceCountsRandomly = true;
	public float ReferenceDecreaseProbability = 0.02f;
	public float ReferenceIncreaseProbability = 0.01f;



	[Header("Win Condition")]
	public bool LevelTimeAsSuccessTrigger = true;
	public float LevelTimeInSeconds = 10;

	[Header("Lose Condition")]
	public int DeadAtWrongBoxes = 10;

	public void StartNextLevel() {
		SceneManager.LoadSceneAsync(NextLevelName);
	}

	public void RestartLevel() {
		SceneManager.LoadSceneAsync(LevelName);
	}
}
