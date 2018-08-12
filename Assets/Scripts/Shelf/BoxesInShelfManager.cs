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
	public TextMeshPro FreeSlotsText2;

	// Use this for initialization
	void Start () {
		areSlotsFull = new Box[Width * Height];

		var manager = GameObject.Find("GlobalManagers");
		if (manager != null) {
			manager.GetComponent<FreeSpaceManager>().RegisterShelf(this);
		}
	}
	
	
	// Update is called once per frame
	void Update () {
		var used = GetUsedSlots();
		var total = GetTotalSpace();

		FreeSlotsText.text = GetUsedSlots() + "/" + GetTotalSpace();
		FreeSlotsText2.text = GetUsedSlots() + "/" + GetTotalSpace();

		if (used == total) { // is full
			FreeSlotsText.color = Colors.Red;
			FreeSlotsText2.color = Colors.Red;
		} else {
			FreeSlotsText.color = Color.gray;
			FreeSlotsText2.color = Color.gray;
		}
		
		//"Free: " + GetFreeSlots() + " slots";

//		Debug.Log("Boxes in shelf: " + boxesInShelf.Count);
		// DebugPrintShelfContent();
	}

	private string LeadingZero(int value) {
		if (value < 10) {
			return "0" + value;
		}
		return ""+ value;
	}

	private int GetUsedSlots() {
		return GetTotalSpace() - GetFreeSlots();
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

	public int GetFreeSpace() {
		return GetFreeSlots();
	}

	public int GetTotalSpace() {
		return Width * Height;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Pickupable") {
			var box = other.gameObject.GetComponent<Box>();

			// if (!boxesInShelf.Contains(box)) {
			if (!boxesInShelf.ContainsKey(box)) {
				box.PlacedOnShelf(this);
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
			box.RemovedFromShelf(this);
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
		var localPosition = gameObject.transform.rotation * (box.transform.position - gameObject.transform.position);

		var x = 0;
		var boxWidth = 0;
		switch (box.Type)
		{
			case Box.BoxType.Single:
				x = (int)(localPosition.x - SingleBoxOffsetX + 0.5f);
				boxWidth = 1;
				break;
			case Box.BoxType.Double:
				x = (int)(localPosition.x - DoubleBoxOffsetX + 0.5f);
				boxWidth = 2;
				break;
			case Box.BoxType.Quad:
				x = (int)(localPosition.x - QuadBoxOffsetX + 0.5f);
				boxWidth = 4;
				break;
				
		}
		

		
		var y = (int)(localPosition.y + 0.5);

		x = Mathf.Clamp(x, 0, Width - boxWidth);
		y = Mathf.Clamp(y, 0, Height - 1);

		placement.SlotX = x;
		placement.SlotY = y;

		// Debug.Log("Guessed: " + x + ", "+ y + "  | " + localPosition);
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


	public bool CanSpawnBox(Box.BoxType type) {
		var neededFreeBoxes = type.GetNeededSpace();
		for (int y = 0; y < Height; y++)
		{
			var continousFreeBoxes = 0;
			for (int x = 0; x < Width; x++)
			{
				var free = IsSlotFree(x, y);
				if (!free && continousFreeBoxes >= neededFreeBoxes) {
					return true;
				} else if(free) {
					continousFreeBoxes++;
				} else {
					continousFreeBoxes = 0;
				}
			}
			if (continousFreeBoxes >= neededFreeBoxes) {
				return true;
			}
		}
		return false;
	}

	public Vector2Int GetFreeSlot(Box.BoxType type) {
		// TODO: maybe select a random free slot?
		var slotX = 0;
		var slotY = 0;

		var neededFreeBoxes = type.GetNeededSpace();


		var foundFree = false;
		for (int y = 0; y < Height; y++)
		{
			var continousFreeBoxes = 0;
			var firstSlotX = 0;
			for (int x = 0; x < Width; x++)
			{
				var free = IsSlotFree(x, y);
				if (!free && continousFreeBoxes >= neededFreeBoxes) {
					foundFree = true;
					slotX = firstSlotX;
					slotY = y;
					break;
				} else if(free) {
					continousFreeBoxes++;
				} else {
					continousFreeBoxes = 0;
					firstSlotX = x + 1; // The next one might be the first free one
				}
			}
			if (foundFree) {
				break;
			}
			if (continousFreeBoxes >= neededFreeBoxes) {
				slotX = firstSlotX;
				slotY = y;
				break;
			}
		}

		return new Vector2Int(slotX, slotY);
	}

}
