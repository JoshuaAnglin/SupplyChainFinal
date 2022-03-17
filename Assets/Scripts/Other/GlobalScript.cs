using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GlobalScript : MonoBehaviour
{
    public enum GameplayStatus
    {
        inTitleScreen,
        inMainMenu,
        inGame
    }

    static public GameplayStatus GameState;

    static public float musicVolume = 50f;
    static public float soundEffectsVolume = 50f;

    static public string unPointed = "#222222";
    static public string pointed = "#446DCB";

    // UI

    static public Color HexColour(string colour)
    {
        Color col;
        ColorUtility.TryParseHtmlString(colour, out col);
        return col;
    }

    static public void SwitchScenes(int scene)
    {
        SceneManager.LoadScene(scene);
    }

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
