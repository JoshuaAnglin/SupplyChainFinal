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
        if (GlobalScript.state == GlobalScript.GameState.inMainMenu)
        {
            MainMenu.inst.screenTitle.text = Title;
            MainMenu.inst.screenDesc.text = Description;
        }
    }

    void OnMouseDown()
    {
        if (GlobalScript.state == GlobalScript.GameState.inMainMenu && GlobalScript.mms == GlobalScript.MainMenuStatus.Default)
            action.Invoke();
    }
}