using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu_Pause : Menus
{
    [Header("Buttons")]
    public Button restartButton;
    public Button settingsButton;
    public Button helpButton;
    public Button returnButton;
    public Button quitButton;

    private void Start()
    {
        mediator.eventSystem.SetSelectedGameObject(restartButton.gameObject);

        restartButton.onClick.AddListener(() => mediator.RestartCurrentScene());
        settingsButton.onClick.AddListener(() => mediator.GoToSettigns(settings.graphicsButton));
        helpButton.onClick.AddListener(() => mediator.GoToHelpGame(helpGame.gameButton));
        returnButton.onClick.AddListener(() => mediator.GoToPlay());
        quitButton.onClick.AddListener(() => mediator.GoToQuit());
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
        mediator.eventSystem.SetSelectedGameObject(pause.restartButton.gameObject);
    }
}
