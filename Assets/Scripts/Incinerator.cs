﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incinerator : MonoBehaviour {
	public GameObject BoxIncinerateEffectPrefab;
	public GameEvent BoxIncineratedEvent;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Pickupable") {
			BoxIncineratedEvent.Raise();
			other.gameObject.GetComponent<Box>().OnIncinerate();
			GameObject.Instantiate(BoxIncinerateEffectPrefab, other.gameObject.transform.position, Quaternion.identity);
			Destroy(other.gameObject, 1.5f);
		}
	}
}
