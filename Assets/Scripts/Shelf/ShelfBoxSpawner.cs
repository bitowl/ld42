using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfBoxSpawner : MonoBehaviour {

	public int Width = 8;
	public int Height = 4;

	public float SingleBoxOffsetX = .5f;
	public float DoubleBoxOffsetX = 1;
	public float QuadBoxOffsetX = 2;


	public GameObject BoxPrefab;
	public GameObject DoubleBoxPrefab;
	public GameObject QuadBoxPrefab;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void spawnBox(int x, int y) {
		var box = GameObject.Instantiate(BoxPrefab, transform.position + transform.rotation * new Vector3(x + SingleBoxOffsetX, y, 0), transform.rotation).GetComponent<Box>();
		/*box.InShelf = true;
		box.ShelfSlotX = x;
		box.ShelfSlotY = y;*/
		box.Type = Box.BoxType.Single;
	}

	private void spawnDoubleBox(int x, int y) {
		var box = GameObject.Instantiate(DoubleBoxPrefab, transform.position + transform.rotation * new Vector3(2 * x + DoubleBoxOffsetX, y, 0), transform.rotation).GetComponent<Box>();
		/*box.InShelf = true;
		box.ShelfSlotX = 2 * x;
		box.ShelfSlotY = y;*/
		box.Type = Box.BoxType.Double;
	}

	private void spawnQuadBox(int x, int y) {
		var box = GameObject.Instantiate(QuadBoxPrefab, transform.position + transform.rotation * new Vector3(4 * x + QuadBoxOffsetX, y, 0), transform.rotation).GetComponent<Box>();
		/*box.InShelf = true;
		box.ShelfSlotX = 4 * x;
		box.ShelfSlotY = y;*/
		box.Type = Box.BoxType.Quad;
	}

	public void TestSpawnSingleBoxes() {
		for (int y = 0; y < Height; y++) {
			for (int x = 0; x < Width; x++) {
				spawnBox(x, y);
			}
		}
	}

	

	public void TestSpawnDoubleBoxes() {
		for (int y = 0; y < Height; y++) {
			for (int x = 0; x < Width / 2; x++) {
				spawnDoubleBox(x, y);
			}
		}
	}

	public void TestSpawnQuadBoxes() {
		for (int y = 0; y < Height; y++) {
			for (int x = 0; x < Width / 4; x++) {
				spawnQuadBox(x, y);
			}
		}
	}

	public void TestSpawnBoxAtRandom() {
		var x = Random.Range(0, Width);
		var y = Random.Range(0, Height);

		if (GetComponent<BoxesInShelfManager>().IsSlotFree(x, y)) {
			spawnBox(x, y);
		} else {
			Debug.LogWarning("Slot " + x + ", " + y + " is already full.");
		}
		
	}
}

