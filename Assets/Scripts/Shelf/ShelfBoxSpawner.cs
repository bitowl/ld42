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
		box.Variable = CreateVariable(3);
	}

	private void spawnDoubleBox(int x, int y) {
		var box = GameObject.Instantiate(DoubleBoxPrefab, transform.position + transform.rotation * new Vector3(x + DoubleBoxOffsetX, y, 0), transform.rotation).GetComponent<Box>();
		/*box.InShelf = true;
		box.ShelfSlotX = 2 * x;
		box.ShelfSlotY = y;*/
		box.Type = Box.BoxType.Double;
		box.Variable = CreateVariable(6);
	}

	private void spawnQuadBox(int x, int y) {
		var box = GameObject.Instantiate(QuadBoxPrefab, transform.position + transform.rotation * new Vector3(x + QuadBoxOffsetX, y, 0), transform.rotation).GetComponent<Box>();
		/*box.InShelf = true;
		box.ShelfSlotX = 4 * x;
		box.ShelfSlotY = y;*/
		box.Type = Box.BoxType.Quad;
		box.Variable = CreateVariable(10);
	}

	public void SpawnBox(Box.BoxType type, Vector2Int position) {
		switch (type)
		{
			case Box.BoxType.Single:
				spawnBox(position.x, position.y);
				break;
			case Box.BoxType.Double:
				spawnDoubleBox(position.x, position.y);
				break;
			case Box.BoxType.Quad:
				spawnQuadBox(position.x, position.y);
				break;
		}
	}


	private Variable CreateVariable(int maxCount) {
		var variable = ScriptableObject.CreateInstance<Variable>();
		variable.Name = JavaClassNameGenerator.GenerateClassName(maxCount);
		variable.ReferenceCount = Random.Range(0, 10);
		return variable;
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
				spawnDoubleBox(x * 2, y);
			}
		}
	}

	public void TestSpawnQuadBoxes() {
		for (int y = 0; y < Height; y++) {
			for (int x = 0; x < Width / 4; x++) {
				spawnQuadBox(x * 4, y);
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

