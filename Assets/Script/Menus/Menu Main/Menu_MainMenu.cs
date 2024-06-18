using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu_MainMenu : Menus
{
    [Header("Buttons")]
    public Button playButton;
    public Button settignsButton;
    public Button exitButton;

    private void Start()
    {
        mainMenu = this;

        playButton.onClick.AddListener(() => mediator.GoToMainSelectLevel(selectLevel.selectLevelButton));
        settignsButton.onClick.AddListener(() => mediator.GoToSettigns(settings.graphicsButton));
        exitButton.onClick.AddListener(() => mediator.GoToQuit());
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
        mediator.eventSystem.SetSelectedGameObject(mainMenu.playButton.gameObject);
    }
}
