using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectButtons : MonoBehaviour
{
    [SerializeField] string Title;
    [SerializeField] [TextArea] string Description;
    [SerializeField] UnityEvent action;

    void OnMouseOver()
    {
        if (GlobalScript.gs == GlobalScript.GameStatus.inMainMenu) MainMenu.inst.setScreenText(Title, Description);
    }

    void OnMouseDown()
    {
        if (GlobalScript.gs == GlobalScript.GameStatus.inMainMenu && GlobalScript.mms == GlobalScript.MainMenuStatus.Default)
            action.Invoke();
    }
}