using UnityEngine;
using UnityEditor;
using System.IO;

public class GameEventCreator : EditorWindow
{
    private string evName;
    void OnGUI()
    {
        evName = EditorGUILayout.TextField("Enter event name:", evName);
        if (GUILayout.Button("Generate Event"))
        {
            GenerateEvent(evName);
            Close();
        }
        if (GUILayout.Button("Close"))
        {
            Close();
        }
    }

    [MenuItem("Custom/Generate Event")]
    static void GenerateEvent()
    {
        GameEventCreator window = new GameEventCreator();
        window.ShowUtility();
    }

    static void GenerateEvent(string eventName)
    {
        if (eventName == null || eventName == "") {
            Debug.LogError("Can't genereate event without name.");
            return;
        }


        string eventCsTemplate = @"using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = ""Events/" + eventName + @"Event"")]
public class " + eventName + @"Event : ScriptableObject
{
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    private readonly List<" + eventName + @"EventListener> eventListeners =
        new List<" + eventName + @"EventListener>();

    public void Raise(" + eventName + @"EventData value)
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
        {
            eventListeners[i].OnEventRaised(value);
        }
    }

    public void RegisterListener(" + eventName + @"EventListener listener)
    {
        if (!eventListeners.Contains(listener))
        {
            eventListeners.Add(listener);
        }
    }

    public void UnregisterListener(" + eventName + @"EventListener listener)
    {
        if (eventListeners.Contains(listener))
        {
            eventListeners.Remove(listener);
        }
    }
}";
        string eventListenerCsTemplate = @"using UnityEngine;
using UnityEngine.Events;

public class " + eventName + @"EventListener : MonoBehaviour
{
    [Tooltip(""Event to register with."")]
    public " + eventName + @"Event Event;

    [Tooltip(""Response to invoke when Event is raised."")]
    [SerializeField]
    public " + eventName + @"UnityEvent Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(" + eventName + @"EventData value)
    {
        Response.Invoke(value);
    }
}";
        string eventEditorCsTemplate = @"using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(" + eventName + @"Event))]
public class " + eventName + @"EventEditor : Editor
{
    private " + eventName + @"EventData eventData;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (eventData == null) {
            eventData = ScriptableObject.CreateInstance<" + eventName + @"EventData>();
        }

        GUI.enabled = Application.isPlaying;

        Editor.CreateEditor(eventData).OnInspectorGUI();

        " + eventName + @"Event e = target as " + eventName + @"Event;
        if (GUILayout.Button(""Raise""))
        {
            e.Raise(eventData);
        }
    }
}";
        string eventDataCsTemplate = @"using UnityEngine;

[System.Serializable]
public class " + eventName + @"EventData : ScriptableObject {

}";
        string unityEventCsTemplate = @"using UnityEngine.Events;

[System.Serializable]
public class " + eventName + @"UnityEvent : UnityEvent<" + eventName + @"EventData> {

}";

        WriteFile("Assets/Scripts/Events/Generated/" + eventName + "Event.cs", eventCsTemplate);
        WriteFile("Assets/Scripts/Events/Generated/" + eventName + "EventListener.cs", eventListenerCsTemplate);
        WriteFile("Assets/Scripts/Events/Generated/Editor/" + eventName + "EventEditor.cs", eventEditorCsTemplate);
        WriteFile("Assets/Scripts/Events/Generated/" + eventName + "UnityEvent.cs", unityEventCsTemplate);
        WriteFile("Assets/Scripts/Events/" + eventName + "EventData.cs", eventDataCsTemplate);
        Debug.Log("Generated event " + eventName);

    }

    static void WriteFile(string filename, string contents)
    {
        StreamWriter writer = new StreamWriter(filename, false);
        writer.Write(contents);
        writer.Close();
    }
}