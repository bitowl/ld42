using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour {

	public Transform TargetTransform;
	public Transform PlayerTransform;
	public LayerMask WallMask = -1;
	// Use this for initialization
	void Start () {
		
	}
	
	// http://answers.unity.com/answers/26664/view.html
	void Update () {
		RaycastHit hit;

		var behindCheck = .8f;

		var target = TargetTransform.position;
		var minimum = PlayerTransform.position;
		var diffTM = target-minimum;
		var diffMT = minimum-target;


		if (Physics.Raycast(
			target + behindCheck * diffTM,
			diffMT,
			out hit,
			diffMT.magnitude * (1+behindCheck),
			WallMask.value)) {
			// Debug.Log("collison" + hit.rigidbody);
			var wishPosition =  (hit.point + behindCheck * diffMT);
			if (((wishPosition - minimum).normalized - diffTM.normalized).magnitude < 0.01) {
				transform.position = wishPosition;
			} else {
				transform.position = minimum;
			}

    		
			
			// (hit.point - PlayerTransform.position) * (1-behindCheck) + PlayerTransform.position;    
		}

		/*
		
		given: the hit point is at
		target + behindCheck
		then transform.position should be target

		
		 */
		else
		{
			transform.position = TargetTransform.position;
		}
	}
}
