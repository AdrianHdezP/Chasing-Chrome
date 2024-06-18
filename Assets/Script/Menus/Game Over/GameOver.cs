using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public EventSystem eventSystem;

    public GameObject[] buttons;

    private void Update()
    {
        TrackDevice();
    }

    private void TrackDevice()
    {
        if (!ControlDevice.instance.IsGamepadActive())
            eventSystem.SetSelectedGameObject(null);

        if (ControlDevice.instance.IsGamepadActive() && eventSystem.currentSelectedGameObject == false)
            eventSystem.SetSelectedGameObject(buttons[0]);
    }

    public void TryAgain() => SceneManager.LoadScene("Level 1");

    public void MainMenu() => SceneManager.LoadScene("Main Menu");

}
