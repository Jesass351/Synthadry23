using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TabToggler : MonoBehaviour
{
    public GameObject mainUI;
    public GameObject tabUI;

    private float fixedDeltaTime;

    private CustomCharacterController customCharacterController;
    private float oldSensitivityX;
    private float oldSensitivityY;


    private void Awake()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
    }

    private void Start()
    {
        customCharacterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CustomCharacterController>();
        oldSensitivityX = customCharacterController.xSensitivity;
        oldSensitivityY = customCharacterController.ySensitivity;
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Tab))
        // {
        //     mainUI.SetActive(false);
        //     tabUI.SetActive(true);
        //     Time.timeScale = 0.05f;
        //     Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;
        //     customCharacterController.xSensitivity = 0.002f;
        //     customCharacterController.ySensitivity = 0.002f;
        //     Cursor.lockState = CursorLockMode.Confined;
        // }
        // if (Input.GetKeyUp(KeyCode.Tab))
        // {
        //     mainUI.SetActive(true);
        //     tabUI.SetActive(false);
        //     Time.timeScale = 1f;
        //     Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;
        //     customCharacterController.xSensitivity = oldSensitivityX;
        //     customCharacterController.ySensitivity = oldSensitivityY;
        //     Cursor.lockState = CursorLockMode.Locked;
        // }
    }

    public void OnTabToggle(InputAction.CallbackContext ctx) {
        bool isHoldingTab = ctx.ReadValueAsButton();
        if (isHoldingTab) {
            mainUI.SetActive(false);
            tabUI.SetActive(true);
            Time.timeScale = 0.05f;
            Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;
            customCharacterController.xSensitivity = 0.002f;
            customCharacterController.ySensitivity = 0.002f;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else {
            mainUI.SetActive(true);
            tabUI.SetActive(false);
            Time.timeScale = 1f;
            Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;
            customCharacterController.xSensitivity = oldSensitivityX;
            customCharacterController.ySensitivity = oldSensitivityY;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
