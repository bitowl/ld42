using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoxesInShelfManager : MonoBehaviour {

	// private List<BoxPlacement> boxesInShelf = new List<BoxPlacement>();
	private Dictionary<Box, BoxPlacement> boxesInShelf = new Dictionary<Box, BoxPlacement>();

	// private bool[] areSlotsFull;
	private Box[] areSlotsFull;

	public int Width = 8;
	public int Height = 4;

	public float SingleBoxOffsetX = .5f;
	public float DoubleBoxOffsetX = 1;
	public float QuadBoxOffsetX = 2;

	public TextMeshPro FreeSlotsText;

	// Use this for initialization
	void Start () {
		areSlotsFull = new Box[Width * Height];
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
			var box = other.gameObject.GetComponent<Box>();

			// if (!boxesInShelf.Contains(box)) {
			if (!boxesInShelf.ContainsKey(box)) {

				var placement = new BoxPlacement();
				GuessSlotFromGameObject(box, placement);
				boxesInShelf.Add(box, placement);
				
				SetBoxInSlots(box, placement, true);
			/*	if (box.InShelf) {
					SetBoxInSlots(box, true);
				} else {
					Debug.LogError("This box is currently not in a shelf");
					var slot = GuessSlotFromGameObject(box);
					SetBoxInSlots(box, true);
				}*/
			// }
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Pickupable") {
			var box = other.gameObject.GetComponent<Box>();
			BoxPlacement placement = boxesInShelf[box];
			boxesInShelf.Remove(box);
			SetBoxInSlots(box, placement, false);
				
				/*if (box.InShelf) {
					SetBoxInSlots(box, false);
				} else {
					// Should never happen (?)
					Debug.LogError("This box is currently not in a shelf");
					// var slot = GuessSlotFromGameObject(other.gameObject);
					// areSlotsFull[slot] = false;
				}

				box.InShelf = false;*/
		}
	}

	private void SetBoxInSlots(Box box, BoxPlacement placement, bool content) {
		switch (box.Type)
		{
			case Box.BoxType.Single:
				SetSlot(placement.SlotX, placement.SlotY, box, content);
				break;
			case Box.BoxType.Double:
				SetSlot(placement.SlotX, placement.SlotY, box, content);
				SetSlot(placement.SlotX + 1, placement.SlotY, box, content);
				break;
			case Box.BoxType.Quad:
				SetSlot(placement.SlotX, placement.SlotY, box, content);
				SetSlot(placement.SlotX + 1, placement.SlotY, box, content);
				SetSlot(placement.SlotX + 2, placement.SlotY, box, content);
				SetSlot(placement.SlotX + 3, placement.SlotY, box, content);
				break;
		}
	}

	private void SetSlot(int x, int y, Box box, bool content) {
		if (content && areSlotsFull[GetSlotId(x,y)] == null) {
			areSlotsFull[GetSlotId(x, y)] = box;
		} else if( !content && areSlotsFull[GetSlotId(x,y)] == box) {
			areSlotsFull[GetSlotId(x, y)] = null;
		}
	}

	private int GuessSlotFromGameObject(Box box, BoxPlacement placement) {
		// TODO: handle rotation
		var localPosition = box.transform.position - gameObject.transform.position;

		var x = 0;
		var boxWidth = 0;
		switch (box.Type)
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
		

		
		var y = (int)(localPosition.y + 0.5);

		x = Mathf.Clamp(x, 0, Width - boxWidth);
		y = Mathf.Clamp(y, 0, Height - 1);

		placement.SlotX = x;
		placement.SlotY = y;

		Debug.Log("Guessed: " + x + ", "+ y + "  | " + localPosition);
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
