using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : MonoBehaviour {
	public float MoveSpeed = 10;
	public float RotationSpeed = 10;
	public float HorizontalDrag = 0.9f;
	public float JumpForce = 10;

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
		if (Input.GetButtonDown("Jump")) {
			rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
		}
	}

	void FixedUpdate()
	{
		rb.AddForce(transform.forward * verticalInput * MoveSpeed - rb.velocity, ForceMode.Force);
		rb.AddTorque(transform.up * RotationSpeed * horizontalInput - rb.angularVelocity);

		var vel = rb.velocity;
		vel.x *= HorizontalDrag;
		vel.z *= HorizontalDrag;
		rb.velocity = vel;
	}

	// Manual X, Z Axis freeze
	void LateUpdate()
	{
		transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);	
	}
	
    
}
