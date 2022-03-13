using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuLights : MonoBehaviour
{
    Animator anim;

    void Awake()
    {anim = GetComponent<Animator>();}

    void OnEnable()
    {
        GameEventSystemMainMenu.GESMainMenu.onTitleToMain += LightRoomLights;
        GameEventSystemMainMenu.GESMainMenu.onPlayStage += GoingToGameplay;
        GameEventSystemMainMenu.GESMainMenu.onBackFromStage += BackFromGameplay;
    }

    void OnDisable()
    {
        GameEventSystemMainMenu.GESMainMenu.onTitleToMain -= LightRoomLights;
        GameEventSystemMainMenu.GESMainMenu.onPlayStage -= GoingToGameplay;
        GameEventSystemMainMenu.GESMainMenu.onBackFromStage -= BackFromGameplay;
    }

    // Title Screen > Main Menu
    void LightRoomLights()
    {
        anim.SetInteger("LightState", 1);
    }

    // Main Menu > Gameplay
    void GoingToGameplay()
    { 
        anim.SetInteger("LightState", 2);
    }

    // Gameplay > Main Menu
    void BackFromGameplay()
    {
        anim.SetInteger("LightState", 0);

    }
}