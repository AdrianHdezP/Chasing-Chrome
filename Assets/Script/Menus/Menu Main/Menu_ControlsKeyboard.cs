using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class Menu_ControlsKeyboard : Menus
{
    [Header("Is in level")]
    [SerializeField] private bool inLevel;

    [Header("Buttons")]
    public Button gameButton;
    public Button itemsButton;
    public Button powerupsButton;
    public Button controlsButton;
    public Button keyboardButton;
    public Button controllerButton;
    public Button returnButton;

    private void Start()
    {
        gameButton.onClick.AddListener(() => mediator.GoToHelpGame(helpGame.gameButton));
        itemsButton.onClick.AddListener(() => mediator.GoToHelpItems(helpItems.itemsButton));
        powerupsButton.onClick.AddListener(() => mediator.GoToHelpPowerups(helpPowerups.powerupsButton));
        controlsButton.onClick.AddListener(() => mediator.GoToControlsKeyboard(controlsKeyboard.keyboardButton));
        keyboardButton.onClick.AddListener(() => mediator.GoToControlsKeyboard(controlsKeyboard.keyboardButton));
        controllerButton.onClick.AddListener(() => mediator.GoToControlsController(controlsController.controllerButton));
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
        mediator.eventSystem.SetSelectedGameObject(controlsKeyboard.keyboardButton.gameObject);
    }
}
