using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxesInShelfManager : MonoBehaviour {

	private List<GameObject> boxesInShelf = new List<GameObject>();

	private bool[] areSlotsFull;

	public int Width = 8;
	public int Height = 4;

	public float SingleBoxOffsetX = .5f;
	public float DoubleBoxOffsetX = 1;
	public float QuadBoxOffsetX = 2;


	// Use this for initialization
	void Start () {
		areSlotsFull = new bool[Width * Height];
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log("Boxes in shelf: " + boxesInShelf.Count);
		DebugPrintShelfContent();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Pickupable") {
			if (!boxesInShelf.Contains(other.gameObject)) {
				boxesInShelf.Add(other.gameObject);

				var slot = GuessSlotFromGameObject(other.gameObject);
				areSlotsFull[slot] = true;
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Pickupable") {
			boxesInShelf.Remove(other.gameObject);

			var slot = GuessSlotFromGameObject(other.gameObject);
			areSlotsFull[slot] = false;
		}
	}

	private int GuessSlotFromGameObject(GameObject boxObject) {
		// TODO: handle rotation
		var localPosition = boxObject.transform.position - gameObject.transform.position;

		var x = (int)(localPosition.x - SingleBoxOffsetX);
		var y = (int)localPosition.y;
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

	public bool IsSlotFree(int x, int y) {
		return !areSlotsFull[x + y * Width];
	}
}
