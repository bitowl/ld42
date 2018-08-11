using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : MonoBehaviour {
	public float MoveSpeed = 10;
	public float RotationSpeed = 10;

	private Rigidbody rb;

	private float horizontalInput;
	private float verticalInput;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		horizontalInput = Input.GetAxis("Horizontal");
		verticalInput = Input.GetAxis("Vertical");
	}

	void FixedUpdate()
	{
		rb.AddForce(transform.forward * verticalInput * MoveSpeed - rb.velocity, ForceMode.Force);
		rb.AddTorque(transform.up * RotationSpeed * horizontalInput - rb.angularVelocity);
	}

	// Manual X, Z Axis freeze
	void LateUpdate()
	{
		transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);	
	}
	
    
}
