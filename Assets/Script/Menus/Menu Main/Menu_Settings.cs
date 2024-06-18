using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Settings : Menus
{
    [Header("Is in level")]
    [SerializeField] private bool inLevel;

    [Header("Buttons")]
    public Button graphicsButton;
    public Button soundButton;
    public Button returnButton;

    private void Start()
    {
        graphicsButton.onClick.AddListener(() => mediator.GoToGraphics(graphics.returnButton));
        soundButton.onClick.AddListener(() => mediator.GoToSound(sound.returnButton));
    }

    private void Update()
    {
        if (inLevel)
        {
            returnButton.onClick.AddListener(() => mediator.GoToPause(pause.restartButton));
        }
        else
        {
            if (mediator.isPlaying == false)
                returnButton.onClick.AddListener(() => mediator.GoToMainMenu(mainMenu.playButton));

            if (mediator.isPlaying == true)
                returnButton.onClick.AddListener(() => mediator.GoToMainSelectLevel(selectLevel.selectLevelButton));
        }  
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
        mediator.eventSystem.SetSelectedGameObject(settings.graphicsButton.gameObject);
    }
}
