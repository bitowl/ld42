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
	public float TimeSlowdown = 0.5f;

	public GameEvent InTraciSenseStartEvent;
	public GameEvent InTraciSenseStopEvent;

	// Use this for initialization
	void Start () {
		InTraciSenseMeter.value = 1;
		InTraciSenseActive.value = false;
	}
	private bool fire2AxisTrueLastFrame;
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0) {
			return;
		}

		var fire2AxisTrue = Input.GetAxis("Fire2") > 0;
		var fire2AxisDown = fire2AxisTrue && !fire2AxisTrueLastFrame;
		// var fire2AxisUp = !fire2AxisTrue && fire2AxisTrueLastFrame;
		fire2AxisTrueLastFrame = fire2AxisTrue;

		if (Input.GetButtonDown("Fire2") || fire2AxisDown) {
			if (InTraciSenseMeter.value > MinimumBeforeActivatable) {
				activated = true;
				InTraciSenseStartEvent.Raise();
			}
		}

		if ((Input.GetButton("Fire2") || fire2AxisTrue) && activated) {
			if (InTraciSenseMeter.value > 0) {
				InTraciSenseActive.value = true;
				InTraciSenseMeter.value -= Time.deltaTime / SecondsActive / TimeSlowdown;
				if (InTraciSenseMeter.value < 0) {
					InTraciSenseMeter.value = 0;
				}
			} else {
				if (InTraciSenseActive.value) {

					InTraciSenseStopEvent.Raise();
					InTraciSenseActive.value = false;
				}
			}
		} else {
			if (InTraciSenseActive.value) {
				InTraciSenseStopEvent.Raise();
				InTraciSenseActive.value = false;
				
			}
			activated = false;
			if (InTraciSenseMeter.value < 1) {
				InTraciSenseMeter.value += RegeneratePerSecond / SecondsActive * Time.deltaTime;
				if (InTraciSenseMeter.value > 1) {
					InTraciSenseMeter.value = 1;
				}
			}
		}

		if (Time.timeScale > 0) {
			Time.timeScale = InTraciSenseActive.value ? TimeSlowdown : 1;
		}
		
		Time.fixedDeltaTime = 0.02F * Time.timeScale;
	}

}
