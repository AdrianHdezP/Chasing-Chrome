using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_HelpGame : Menus
{
    [Header("Is in level")]
    [SerializeField] private bool inLevel;

    [Header("Buttons")]
    public Button gameButton;
    public Button itemsButton;
    public Button powerupsButton;
    public Button controlsButton;
    public Button returnButton;

    private void Start()
    {
        gameButton.onClick.AddListener(() => mediator.GoToHelpGame(helpGame.gameButton));
        itemsButton.onClick.AddListener(() => mediator.GoToHelpItems(helpItems.itemsButton));
        powerupsButton.onClick.AddListener(() => mediator.GoToHelpPowerups(helpPowerups.powerupsButton));
        controlsButton.onClick.AddListener(() => mediator.GoToControlsKeyboard(controlsKeyboard.keyboardButton));
    }

    private void Update()
    {
        if (inLevel)
            returnButton.onClick.AddListener(() => mediator.GoToPause(pause.restartButton));
        else
            returnButton.onClick.AddListener(() => mediator.GoToMainSelectLevel(selectLevel.selectLevelButton));
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
        mediator.eventSystem.SetSelectedGameObject(helpGame.gameButton.gameObject);
    }
}
