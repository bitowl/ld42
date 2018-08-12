using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FreeSpaceSignOnTheWall : MonoBehaviour {
	public TextMeshProUGUI TotalSpaceText;
	public Slider FreeSpaceSlider;

	
	public FloatVariable FreeSlots;
	public FloatVariable TotalSlots;

	void Update()
	{
		TotalSpaceText.text = "Free Space: " + FreeSlots.value + " / " + TotalSlots.value;
		FreeSpaceSlider.value = FreeSlots.value / TotalSlots.value;
	}
}
