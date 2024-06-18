using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_SettingsSound : Menus
{
    [Header("Buttons")]
    public Button returnButton;

    private void Start()
    {
        returnButton.onClick.AddListener(() => mediator.GoToSettigns(settings.graphicsButton));
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
        mediator.eventSystem.SetSelectedGameObject(sound.returnButton.gameObject);
    }
}
