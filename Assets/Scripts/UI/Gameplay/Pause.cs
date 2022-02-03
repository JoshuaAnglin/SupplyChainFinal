using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] AudioClip buttonOver;
    [SerializeField] AudioClip buttonSelected;
    [SerializeField] AudioClip buttonTransfer;
    public AudioSource bgm;
    public AudioSource soundEffects;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Image optionsMenu;
    [SerializeField] Text musicPercentage;
    [SerializeField] Text sePercentage;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider seSlider;

    static public Pause inst;

    bool canUnpause;

    // Inheritance with main menu later?

    void Start()
    {
        inst = this;
        optionsMenu.gameObject.SetActive(true);
        musicSlider.value = GlobalScript.musicVolume;
        seSlider.value = GlobalScript.soundEffectsVolume;
        optionsMenu.gameObject.SetActive(false);

        canUnpause = true;

        GlobalScript.Volume(musicSlider, musicPercentage, ref GlobalScript.musicVolume, bgm);
        GlobalScript.Volume(seSlider, sePercentage, ref GlobalScript.soundEffectsVolume, soundEffects);

        musicSlider.onValueChanged.AddListener(delegate { GlobalScript.Volume(musicSlider, musicPercentage, ref GlobalScript.musicVolume, bgm); });
        seSlider.onValueChanged.AddListener(delegate { GlobalScript.Volume(seSlider, sePercentage, ref GlobalScript.soundEffectsVolume, soundEffects); });
    }

    void Update()
    {
        // Pausing

        if (Input.GetKeyDown(KeyCode.Escape) && canUnpause)
        {
            if (GameplayUI.status != GameplayUI.gameplayState.startup)
            {
                if (GameplayUI.status == GameplayUI.gameplayState.paused)
                {
                    GameplayUI.status = GameplayUI.gameplayState.unpaused;
                    pauseMenu.SetActive(false);
                }

                else if (GameplayUI.status == GameplayUI.gameplayState.unpaused)
                {
                    GameplayUI.status = GameplayUI.gameplayState.paused;
                    pauseMenu.SetActive(true);
                }

                soundEffects.PlayOneShot(buttonSelected);
                GameplayUI.StatusAction();
            }
        }
    }

    public void UnPause()
    {
        GameplayUI.status = GameplayUI.gameplayState.unpaused;
        pauseMenu.SetActive(false);
        GameplayUI.StatusAction();
    }

    public void Hovered(Image btn)
    {
        GlobalScript.OnButton(btn, GlobalScript.pointed, soundEffects, buttonOver, true);
    }

    public void Unhovered(Image btn)
    {
        GlobalScript.OnButton(btn, GlobalScript.unPointed, null, null, false);
    }

    public void Restart()
    {
        soundEffects.PlayOneShot(buttonTransfer);
        GlobalScript.SwitchScenes(1);
    }

    public void Options()
    {
        soundEffects.PlayOneShot(buttonSelected);
        GlobalScript.OpenMenu(soundEffects, buttonSelected, optionsMenu);
        if (canUnpause) canUnpause = false;
        else if (!canUnpause) canUnpause = true;
    }

    public void ExitGameplay()
    {
        soundEffects.PlayOneShot(buttonTransfer);
        GlobalScript.SwitchScenes(0);
    }
}
