using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoxesInShelfManager : MonoBehaviour {

	private List<Box> boxesInShelf = new List<Box>();

	private bool[] areSlotsFull;

	public int Width = 8;
	public int Height = 4;

	public float SingleBoxOffsetX = .5f;
	public float DoubleBoxOffsetX = 1;
	public float QuadBoxOffsetX = 2;

	public TextMeshPro FreeSlotsText;

	// Use this for initialization
	void Start () {
		areSlotsFull = new bool[Width * Height];
	}
	
	// Update is called once per frame
	void Update () {
		FreeSlotsText.text = "Free: " + GetFreeSlots() + " slots";

//		Debug.Log("Boxes in shelf: " + boxesInShelf.Count);
		// DebugPrintShelfContent();
	}

	private int GetFreeSlots() {
		// TODO: add counter that is in/de-cremented whenever areSlotsFull is changed
		int freeSlots = 0;

		for (int i = 0; i < areSlotsFull.Length; i++)
		{
			if (!areSlotsFull[i]) {
				freeSlots ++;
			}
		}
		return freeSlots;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Pickupable") {
			if (!boxesInShelf.Contains(other.gameObject)) {
				boxesInShelf.Add(other.gameObject);

				var box = other.gameObject.GetComponent<Box>();
				if (box.InShelf) {
					SetBoxInSlots(box, true);
				} else {
					Debug.LogError("This box is currently not in a shelf");
					var slot = GuessSlotFromGameObject(box);
					SetBoxInSlots(box, true);
				}
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Pickupable") {
			boxesInShelf.Remove(other.gameObject);

				var box = other.gameObject.GetComponent<Box>();
				if (box.InShelf) {
					SetBoxInSlots(box, false);
				} else {
					// Should never happen (?)
					Debug.LogError("This box is currently not in a shelf");
					// var slot = GuessSlotFromGameObject(other.gameObject);
					// areSlotsFull[slot] = false;
				}

				box.InShelf = false;
		}
	}

	private void SetBoxInSlots(Box box, bool content) {
		switch (box.Type)
		{
			case Box.BoxType.Single:
				areSlotsFull[GetSlotId(box.ShelfSlotX, box.ShelfSlotY)]	= content;
				break;
			case Box.BoxType.Double:
				areSlotsFull[GetSlotId(box.ShelfSlotX, box.ShelfSlotY)]	= content;
				areSlotsFull[GetSlotId(box.ShelfSlotX + 1, box.ShelfSlotY)]	= content;
				break;
			case Box.BoxType.Quad:
				areSlotsFull[GetSlotId(box.ShelfSlotX, box.ShelfSlotY)]	= content;
				areSlotsFull[GetSlotId(box.ShelfSlotX + 1, box.ShelfSlotY)]	= content;
				areSlotsFull[GetSlotId(box.ShelfSlotX + 2, box.ShelfSlotY)]	= content;
				areSlotsFull[GetSlotId(box.ShelfSlotX + 3, box.ShelfSlotY)]	= content;
				break;
		}
	}

	private int GuessSlotFromGameObject(Box boxObject) {
		// TODO: handle rotation
		var localPosition = boxObject.transform.position - gameObject.transform.position;

		var x = 0;
		var boxWidth = 0;
		switch (boxObject.Type)
		{
			case Box.BoxType.Single:
				x = (int)(localPosition.x - SingleBoxOffsetX);
				boxWidth = 1;
				break;
			case Box.BoxType.Double:
				x = (int)(localPosition.x - DoubleBoxOffsetX);
				boxWidth = 2;
				break;
			case Box.BoxType.Quad:
				x = (int)(localPosition.x - QuadBoxOffsetX);
				boxWidth = 4;
				break;
				
		}
		

		
		var y = (int)localPosition.y;

		x = Mathf.Clamp(x, 0, Width - boxWidth);
		y = Mathf.Clamp(y, 0, Height - 1);

		boxObject.InShelf = true;
		boxObject.ShelfSlotX = x;
		boxObject.ShelfSlotY = y;

		Debug.Log("Guessed: " + x + ", "+ y);
		Debug.Log(localPosition);
		return x + y * Width;
	}



	private void DebugPrintShelfContent() {
		var str = "Shelf Content:\n";
		for (int i = Height - 1; i >= 0; i--)
		{
			
			for (int j = 0; j < Width; j++)
			{
				if (IsSlotFree(j, i)) {
					str += "_";
				} else {
					str += "x";
				}
			}
			str += "\n";
		}
		Debug.Log(str);
	}

	private int GetSlotId(int x, int y) {
		return x + y * Width;
	}

	public bool IsSlotFree(int x, int y) {
		return !areSlotsFull[GetSlotId(x, y)];
	}
}
