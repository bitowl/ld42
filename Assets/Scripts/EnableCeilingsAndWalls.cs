using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCeilingsAndWalls : MonoBehaviour {
	public GameObject Walls;
	public GameObject Ceilings;

	void Awake()
	{
		Walls.SetActive(true);
		Ceilings.SetActive(true);
	}
	
}
