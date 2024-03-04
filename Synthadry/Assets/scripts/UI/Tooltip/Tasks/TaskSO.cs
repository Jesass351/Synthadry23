using UnityEngine;

[CreateAssetMenu(fileName = "New Task", menuName = "Create new Task")]
[System.Serializable]

public class TaskSO : ScriptableObject
{
    public string Title;
    public string Text;
}
