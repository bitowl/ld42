using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour
{

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;	
	}

	
	void OnDestroy()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;	
	}

}