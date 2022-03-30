using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used as a way for objects (specifically their methods) to subscribe to so that events can be fired from one point, and so that data can be shared
// without having to make everything public
public class GameEventSystemMainMenu : MonoBehaviour
{
    static public GameEventSystemMainMenu GESMainMenu;

    public event Action onBackFromStage;
    public event Action onEnteringGame;
    public event Action onTitleToMain;
    public event Action onDefaultState;
    public event Action<int> onCameraTurn;
    public event Action<int> onWithinAnArea;
    public event Action onPlayStage;

    void Awake()
    {
        Time.timeScale = 1;
        GESMainMenu = this;
    }

    public void BackFromStage()
    { if (onBackFromStage != null) onBackFromStage(); }

    public void EnteringGame()
    { if (onEnteringGame != null) onEnteringGame(); }

    public void TitleToMain()
    { if (onTitleToMain != null) onTitleToMain();}

    public void DefaultState()
    { if (onDefaultState != null) onDefaultState(); }

    public void CameraTurn(int direction)
    {if (onCameraTurn != null) onCameraTurn(direction);}

    public void WithinAnArea(int animState)
    {if (onWithinAnArea != null) onWithinAnArea(animState);}

    public void PlayStage()
    { if (onPlayStage != null) onPlayStage(); }
}