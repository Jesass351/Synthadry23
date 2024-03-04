using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Notes : MonoBehaviour
{
    public NotesSO notesSO;
    public GameObject notesParent;
    private PlayerInput playerInput;
    private GameObject player;
    private bool IsUsing;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInput = player.GetComponent<PlayerInput>();
    }

    private void OnNotes(InputValue value)
    {
        IsUsing = value.isPressed;
        Debug.Log("use note");
    }

    private void OnEnable()
    {
        playerInput.ActivateInput();
    }
}
