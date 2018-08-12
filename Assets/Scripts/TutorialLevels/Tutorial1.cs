using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1 : MonoBehaviour {


	private FreeSpaceManager freeSpaceManager;
	private bool initialized;

	// Use this for initialization
	void Start () {
		freeSpaceManager = GameObject.Find("GlobalManagers").GetComponent<FreeSpaceManager>();

		
	}
	
	// Update is called once per frame
	void Update () {
		if (!initialized) {
			initialized = true;
			freeSpaceManager.SpawnBox(Box.BoxType.Single);
		}
	}
}
