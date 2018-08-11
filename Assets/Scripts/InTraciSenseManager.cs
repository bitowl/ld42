using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InTraciSenseManager : MonoBehaviour {
	public BooleanVariable InTraciSenseActive;
	public FloatVariable InTraciSenseMeter;

	public float SecondsActive = 5;
	public float RegeneratePerSecond = 0.2f;
	public float MinimumBeforeActivatable = 0.1f;
	private bool activated;

	// Use this for initialization
	void Start () {
		InTraciSenseMeter.value = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire2")) {
			if (InTraciSenseMeter.value > MinimumBeforeActivatable) {
				activated = true;
			}
		}

		if (Input.GetButton("Fire2") && activated) {
			if (InTraciSenseMeter.value > 0) {
				InTraciSenseActive.value = true;
				InTraciSenseMeter.value -= Time.deltaTime / SecondsActive;
				if (InTraciSenseMeter.value < 0) {
					InTraciSenseMeter.value = 0;
				}
			} else {
				InTraciSenseActive.value = false;
			}
		} else {
			InTraciSenseActive.value = false;
			activated = false;
			if (InTraciSenseMeter.value < 1) {
				InTraciSenseMeter.value += RegeneratePerSecond / SecondsActive * Time.deltaTime;
				if (InTraciSenseMeter.value > 1) {
					InTraciSenseMeter.value = 1;
				}
			}
		}

	}

}
