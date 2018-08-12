using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {

	public float LifeTimeInSeconds = 1;
	// Use this for initialization
	void Start () {
		Destroy(this, LifeTimeInSeconds);
	}
	
}
