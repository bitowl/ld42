using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(FreeSpaceManager))]
public class FreeSpaceManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (!Application.isPlaying) {
			return;
		}
        FreeSpaceManager myScript = (FreeSpaceManager)target;
        if(GUILayout.Button("Spawn single box"))
        {
            myScript.SpawnBox(Box.BoxType.Single);
        }

        if(GUILayout.Button("Spawn double box"))
        {
            myScript.SpawnBox(Box.BoxType.Double);
        }

        if(GUILayout.Button("Spawn quad box"))
        {
            myScript.SpawnBox(Box.BoxType.Quad);
        }

    }
}
