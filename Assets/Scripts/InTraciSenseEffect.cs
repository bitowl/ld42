using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class InTraciSenseEffect : MonoBehaviour {
	public Animator Animator;
	public BooleanVariable InTraciSenseActive;

	public PostProcessVolume PostProcessVolume;
	public PostProcessProfile NormalProfile;
	public PostProcessProfile InTraciSenseProfile;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Animator.SetBool("active", InTraciSenseActive.value);
		PostProcessVolume.profile = InTraciSenseActive.value ? InTraciSenseProfile : NormalProfile;
	}
}
