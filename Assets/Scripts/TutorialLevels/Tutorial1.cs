using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial1 : MonoBehaviour {


	private FreeSpaceManager freeSpaceManager;

	public TextMeshProUGUI TutorialText;
	public GameEvent GameWonEvent;

	private int currentStep;
	private string[] stepTexts = {
		"Use WSAD to move. Look around with mouse.", // 0
		"Use mouse to control forklift. Pick up box using left mouse button.", // 1
		"Throw the box away by clicking the left mouse button again.", // 2
		"Throw the box into that incinerator.", // 3
		"Hold left mouse button longer to throw the box further.", // 4
		"Burn that one as well.", // 5
		"The reference count is displayed on top of the boxes.\nOnly throw away those with reference count 0.", // 6
		"Press right mouse button to trigger InTraciSenseMode, to quicker find boxes with reference count 0.", // 7
		"You are ready for the game." // 8
	};

	private LevelSettings levelSettings;
	// Use this for initialization
	void Start () {
		freeSpaceManager = GameObject.Find("GlobalManagers").GetComponent<FreeSpaceManager>();
		currentStep = -1;
		NextStep();
		levelSettings = GameObject.Find("LevelSettings").GetComponent<LevelSettings>();
	}

	private void NextStep(){
		currentStep++;

		TutorialText.text = stepTexts[currentStep];
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void OnColliderHit() {
		if (currentStep == 0) {
			
			freeSpaceManager.SpawnBox(Box.BoxType.Single, 0);

			NextStep();
		}
	}

	public void OnBoxPickup() {
		if (currentStep == 1) {
			NextStep();
		}
	}

	public void OnBoxThrow() {
		if (currentStep == 2) {
			NextStep();
		}
	}

	public void OnBoxIncinerated() {
		if (currentStep == 3) {
			freeSpaceManager.SpawnBox(Box.BoxType.Double, 0);
			NextStep();
		} else  if(currentStep == 5) { // Throwing away the next
			freeSpaceManager.SpawnBox(Box.BoxType.Double, 0);
			NextStep();
		} else  if(currentStep == 6) { // Throw reference with 0 away
			freeSpaceManager.SpawnBox(Box.BoxType.Quad, 0);
			NextStep();
		} else if (currentStep == 8) { // 8: Throw all boxes away
			Win();
		} else {
			freeSpaceManager.SpawnBox(Box.BoxType.Single, 0);
		}
	}

	public void OnBoxLongThrow() {
		if (currentStep == 4) {
			freeSpaceManager.SpawnBox(Box.BoxType.Single, 1);
			NextStep();
		}
	}

/*	public void OnReferenceCount0ThrownAway() {

	}*/

	public void OnInTraciSenseModeOn() {
		if (currentStep == 7) {
			NextStep();
		}
	}

	/*public void OnAllBoxesThrownAway() {
		if (currentStep == 8) {
			Win();
		}
	}*/

	private void Win() {
		GameWonEvent.Raise();
		// TODO
	}


}
