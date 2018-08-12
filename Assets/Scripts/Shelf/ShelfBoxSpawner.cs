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
	private VariablesManager variablesManager;

	private BoxesInShelfManager boxesInShelfManager;

	// Use this for initialization
	void Start () {
		variablesManager = GameObject.Find("GlobalManagers").GetComponent<VariablesManager>();
		boxesInShelfManager = GetComponent<BoxesInShelfManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void spawnBox(int x, int y, int referenceCount) {
		var box = GameObject.Instantiate(BoxPrefab, transform.position + transform.rotation * new Vector3(x + SingleBoxOffsetX, y, 0), transform.rotation).GetComponent<Box>();
		/*box.InShelf = true;
		box.ShelfSlotX = x;
		box.ShelfSlotY = y;*/
		box.Type = Box.BoxType.Single;
		box.Variable = CreateVariable(3, referenceCount);
		var placement = new BoxPlacement();
		placement.SlotX = x;
		placement.SlotY = y;
		boxesInShelfManager.OnSpawnBoxInitially(box, placement);
	}

	private void spawnDoubleBox(int x, int y, int referenceCount) {
		var box = GameObject.Instantiate(DoubleBoxPrefab, transform.position + transform.rotation * new Vector3(x + DoubleBoxOffsetX, y, 0), transform.rotation).GetComponent<Box>();
		/*box.InShelf = true;
		box.ShelfSlotX = 2 * x;
		box.ShelfSlotY = y;*/
		box.Type = Box.BoxType.Double;
		box.Variable = CreateVariable(6, referenceCount);
				var placement = new BoxPlacement();
		placement.SlotX = x;
		placement.SlotY = y;
		boxesInShelfManager.OnSpawnBoxInitially(box, placement);
	}

	private void spawnQuadBox(int x, int y, int referenceCount) {
		var box = GameObject.Instantiate(QuadBoxPrefab, transform.position + transform.rotation * new Vector3(x + QuadBoxOffsetX, y, 0), transform.rotation).GetComponent<Box>();
		/*box.InShelf = true;
		box.ShelfSlotX = 4 * x;
		box.ShelfSlotY = y;*/
		box.Type = Box.BoxType.Quad;
		box.Variable = CreateVariable(10, referenceCount);
		var placement = new BoxPlacement();
		placement.SlotX = x;
		placement.SlotY = y;
		boxesInShelfManager.OnSpawnBoxInitially(box, placement);
	}

	public void SpawnBox(Box.BoxType type, Vector2Int position, int referenceCount) {
		switch (type)
		{
			case Box.BoxType.Single:
				spawnBox(position.x, position.y, referenceCount);
				break;
			case Box.BoxType.Double:
				spawnDoubleBox(position.x, position.y, referenceCount);
				break;
			case Box.BoxType.Quad:
				spawnQuadBox(position.x, position.y, referenceCount);
				break;
		}
	}


	private Variable CreateVariable(int maxCount, int referenceCount) {
		var variable = ScriptableObject.CreateInstance<Variable>();
		variable.Name = JavaClassNameGenerator.GenerateClassName(maxCount);
		variable.ReferenceCount = referenceCount;
		variablesManager.AddVariable(variable);
		return variable;
	}

	public void TestSpawnSingleBoxes() {
		for (int y = 0; y < Height; y++) {
			for (int x = 0; x < Width; x++) {
				spawnBox(x, y, Random.Range(0, 10));
			}
		}
	}



	public void TestSpawnDoubleBoxes() {
		for (int y = 0; y < Height; y++) {
			for (int x = 0; x < Width / 2; x++) {
				spawnDoubleBox(x * 2, y, Random.Range(0, 10));
			}
		}
	}

	public void TestSpawnQuadBoxes() {
		for (int y = 0; y < Height; y++) {
			for (int x = 0; x < Width / 4; x++) {
				spawnQuadBox(x * 4, y, Random.Range(0, 10));
			}
		}
	}

	public void TestSpawnBoxAtRandom() {
		var x = Random.Range(0, Width);
		var y = Random.Range(0, Height);

		if (GetComponent<BoxesInShelfManager>().IsSlotFree(x, y)) {
			spawnBox(x, y, Random.Range(0, 10));
		} else {
			Debug.LogWarning("Slot " + x + ", " + y + " is already full.");
		}
		
	}
}

