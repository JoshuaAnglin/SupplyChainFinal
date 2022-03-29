using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Options : MonoBehaviour
{
    [SerializeField] TextMeshPro fsModeText;

    void Awake()
    {ChangeFullscreenMode(0);}

    public void ChangeFullscreenMode(int Change)
    {
        GlobalScript.fsMode += Change;

        GlobalScript.fsMode = GlobalScript.fsMode < 0 ? 3 : GlobalScript.fsMode > 3 ? 0 : GlobalScript.fsMode;

        Screen.fullScreenMode = (FullScreenMode)GlobalScript.fsMode;

        switch (GlobalScript.fsMode)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                fsModeText.text = "Full Screen";
                break;

            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                fsModeText.text = "Full Screen Window";
                break;

            case 2:
                Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                fsModeText.text = "Maximized Window";
                break;

            case 3:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                fsModeText.text = "Window";
                break;
        }
    }
}