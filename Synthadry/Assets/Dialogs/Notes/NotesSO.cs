using UnityEngine;

[CreateAssetMenu(fileName = "New Note", menuName = "Create new Note")]
[System.Serializable]

public class NotesSO : ScriptableObject
{
    public string Title;
    public string Text;
}
