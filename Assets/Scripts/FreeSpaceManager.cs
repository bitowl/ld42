using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FreeSpaceManager : MonoBehaviour {

	public TextMeshProUGUI TotalSpaceText;
	public Slider FreeSpaceSlider;

	private List<BoxesInShelfManager> shelves = new List<BoxesInShelfManager>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var free = GetFreeSpace();
		var total = GetTotalSpace();
		
		TotalSpaceText.text = "Free Space: " + free + " / " + total;
		FreeSpaceSlider.value = (float)free / total;
	}

	public void RegisterShelf(BoxesInShelfManager shelf) {
		shelves.Add(shelf);
	}

	public int GetTotalSpace() {
		int totalSpace = 0;
		foreach (var shelf in shelves)
		{
			totalSpace += shelf.GetTotalSpace();
		}
		return totalSpace;
	}

	public int GetFreeSpace() {
		int freeSpace = 0;
		foreach (var shelf in shelves)
		{
			freeSpace += shelf.GetFreeSpace();
		}
		return freeSpace;
	}
}
