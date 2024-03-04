using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Screenshot : MonoBehaviour
{
    // void Update()
    // {
    //     if (Input.GetKeyDown("p"))
    //     {
    //         ScreenCapture.CaptureScreenshot("C://Users/artemGame/Desktop/screenshot" + System.DateTime.Now.ToString("MM-dd-yy (HH-mm-ss)") + ".png", 4);
    //         Debug.Log("A screenshot was taken!");
    //     }
    // }
    public void OnScreenshot(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            ScreenCapture.CaptureScreenshot("C://Users/artemGame/Desktop/screenshot" + System.DateTime.Now.ToString("MM-dd-yy (HH-mm-ss)") + ".png", 4);
            Debug.Log("A screenshot was taken!");
        }
    }
}