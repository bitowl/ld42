using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariablesManager : MonoBehaviour {

	public GameEvent IncreaseReferenceOfUnreachableVariableEvent;

	private List<Variable> deadVariables = new List<Variable>();
	private List<Variable> aliveVariables = new List<Variable>();

	private LevelSettings levelSettings;

	// Use this for initialization
	void Start () {
		levelSettings = GameObject.Find("LevelSettings").GetComponent<LevelSettings>();
	}
	
	// Update is called once per frame
	void Update () {
		if (levelSettings.ChangeReferenceCountsRandomly) {
			ChangeReferenceCountsRandomly();
		}
	}

	private void ChangeReferenceCountsRandomly() {
		if (Random.Range(0f, 1f) < levelSettings.ReferenceDecreaseProbability * aliveVariables.Count) {
			DecreaseRandomReference();
		}
		if (Random.Range(0f, 1f) < levelSettings.ReferenceIncreaseProbability * aliveVariables.Count) {
			IncreaseRandomReference();
		}
	}

	private float MAX_TRIES = 50;
	private void DecreaseRandomReference() {
		for (int i = 0; i < MAX_TRIES; i++)
		{
			var variable = GetRandomVariable();
			if (variable.ReferenceCount > 0 && variable.InShelf) {
				variable.ReferenceCount--;
				if (variable.ReferenceCount <= 0) {
					aliveVariables.Remove(variable);
					deadVariables.Add(variable);
				}
				return;
			}
		}
	}

	private void IncreaseRandomReference() {
		for (int i = 0; i < MAX_TRIES; i++)
		{
			var variable = GetRandomVariable();
			if (variable.ReferenceCount > 0) {
				if (!variable.InShelf) {
					IncreaseReferenceOfUnreachableVariableEvent.Raise();
					// Debug.LogError("THIS VARIABLE CANNOT BE REFERENCED. IT'S UNREACHABLE");
					continue;
				}

				variable.ReferenceCount++;
				return;
			}
		}
	}



	private Variable GetRandomVariable() {
		return aliveVariables[Random.Range(0, aliveVariables.Count)];
	}

	public void AddVariable(Variable variable) {
		if (variable.ReferenceCount > 0) {
			aliveVariables.Add(variable);
		} else {
			deadVariables.Add(variable);
		}
		
	}
}
