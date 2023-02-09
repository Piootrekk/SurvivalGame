using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class fullscreen : MonoBehaviour
{
    private void Update()
    {
        var keyboard = InputSystem.GetDevice<Keyboard>();
        if (keyboard.leftBracketKey.wasPressedThisFrame)
        {
            Screen.fullScreen = false;
        }
        else if (keyboard.rightBracketKey.wasPressedThisFrame)
            Screen.fullScreen = true;
    }
}
