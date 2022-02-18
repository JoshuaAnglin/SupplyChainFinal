using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public Text anP1Text;
    public Text aNP2Text;
    static public MainMenu inst;

    public enum MenuState
    {
        TitleScreen,
        MainMenu
    }

    public MenuState menuState = MenuState.TitleScreen;

    void Awake()
    {
        SwitchState(menuState);
        Time.timeScale = 1.0f;
        inst = this;
    }

    public void PlayStage()
    {
        // Camera Goes to Door
        // Door slowly opens

        SceneManager.LoadScene(1);
    }

    public void StageSelector()
    {
        // Stage selector screen opens
        // UI Focuses on Map

        Debug.Log("Stage Selector");
    }

    public void Crafting()
    {
        Debug.Log("Crafting");
    }

    public void Scenery()
    {
        Debug.Log("Scenery");
    }

    public void Options()
    {
        Debug.Log("Options");
    }

    public void Credits()
    {
        Debug.Log("Credits");
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
    }

    public void SwitchState(MenuState newState)
    {
        switch (newState)
        {
            case MenuState.TitleScreen:
                gameObject.SetActive(false);
                break;

            case MenuState.MainMenu:
                gameObject.SetActive(true);
                break;
        }
    }
}