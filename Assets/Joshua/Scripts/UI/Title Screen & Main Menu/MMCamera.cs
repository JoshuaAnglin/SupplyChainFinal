using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMCamera : MonoBehaviour
{
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SwitchMenuState()
    {
        MainMenu.inst.SwitchState(MainMenu.inst.menuState = MainMenu.MenuState.MainMenu);
    }

    
}
