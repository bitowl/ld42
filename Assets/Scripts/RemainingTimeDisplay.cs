using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class RemainingTimeDisplay : MonoBehaviour {
	public FloatVariable RemainingTime;
	public TextMeshProUGUI Text;
	
	private LevelSettings levelSettings;
	void Start()
	{
		levelSettings = GameObject.Find("LevelSettings").GetComponent<LevelSettings>();
		if (Application.isPlaying) { 
			gameObject.SetActive(levelSettings.LevelTimeAsSuccessTrigger);
		}
	}

	
	// Update is called once per frame
	void Update () {
		Text.text = "Remaining Computer Use Time: " + Mathf.RoundToInt(RemainingTime.value) + "s";

		if (Application.isEditor) {
			Text.enabled = levelSettings.LevelTimeAsSuccessTrigger;
		}
	}
}
