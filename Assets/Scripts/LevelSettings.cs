using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour {
	[Header("General")]
	public string LevelName = "";
	public string NextLevelName = "";

	[Header("Box Probabilities")]
	public bool SpawnBoxesRandomly = true;
	public float SpawnBoxProbability = 0.01f;
	[Space(10)]
	public bool ChangeReferenceCountsRandomly = true;
	public float ReferenceDecreaseProbability = 0.2f;
	public float ReferenceIncreaseProbability = 0.1f;

	[Header("Win Condition")]
	public bool LevelTimeAsSuccessTrigger = true;
	public float LevelTimeInSeconds = 10;

}
