using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used as a way for objects (specifically their methods) to subscribe to so that events can be fired from one point, and so that data can be shared
// without having to make everything public
public class GameEventSystemMainMenu : MonoBehaviour
{
    static public GameEventSystemMainMenu GESMainMenu;

    public event Action onEnteringGame;
    public event Action onTitleToMain;
    public event Action onDefaultState;
    public event Action<int> onCameraTurn;
    public event Action<int> onWithinAnArea;
    public event Action onPlayStage;
    public event Action onBackFromStage;

    void Awake()
    {GESMainMenu = this;}

    // When the player opens the game...
    public void EnteringGame()
    { if (onEnteringGame != null) onEnteringGame(); }

    // When the player presses any key/button on the title screen...
    public void TitleToMain()
    { if (onTitleToMain != null) onTitleToMain();}

    // When the player in the default (overview) position in the main menu...
    public void DefaultState()
    { if (onDefaultState != null) onDefaultState(); }

    // When the player turns the camera in the default (overview) position in the main menu...
    public void CameraTurn(int direction)
    {if (onCameraTurn != null) onCameraTurn(direction);}

    // When the player is within an area on the main menu...
    public void WithinAnArea(int animState)
    {if (onWithinAnArea != null) onWithinAnArea(animState);}

    // When the player plays the stage...
    public void PlayStage()
    { if (onPlayStage != null) onPlayStage(); }

    // When the player comes back from a stage...
    public void BackFromStage()
    { if (onBackFromStage != null) onBackFromStage();}
}