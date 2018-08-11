using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ShelfBoxSpawner))]
public class ShelfBoxSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (!Application.isPlaying) {
			return;
		}
        ShelfBoxSpawner myScript = (ShelfBoxSpawner)target;
        if(GUILayout.Button("Spawn single boxes"))
        {
            myScript.TestSpawnSingleBoxes();
        }

        if(GUILayout.Button("Spawn double boxes"))
        {
            myScript.TestSpawnDoubleBoxes();
        }

        if(GUILayout.Button("Spawn quad boxes"))
        {
            myScript.TestSpawnQuadBoxes();
        }

        if(GUILayout.Button("Spawn box at random position"))
        {
            myScript.TestSpawnBoxAtRandom();
        }
    }
}
