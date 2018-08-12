using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FreeSpaceManager : MonoBehaviour {

	public GameEvent GameOverEvent;
	public FloatVariable FreeSpace;
	public FloatVariable FreeSlots;
	public FloatVariable TotalSlots;

	private List<BoxesInShelfManager> shelves = new List<BoxesInShelfManager>();

	private LevelSettings levelSettings;

	// Use this for initialization
	void Start () {
		levelSettings = GameObject.Find("LevelSettings").GetComponent<LevelSettings>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0) {
			return;
		}
		CalculateFreeSpace();
		if (levelSettings.SpawnBoxesRandomly) {
			SpawnBoxesRandomly();
		}
	}

	private void CalculateFreeSpace() {
		var free = GetFreeSpace();
		var total = GetTotalSpace();
		FreeSlots.value = free;
		TotalSlots.value = total;
		if (total == 0) {
			FreeSpace.value = 0;
		} else {
			FreeSpace.value = (float)free / total;
		}
	}

	private void SpawnBoxesRandomly() {
		// Spawn boxes randomly
		if (Random.Range(0f, 1f) < levelSettings.SpawnBoxProbability) {
			// TODO set different probabilities for the different box types?
			int type = Random.Range(0,3);
			switch (type)
			{
				case 0:
					SpawnBox(Box.BoxType.Single, Random.Range(0, 10)); // TODO: maybe not random reference count?
					break;
				case 1:
					SpawnBox(Box.BoxType.Double, Random.Range(0, 10));
					break;
				case 2:
					SpawnBox(Box.BoxType.Quad, Random.Range(0, 10));
					break;
			}
		}
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

	public void SpawnBox(Box.BoxType type, int referenceCount) {
		// TODO: maybe make this random?
		foreach (var shelf in shelves)
		{
			if (shelf.GetFreeSpace() >= type.GetNeededSpace()) {
				if (shelf.CanSpawnBox(type)) {
					shelf.GetComponent<ShelfBoxSpawner>().SpawnBox(type, shelf.GetFreeSlot(type), referenceCount);
					return;
				}
			}
		}
		Debug.LogError("CANNOT PLACE BOX " + type);
		TriggerGameOver();
	}

	private void TriggerGameOver() {
		GameOverEvent.Raise();
	}
}
