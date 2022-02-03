using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

namespace SCG.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] AudioClip buttonOver;
        [SerializeField] AudioClip buttonSelected;
        [SerializeField] AudioClip buttonTransfer;
        [SerializeField] AudioSource bgm;
        [SerializeField] AudioSource soundEffects;
        [SerializeField] Text musicPercentage;
        [SerializeField] Text sePercentage;
        [SerializeField] Slider musicSlider;
        [SerializeField] Slider seSlider;
        [SerializeField] GameObject options;

        void Start()
        {
            Time.timeScale = 1;

            options.gameObject.SetActive(true);
            musicSlider.value = GlobalScript.musicVolume;
            seSlider.value = GlobalScript.soundEffectsVolume;
            options.gameObject.SetActive(false);

            GlobalScript.Volume(musicSlider, musicPercentage, ref GlobalScript.musicVolume, bgm);
            GlobalScript.Volume(seSlider, sePercentage, ref GlobalScript.soundEffectsVolume, soundEffects);

            musicSlider.onValueChanged.AddListener(delegate { GlobalScript.Volume(musicSlider,musicPercentage, ref GlobalScript.musicVolume, bgm); });
            seSlider.onValueChanged.AddListener(delegate { GlobalScript.Volume(seSlider, sePercentage, ref GlobalScript.soundEffectsVolume, soundEffects); });
        }

        public void PlayGame()
        {
            soundEffects.PlayOneShot(buttonTransfer);
            SceneManager.LoadScene(1);
        }

        public void MenuStatus(Image menu)
        {
            GlobalScript.OpenMenu(soundEffects, buttonSelected, menu);
        }

        public void setAudio()
        {
            musicSlider.value = GlobalScript.musicVolume;
            seSlider.value = GlobalScript.soundEffectsVolume;
        }

        public void Hovered(Image btn)
        {
            GlobalScript.OnButton(btn, GlobalScript.pointed, soundEffects, buttonOver, true);
        }

        public void Unhovered(Image btn)
        {
            GlobalScript.OnButton(btn, GlobalScript.unPointed, null, null, false);
        }

        public void QuitGame()
        {
            soundEffects.PlayOneShot(buttonTransfer);
            Application.Quit(); 
        }
    }
}