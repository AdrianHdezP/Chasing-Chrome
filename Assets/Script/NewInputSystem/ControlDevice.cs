using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlDevice : MonoBehaviour
{
    public static ControlDevice instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Control Device in the scene");
        }

        instance = this;
    }

    public bool IsGamepadActive()
    {
        if (Gamepad.all.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
