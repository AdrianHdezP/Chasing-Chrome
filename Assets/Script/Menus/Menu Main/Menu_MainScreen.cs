using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_MainScreen : Menus
{
    [Header("Buttons")]
    public Button startButton;

    private void Start()
    {
        mediator.isPlaying = false;

        startButton.onClick.AddListener(() => mediator.GoToMainMenu(mainMenu.playButton));
    }

    public override void Show()
    {
        gameObject.SetActive(true);
    }

    public override void Hide()
    {
        gameObject.SetActive(false);
    }

    public override void GamepadReconected()
    {
        mediator.eventSystem.SetSelectedGameObject(startButton.gameObject);
    }
}
