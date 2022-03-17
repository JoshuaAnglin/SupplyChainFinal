using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectButtons : MonoBehaviour
{
    public string Title;
    [TextArea] public string Description;
    [SerializeField] UnityEvent action;

    public enum AreaType
    {
        Options,
        Scenery,
        Credits,
        Play,
        Exit,
        Crafting
    }

    public AreaType area;

    void OnMouseOver()
    {
        MainMenu.inst.anP1Text.text = Title;
        MainMenu.inst.aNP2Text.text = Description;
    }

    void OnMouseDown()
    {
        action.Invoke();

        switch (area)
        { 
            case AreaType.Options:
                MainMenu.inOptions = true;
                break;

            case AreaType.Scenery:
                MainMenu.inScenery = true;
                break;

            case AreaType.Credits:
                MainMenu.inCredits = true;
                break;

            case AreaType.Play:
                MainMenu.inPlay = true;
                break;

            case AreaType.Exit:
                MainMenu.inExit = true;
                break;

            case AreaType.Crafting:
                MainMenu.inCrafting = true;
                break;
        }
    }
}