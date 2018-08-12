using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
public class OnBotIndicator : MonoBehaviour {

	public FloatVariable ValueToDisplay;
	public float MaxScaleZ;
	public float MinScaleZ;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	
	void Update () {
		var scale = transform.localScale;
		scale.z = ValueToDisplay.value * (MaxScaleZ-MinScaleZ) + MinScaleZ;
		transform.localScale = scale;
	}
}
