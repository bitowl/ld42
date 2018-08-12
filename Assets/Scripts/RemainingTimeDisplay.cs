using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemainingTimeDisplay : MonoBehaviour {
	public FloatVariable RemainingTime;
	public TextMeshProUGUI Text;
	
	
	// Update is called once per frame
	void Update () {
		Text.text = "Remaining Computer Use Time: " + Mathf.RoundToInt(RemainingTime.value) + "s";
	}
}
