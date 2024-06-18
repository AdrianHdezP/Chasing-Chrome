using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Upgrades : Menus
{
    [Header("Buttons")]
    public Button selectLevelButton;
    public Button upgradesButton;
    public Button settingsButton;
    public Button helpButton;
    public Button quitButton;

    private void Start()
    {
        selectLevelButton.onClick.AddListener(() => mediator.GoToMainSelectLevel(selectLevel.selectLevelButton));
        upgradesButton.onClick.AddListener(() => mediator.GoToUpgrades(upgrades.upgradesButton));
        settingsButton.onClick.AddListener(() => mediator.GoToSettigns(settings.graphicsButton));
        helpButton.onClick.AddListener(() => mediator.GoToHelpGame(helpGame.gameButton));
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
        mediator.eventSystem.SetSelectedGameObject(upgrades.upgradesButton.gameObject);
    }
}
