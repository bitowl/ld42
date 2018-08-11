using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightFlicker : MonoBehaviour {
	public float MinIntensity = 2;
	public float MaxIntensity = 4;
	public float Delta = 0.2f;

	private Light lght;
	// Use this for initialization
	void Start () {
		lght = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		var intensity = lght.intensity;

		intensity += Random.Range(-Delta, Delta) * Time.deltaTime;
		intensity = Mathf.Clamp(intensity, MinIntensity, MaxIntensity);
		lght.intensity = intensity;
	}
}
