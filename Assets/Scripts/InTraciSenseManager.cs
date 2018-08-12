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

	public AudioSource BotAudioSource;
	public SoundFile InTraciSenseStartSound;
	public SoundFile InTraciSenseEndSound;

	// Use this for initialization
	void Start () {
		InTraciSenseMeter.value = 1;
		InTraciSenseActive.value = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire2")) {
			if (InTraciSenseMeter.value > MinimumBeforeActivatable) {
				Debug.Log(InTraciSenseMeter.value + " > " + MinimumBeforeActivatable);
				activated = true;
				InTraciSenseStartSound.Play(BotAudioSource);
			}
		}

		if (Input.GetButton("Fire2") && activated) {
			if (InTraciSenseMeter.value > 0) {
				InTraciSenseActive.value = true;
				InTraciSenseMeter.value -= Time.deltaTime / SecondsActive / TimeSlowdown;
				if (InTraciSenseMeter.value < 0) {
					InTraciSenseMeter.value = 0;
				}
			} else {
				if (InTraciSenseActive.value) {

					InTraciSenseEndSound.Play(BotAudioSource);
					InTraciSenseActive.value = false;
				}
			}
		} else {
			if (InTraciSenseActive.value) {
				InTraciSenseEndSound.Play(BotAudioSource);
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

		Time.timeScale = InTraciSenseActive.value ? TimeSlowdown : 1;
		Time.fixedDeltaTime = 0.02F * Time.timeScale;
	}

}
