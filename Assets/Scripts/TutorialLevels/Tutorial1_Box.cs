using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1_Box : MonoBehaviour {
	public Tutorial1 Tutorial1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		Tutorial1.OnColliderHit();		
	}
}
