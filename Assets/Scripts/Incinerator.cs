using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incinerator : MonoBehaviour {
	public GameObject BoxIncinerateEffectPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Pickupable") {
			other.gameObject.GetComponent<Box>().OnIncinerate();
			GameObject.Instantiate(BoxIncinerateEffectPrefab, other.gameObject.transform.position, Quaternion.identity);
			Destroy(other.gameObject, 1.5f);
		}
	}
}
