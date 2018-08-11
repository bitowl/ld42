using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkLiftControl : MonoBehaviour {

	public Transform ForkLift;
	public float MinY = 0;
	public float MaxY = 10;
	public float ForkSpeed = 10;
	public float AttractionForce = 1;
	public float PushAwayForce = 10;

	private float input;
	private bool isAttracting;
	private List<GameObject> readyForPickup = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		input = Input.GetAxis("Mouse Y");
		if (Input.GetButtonDown("Fire1")) {
			isAttracting = !isAttracting;

			if (!isAttracting) {
				PushObjectsAway();
			}
		}

		Debug.Log(isAttracting + ": " + readyForPickup.Count);
	}

	// When ending attracting, the object currently attracted are just pushed away
	private void PushObjectsAway() {
		foreach (var pickupObject in readyForPickup)
		{
			pickupObject.GetComponent<Rigidbody>().AddForce(ForkLift.forward * PushAwayForce, ForceMode.Impulse);
		}
	}

	
	void FixedUpdate()
	{
		
		ForkLift.localPosition = ForkLift.localPosition + Vector3.up * input * ForkSpeed;
		if (ForkLift.localPosition.y > MaxY) {
			ForkLift.localPosition = new Vector3(ForkLift.localPosition.x, MaxY, ForkLift.localPosition.z);
		} else if (ForkLift.localPosition.y < MinY) {
			ForkLift.localPosition = new Vector3(ForkLift.localPosition.x, MinY, ForkLift.localPosition.z);
		}

		if (isAttracting) {
			foreach (var pickupObject in readyForPickup)
			{
				pickupObject.GetComponent<Rigidbody>().AddForce((ForkLift.transform.position - pickupObject.transform.position) * AttractionForce);
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Pickupable") {
			readyForPickup.Add(other.gameObject);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Pickupable") {
			readyForPickup.Remove(other.gameObject);
		}
	}
}
