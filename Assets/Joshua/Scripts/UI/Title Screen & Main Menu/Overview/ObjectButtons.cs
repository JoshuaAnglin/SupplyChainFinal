using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectButtons : MonoBehaviour
{
    public string Title;
    [TextArea] public string Description;
    [SerializeField] UnityEvent action;

    void OnMouseOver()
    {
        MainMenu.inst.anP1Text.text = Title;
        MainMenu.inst.aNP2Text.text = Description;
    }

    void OnMouseDown()
    {
        action.Invoke();
    }
}