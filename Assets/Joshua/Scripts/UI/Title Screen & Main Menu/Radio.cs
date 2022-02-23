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

    float flashTime = 0;
    float flashMaxTime = 1.5f;
    int flashType;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.anyKey && !GlobalScript.SwitchToMainMenu)
        {            
            anim.SetBool("GoToMainMenu", true);
            MainMenu.inst.vCamAnim.enabled = true;
            GlobalScript.SwitchToMainMenu = true;
        }
        else {
            flashTime += Time.deltaTime;

            if (flashTime > flashMaxTime)
            {
                if (RadioScreen.material.GetInt("_ScreenSwitch") == 0) flashType = 1;
                else flashType = 0;

                RadioScreen.material.SetInt("_ScreenSwitch", flashType);
                flashTime = 0;
            }
        }
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
