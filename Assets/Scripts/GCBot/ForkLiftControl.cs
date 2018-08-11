using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Physics layer ForkLift only collides with layer Pickupables, so that the fork lift does not collide with the level
public class ForkLiftControl : MonoBehaviour {

	public Transform ForkLift;
	public float MinY = 0;
	public float MaxY = 10;
	public float ForkSpeed = 10;
	public float AttractionForce = 1;
	public float PushAwayForce = 10;
	public float PickupLerpSpeed = 0.1f;

	private float input;
	private bool isAttracting;
	private List<GameObject> readyForPickup = new List<GameObject>();
	private GameObject currentlyPickedUp;
	private bool isLerpingPosition;
	private bool isLerpingRotation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		input = Input.GetAxis("Mouse Y");
		if (Input.GetButtonDown("Fire1")) {
			isAttracting = !isAttracting;

			if (isAttracting) {
				SelectGameObjectToPickup();
			} else {
				if (currentlyPickedUp != null) {
					currentlyPickedUp.GetComponent<Rigidbody>().isKinematic = false;
					currentlyPickedUp.GetComponent<BoxCollider>().enabled = true;
				}
				PushObjectsAway();
			}
		}

		AttractUsingLerp();
	}

	private void SelectGameObjectToPickup() {
		// Select the nearest pickupable object to pickup
		currentlyPickedUp = null;
		float distanceToCurrentlyPickedUp = float.MaxValue;
		foreach (var pickupObject in readyForPickup)
		{
			var distance = (ForkLift.position - pickupObject.transform.position).sqrMagnitude;
			if (distance < distanceToCurrentlyPickedUp) {
				currentlyPickedUp = pickupObject;
				distanceToCurrentlyPickedUp = distance;
			}
		}

		if (currentlyPickedUp != null) {
			isLerpingPosition = true;
			isLerpingRotation = true;
			currentlyPickedUp.GetComponent<Rigidbody>().isKinematic = true;
			currentlyPickedUp.GetComponent<BoxCollider>().enabled = false;
		} else {
			isAttracting = false;
		}

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
		CalculateForkPosition();
	}

	private void CalculateForkPosition() {
		ForkLift.localPosition = ForkLift.localPosition + Vector3.up * input * ForkSpeed;
		if (ForkLift.localPosition.y > MaxY) {
			ForkLift.localPosition = new Vector3(ForkLift.localPosition.x, MaxY, ForkLift.localPosition.z);
		} else if (ForkLift.localPosition.y < MinY) {
			ForkLift.localPosition = new Vector3(ForkLift.localPosition.x, MinY, ForkLift.localPosition.z);
		}
	}

	private void AttractUsingLerp() {
		if (!isAttracting || currentlyPickedUp == null) {
			return;	
		}

		if (isLerpingPosition) {
			// Move this to the correct position
			currentlyPickedUp.transform.position = Vector3.Lerp(currentlyPickedUp.transform.position, ForkLift.position, Time.deltaTime * PickupLerpSpeed);

			// Snap completely if close
			float epsilon = .1f;
			if ( (currentlyPickedUp.transform.position - ForkLift.position).sqrMagnitude < epsilon) {
				isLerpingPosition = false;
			}
		} else {
			currentlyPickedUp.transform.position = ForkLift.position;
		}

		if (isLerpingRotation) {
			currentlyPickedUp.transform.rotation = Quaternion.Slerp(currentlyPickedUp.transform.rotation, ForkLift.rotation, Time.deltaTime * PickupLerpSpeed);
			
			float angleEpsilon = 1f;
			if ( Quaternion.Angle(currentlyPickedUp.transform.rotation,ForkLift.rotation) < angleEpsilon) {
				isLerpingRotation = false;
			}
		} else {
			currentlyPickedUp.transform.rotation = ForkLift.rotation;
		}
	}	


	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Pickupable") {
			if (!readyForPickup.Contains(other.gameObject)) {
				readyForPickup.Add(other.gameObject);
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Pickupable") {
			readyForPickup.Remove(other.gameObject);
		}
	}

}
