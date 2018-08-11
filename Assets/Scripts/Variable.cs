using UnityEngine;

[CreateAssetMenu(menuName = "ld42/Variable")]
public class Variable : ScriptableObject {
    public string Name;
    public int ReferenceCount;
}