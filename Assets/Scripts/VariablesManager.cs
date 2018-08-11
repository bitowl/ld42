using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariablesManager : MonoBehaviour {

	public float ReferenceDecreaseProbability = 0.2f;
	public float ReferenceIncreaseProbability = 0.1f;

	private List<Variable> deadVariables = new List<Variable>();
	private List<Variable> aliveVariables = new List<Variable>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Random.Range(0f, 1f) < ReferenceDecreaseProbability * aliveVariables.Count) {
			DecreaseRandomReference();
		}
		if (Random.Range(0f, 1f) < ReferenceIncreaseProbability * aliveVariables.Count) {
			IncreaseRandomReference();
		}
	}

	private float MAX_TRIES = 50;
	private void DecreaseRandomReference() {
		for (int i = 0; i < MAX_TRIES; i++)
		{
			var variable = GetRandomVariable();
			if (variable.ReferenceCount > 0) {
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
