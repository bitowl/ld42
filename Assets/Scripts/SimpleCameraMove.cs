using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraMove : MonoBehaviour {

	
	private float totalTime;

	public float Frequency = 10;
	public float Amplitude = 40;
	private float baseOffset;
	// Use this for initialization
	void Start () {
		baseOffset = transform.eulerAngles.y;
	}
	
	// Update is called once per frame
	void Update () {
		totalTime += Time.deltaTime;
		var euler = transform.eulerAngles;
		euler.y = Mathf.Sin(totalTime / Frequency) * Amplitude + baseOffset;
		transform.rotation = Quaternion.Euler(euler);
	}
}
