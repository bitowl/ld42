using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ld42/Variable")]
public class Variable : ScriptableObject {
    public string Name;
    public int ReferenceCount;
    public bool InShelf;
    // public UnityEvent ReferenceCountChangedEvent;
}