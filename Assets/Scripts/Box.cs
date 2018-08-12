using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Box : MonoBehaviour {
	
/*	public int ShelfSlotX;
	public int ShelfSlotY;
	public bool InShelf;*/

	public enum BoxType
	{
		Single,
		Double,
		Quad

	};



	public BoxType Type;
	public Variable Variable;

	public TextMeshPro NameText;
	public TextMeshPro ReferenceCountText;

	public BooleanVariable InTraciSenseActive;
	public Renderer BoxRenderer;
	public Material DefaultMaterial;
	public Material RedMaterial;
	public Material GreenMaterial;

	public GameEvent VariableWithReferencesPickedUpEvent;
	public ParticleSystem ThrowingEffect;
	private ParticleSystem.EmissionModule throwingEffectEmission;


	private bool senseActive;

	private List<BoxesInShelfManager> shelvesThisBoxIsOn = new List<BoxesInShelfManager>();

	// Use this for initialization
	void Start () {
		NameText.text = Variable.Name;
		throwingEffectEmission = ThrowingEffect.emission;
	}
	
	// Update is called once per frame
	void Update () {
		ReferenceCountText.text = "" + Variable.ReferenceCount; // TODO observe
		if (Variable.ReferenceCount > 0) {
			ReferenceCountText.color = Colors.Red;
		} else {
			ReferenceCountText.color = Colors.Green;
		}

		HandleInTraciSense();
	}

	private void HandleInTraciSense() {
		if (InTraciSenseActive.value) {
			if (Variable.ReferenceCount > 0) {
				BoxRenderer.material = RedMaterial;
			} else {
				BoxRenderer.material = GreenMaterial;
			}
			senseActive = true;
		} else if (senseActive) {
			BoxRenderer.material = DefaultMaterial;
			senseActive = false;
		}

	}

	public void PlacedOnShelf(BoxesInShelfManager shelf) {
		if (!shelvesThisBoxIsOn.Contains(shelf)) {
			shelvesThisBoxIsOn.Add(shelf);
			Variable.InShelf = true;
		}
	}

	public void RemovedFromShelf(BoxesInShelfManager shelf) {
		shelvesThisBoxIsOn.Remove(shelf);
		if (shelvesThisBoxIsOn.Count == 0) {
			Variable.InShelf = false;
			// We were removed from a shelf
			if (Variable.ReferenceCount > 0) {
				Debug.LogWarning("NOO DON'T TAKE MEEE");
				VariableWithReferencesPickedUpEvent.Raise();
			}
		}
	}

	


	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag != "Player") {
			ThrowingEffect.Stop();	
		}		
	}

	public void StartThrowing(float strength) {
		ThrowingEffect.Play();
		throwingEffectEmission.rateOverTime = strength * 80;
	}
}
public static class BoxTypeMethods {
	public static int GetNeededSpace(this Box.BoxType type) {
		switch (type)
		{
			case Box.BoxType.Single:
				return 1;
			case Box.BoxType.Double:
				return 2;
			case Box.BoxType.Quad:
				return  4;
		}
		return 0;

	}


}