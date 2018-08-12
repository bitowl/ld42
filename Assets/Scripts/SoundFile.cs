using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="ld42/Sound File")]
public class SoundFile : ScriptableObject {
	public AudioClip Clip;
	[Range(0, 1)]
	public float Volume = 1;

	public void Play(AudioSource source) {
		source.PlayOneShot(Clip, Volume);
	}
}
