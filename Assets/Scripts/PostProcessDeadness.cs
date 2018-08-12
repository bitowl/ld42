using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class PostProcessDeadness : MonoBehaviour {

	public PostProcessVolume Alive;
	public PostProcessVolume Dead;
	public FloatVariable HowDeadAreWe;
	
	// Update is called once per frame
	void Update () {
		Alive.weight = 1 - HowDeadAreWe.value;
		Dead.weight = HowDeadAreWe.value;
	}
}
