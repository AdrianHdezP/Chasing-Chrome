using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuMediator : MonoBehaviour
{
    public EventSystem eventSystem;
    [Space]
    public Menus[] menus;
    // 0 - Main Screen
    // 1 - Main Menu
    // 2 - Main Menu/Select Level
    // 3 - Main Menu/Upgrades
    // 4 - Pause
    // 5 - Settings
    // 6 - Settings/Graphics
    // 7 - Settings/Sound
    // 8 - Help/Game
    // 9 - Help/Items
    // 10 - Help/Power-Ups
    // 11 - Help/Controls/Keyboard&Mouse
    // 12 - Help/Controls/Controller

    public Player player;
    public GameObject HUD;

    [HideInInspector] public bool isPlaying;
    [HideInInspector] public bool isPaused;

    public int currentIndex {  get; private set; }
    public int lastIndex { get; private set; }

    private void Start()
    {
        isPaused = false;
    }

    private void Update()
    {
        CheckIfCanPause();
        TrackDevice();
    }

    private void CheckIfCanPause()
    {
        if (SceneManager.GetActiveScene().name != "Main Menu")
        {
            player.controls.Player.Pause.performed += context => PauseKey();
        }
    }

    private void TrackDevice()
    {
        if (!ControlDevice.instance.IsGamepadActive())
            eventSystem.SetSelectedGameObject(null);

        // Comprobar si ha reconectado el mando
        if (ControlDevice.instance.IsGamepadActive() && eventSystem.currentSelectedGameObject == false)
            menus[currentIndex].GamepadReconected();

    }

    private void ChangeMenu(int _index, Button _button)
    {
        currentIndex = _index;

        menus[lastIndex].Hide();
        menus[currentIndex].Show();

        if (ControlDevice.instance.IsGamepadActive())
            eventSystem.SetSelectedGameObject(_button.gameObject);

        lastIndex = currentIndex;
    }


    #region Go To

    public void GoToMainMenu(Button _button) => ChangeMenu(1, _button);
    public void GoToMainSelectLevel(Button _button)
    {
        ChangeMenu(2, _button);
        isPlaying = true;
    }
    public void GoToUpgrades(Button _button) => ChangeMenu(3, _button);
    public void PauseKey()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
            return;

        if (isPaused)
        {
            Time.timeScale = 1;
            HUD.SetActive(true);

            for (int i = 4; i < menus.Length; i++)
            {
                menus[i].Hide();
            }

            isPaused = false;
            player.canMove = true;
        }
        else
        {
            Time.timeScale = 0;
            HUD.SetActive(false);
            menus[4].Show();
            isPaused = true;
            player.canMove = false;
        }    
    }
    public void GoToPause(Button _button) => ChangeMenu(4, _button);
    public void GoToSettigns(Button _button) => ChangeMenu(5, _button);
    public void GoToGraphics(Button _button) => ChangeMenu(6, _button);
    public void GoToSound(Button _button) => ChangeMenu(7, _button);   
    public void GoToHelpGame(Button _button) => ChangeMenu(8, _button);
    public void GoToHelpItems(Button _button) => ChangeMenu(9, _button);
    public void GoToHelpPowerups(Button _button) => ChangeMenu(10, _button);
    public void GoToControlsKeyboard(Button _button) => ChangeMenu(11, _button);
    public void GoToControlsController(Button _button) => ChangeMenu(12, _button);
    public void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    public void GoToPlay()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].Hide();
        }

        Time.timeScale = 1;

        HUD.SetActive(true);
        player.canMove = true;
        isPaused = false;
    }
    public void GoToQuit() => Application.Quit();

    #endregion

}