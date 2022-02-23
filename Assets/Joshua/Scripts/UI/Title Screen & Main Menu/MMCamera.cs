using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMCamera : MonoBehaviour
{
    Animator anim;
    static public int direction;

    float clamp;
    Vector3 startPos = new Vector3(-37.88f, 5, -8.18f);
    Vector3 startRot = new Vector3(22.39f, -90, 0);

    [SerializeField] GameObject optionsIndicator;
    [SerializeField] GameObject craftingIndicator;

    void Awake()
    {
        anim = GetComponent<Animator>();
        transform.position = startPos;
        transform.localEulerAngles = startRot;
    }

    void Start()
    {
        GameEventSystem.GES.onCameraTurn += CameraRotation;
    }

    public void DisableAnimator()
    {
        anim.enabled = false;
    }

    public void SwitchMenuState()
    {
        MainMenu.inst.SwitchState(MainMenu.inst.menuState = MainMenu.MenuState.MainMenu);
    }

    void CameraRotation(int dir)
    {
        clamp = Mathf.Clamp(clamp + (dir * 0.5f), -77, 77);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, clamp, transform.localEulerAngles.z);

        if(clamp> 0 && clamp <= 77 && optionsIndicator.activeSelf)
        {
            optionsIndicator.SetActive(false);
            craftingIndicator.SetActive(true);
        }

        else if (clamp < 0 && clamp >= -77 && !optionsIndicator.activeSelf)
        {
            optionsIndicator.SetActive(true);
            craftingIndicator.SetActive(false);
        }
    }

    void OnDisable()
    {
        GameEventSystem.GES.onCameraTurn -= CameraRotation;
    }
}