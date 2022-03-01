using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMCamera : MonoBehaviour
{
    Animator anim;
    static public int direction;

    float clamp;
    Vector3 startPos = new Vector3(-37.93f, 4.81f, -8.8f);
    Vector3 startRot = new Vector3(-10, -60.37f, 0);

    [SerializeField] GameObject optionsIndicator;
    [SerializeField] GameObject craftingIndicator;

    [SerializeField] Animator animDoor;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        switch (GlobalScript.GameState)
        {
            case GlobalScript.GameplayStatus.inTitleScreen:
                GameEventSystem.GES.EnteringGame();
                break;

            case GlobalScript.GameplayStatus.inGame:
                GameEventSystem.GES.BackFromStage();
                break;
        }
    }

    void OnEnable()
    {
        GameEventSystem.GES.onEnteringGame += UponStarting;
        GameEventSystem.GES.onTitleToMain += MainMenuTransition;
        GameEventSystem.GES.onCameraTurn += CameraRotation;
        GameEventSystem.GES.onWithinAnArea += GoToArea;
        GameEventSystem.GES.onDefaultState += GoBackToDefault;
        GameEventSystem.GES.onBackFromStage += GameplayToMainMenu;
    }

    void OnDisable()
    {
        GameEventSystem.GES.onEnteringGame -= UponStarting;
        GameEventSystem.GES.onTitleToMain -= MainMenuTransition;
        GameEventSystem.GES.onCameraTurn -= CameraRotation;
        GameEventSystem.GES.onWithinAnArea -= GoToArea;
        GameEventSystem.GES.onDefaultState -= GoBackToDefault;
        GameEventSystem.GES.onBackFromStage -= GameplayToMainMenu;
    }

    #region Animation Events

    void ActivateDoor()
    { animDoor.SetInteger("DoorState", 0); }

    void GoToStage()
    { SceneManager.LoadScene(2); }

    void setToMainMenu()
    {
        GlobalScript.GameState = GlobalScript.GameplayStatus.inMainMenu;
        MainMenu.inst.SwitchState();
    }

    #endregion

    #region Subscribed Events

    public void DisableAnimator()
    {anim.enabled = false;}

    public void SwitchMenuState()
    {
        GlobalScript.GameState = GlobalScript.GameplayStatus.inMainMenu;
        MainMenu.inst.SwitchState();
    }

    void UponStarting()
    {
        transform.position = startPos;
        transform.localEulerAngles = startRot;
    }

    void CameraRotation(int dir)
    {
        clamp = Mathf.Clamp(clamp + (dir * 0.3f), -77, 77);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, clamp, transform.localEulerAngles.z);

        if (clamp > 0 && clamp <= 77 && optionsIndicator.activeSelf)
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

    void MainMenuTransition()
    {anim.SetBool("(Title Screen) Proceed", true);}

    void GoToArea(int animState)
    {
        anim.enabled = true;
        anim.SetInteger("Area", animState);
    }

    void GoBackToDefault()
    {
        anim.enabled = true;
        anim.SetInteger("Area", -1);
    }

    void GameplayToMainMenu()
    {
        anim.SetBool("BackFromGameplay", true);
        animDoor.SetInteger("DoorState", 1);
    }

    #endregion
}