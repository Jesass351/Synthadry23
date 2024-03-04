using TMPro;
using UnityEngine;

public class NotesSlot : MonoBehaviour
{
    public TextMeshProUGUI titleUI;
    public TextMeshProUGUI textUI;
    public GameObject mainUI;
    public GameObject circleUI;

    public void ShowNotes(string title, string text)
    {
        Cursor.lockState = CursorLockMode.Confined;
        textUI.text = text;
        titleUI.text = title;
        textUI.gameObject.SetActive(true);
        titleUI.gameObject.SetActive(true);
        mainUI.SetActive(false);
        circleUI.SetActive(false);
    }

    public void HideNotes()
    {
        Debug.Log("123");
        Cursor.lockState = CursorLockMode.Locked;
        textUI.text = "";
        titleUI.text = "";
        textUI.gameObject.SetActive(false);
        titleUI.gameObject.SetActive(false);
        mainUI.SetActive(true);
    }
}
