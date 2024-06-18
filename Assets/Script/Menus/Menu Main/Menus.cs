using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Menus : MonoBehaviour
{
    [Header("Config")]
    public MenuMediator mediator;

    [Header("Menus")]
    public Menu_MainMenu mainMenu;
    public Menu_SelectLevel selectLevel;
    public Menu_Upgrades upgrades;
    public Menu_Pause pause;
    public Menu_Settings settings;
    public Menu_SettingsGraphics graphics;
    public Menu_SettingsSound sound;
    public Menu_HelpGame helpGame;
    public Menu_HelpItems helpItems;
    public Menu_HelpPowerups helpPowerups;
    public Menu_ControlsKeyboard controlsKeyboard;
    public Menu_ControlsController controlsController;

    public abstract void Show();
    public abstract void Hide();
    public abstract void GamepadReconected();
    
}
