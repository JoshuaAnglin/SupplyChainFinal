using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Radio : MonoBehaviour
{
    Animator anim;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip sound;
    [SerializeField] AudioClip bgm;

    [SerializeField] Renderer RadioScreen;
    [SerializeField] Light lgtRadioScreen;

    float flashTime = 0;
    float flashMaxTime = 1.5f;
    int flashType;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        GameEventSystem.GES.onTitleToMain += MainMenuTransition;
    }

    void OnDisable()
    {
        GameEventSystem.GES.onTitleToMain -= MainMenuTransition;
    }

    void Update()
    {
        if (Input.anyKey && GlobalScript.GameState == GlobalScript.GameplayStatus.inTitleScreen)
        {
            GlobalScript.GameState = GlobalScript.GameplayStatus.inMainMenu;
            GameEventSystem.GES.TitleToMain();
            lgtRadioScreen.intensity = 1;
        }

        else {
            flashTime += Time.deltaTime;

            if (flashTime > flashMaxTime)
            {
                if (RadioScreen.material.GetInt("_ScreenSwitch") == 0) flashType = 1;
                else flashType = 0;

                RadioScreen.material.SetInt("_ScreenSwitch", flashType);

                if (flashType == 0) lgtRadioScreen.intensity = 0.5f;
                else if (flashType == 1) lgtRadioScreen.intensity = 1;
                    
                flashTime = 0;
            }
        }
    }

    void MainMenuTransition()
    {
        anim.SetBool("GoToMainMenu", true);
    }

    public void fe()
    {
        StartCoroutine(PlaySound());
        StopCoroutine(PlaySound());
    }

    public void SwitchScreen()
    {
        RadioScreen.material.SetInt("_KeyPressed", 1);
    }

    IEnumerator PlaySound()
    {
        audioSource.PlayOneShot(sound);
        yield return new WaitUntil(() => !audioSource.isPlaying);
        audioSource.PlayOneShot(bgm);
    }
}