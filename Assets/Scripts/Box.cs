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


	// Use this for initialization
	void Start () {
		NameText.text = Variable.Name;
	}
	
	// Update is called once per frame
	void Update () {
		ReferenceCountText.text = "" + Variable.ReferenceCount; // TODO observe
		if (Variable.ReferenceCount > 0) {
			ReferenceCountText.color = Colors.Red;
		} else {
			ReferenceCountText.color = Colors.Green;
		}
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