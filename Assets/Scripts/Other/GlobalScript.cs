using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

abstract public class GlobalScript : MonoBehaviour
{
    public enum GameState
    {
        inTitleScreen,
        inMainMenu,
        inGame
    }
    static public GameState state;

    public enum MainMenuStatus
    {
        Default,
        inOptions,
        inScenery,
        inCredits,
        inPlay,
        inExit,
        inCrafting,
        ComingBackFromGameplay
    }
    static public MainMenuStatus mms;

    public enum GameplayStatus
    {
        Gameplay,
        Paused
    }
    static public GameplayStatus gps;

    static public float musicVolume = 50f;
    static public float soundEffectsVolume = 50f;

    static public int fsMode;

    static public string unPointed = "#222222";
    static public string pointed = "#446DCB";

    // Controls

    static public KeyCode jumpControl = KeyCode.Space; // change later
    static public KeyCode crouchControl = KeyCode.F; // change later

    static public KeyCode pauseUnpause = KeyCode.Escape;
    static public KeyCode openInventory = KeyCode.Tab;

    // UI

    static public Color HexColour(string colour)
    {
        Color col;
        ColorUtility.TryParseHtmlString(colour, out col);
        return col;
    }

    static public void SwitchScenes(int scene)
        {SceneManager.LoadScene(scene);}

    static public void OnButton(Image btn, string colour, AudioSource soundSource, AudioClip sound, bool playSound)
    {
        btn.color = HexColour(colour);
        if (playSound) soundSource.PlayOneShot(sound);
    }

    static public void OpenMenu(AudioSource soundSource, AudioClip sound, Image menu)
    {
        soundSource.PlayOneShot(sound);
        if (menu.IsActive()) menu.gameObject.SetActive(false);
        else if (!menu.IsActive()) menu.gameObject.SetActive(true);
    }

    static public void Volume(Slider slider, Text percentage, ref float volume, AudioSource audio)
    {
        percentage.text = slider.value.ToString() + "%";
        audio.volume = slider.value / 100;
        volume = slider.value;
    }
}