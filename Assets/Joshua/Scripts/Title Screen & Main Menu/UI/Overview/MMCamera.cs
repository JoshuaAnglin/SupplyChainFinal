using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMCamera : MonoBehaviour
{
    Animator anim;

    float rotClamp;
    Vector3 startPos = new Vector3(-37.93f, 4.81f, -8.8f);
    Vector3 startRot = new Vector3(-10, -60.37f, 0);

    float minScrollPos = 9f;
    float maxScrollPos = 11.6f;

    [SerializeField] GameObject mainMenu;
    [SerializeField] Animator animDoor;
    [SerializeField] GameObject optionsIndicator;
    [SerializeField] GameObject craftingIndicator;

    static public List<Action> backAction = new List<Action>();

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        switch (GlobalScript.gs)
        {
            case GlobalScript.GameStatus.inTitleScreen:
                GameEventSystemMainMenu.GESMainMenu.EnteringGame();
                break;

            case GlobalScript.GameStatus.inGame:
                GameEventSystemMainMenu.GESMainMenu.BackFromStage();
                break;
        }
    }

    void OnEnable()
    {
        GameEventSystemMainMenu.GESMainMenu.onEnteringGame += UponStarting;
        GameEventSystemMainMenu.GESMainMenu.onTitleToMain += MainMenuTransition;
        GameEventSystemMainMenu.GESMainMenu.onCameraTurn += CameraRotation;
        GameEventSystemMainMenu.GESMainMenu.onWithinAnArea += GoToArea;
        GameEventSystemMainMenu.GESMainMenu.onDefaultState += GoBackToDefault;
        GameEventSystemMainMenu.GESMainMenu.onBackFromStage += GameplayToMainMenu;
    }

    void OnDisable()
    {
        GameEventSystemMainMenu.GESMainMenu.onEnteringGame -= UponStarting;
        GameEventSystemMainMenu.GESMainMenu.onTitleToMain -= MainMenuTransition;
        GameEventSystemMainMenu.GESMainMenu.onCameraTurn -= CameraRotation;
        GameEventSystemMainMenu.GESMainMenu.onWithinAnArea -= GoToArea;
        GameEventSystemMainMenu.GESMainMenu.onDefaultState -= GoBackToDefault;
        GameEventSystemMainMenu.GESMainMenu.onBackFromStage -= GameplayToMainMenu;
    }

    void Update()
    {
        if (GlobalScript.mms == GlobalScript.MainMenuStatus.inOptions)
            OptionsScrollLimit();
    }

    #region Animation Events

    void ActivateDoor()
    { animDoor.SetInteger("DoorState", 0); }

    void GoToStage()
    { SceneManager.LoadScene(2); }

    void setToMainMenu()
    {
        GlobalScript.gs = GlobalScript.GameStatus.inMainMenu;
        GlobalScript.mms = GlobalScript.MainMenuStatus.Default;
        mainMenu.gameObject.SetActive(true);
    }

    void DefaultAnimEvents()
    {
        DisableAnimator();
        rotClamp = 0;
    }

    void DisableAnimator()
    { anim.enabled = false; }

    #endregion

    #region Subscribed Events

    void UponStarting()
    {
        transform.position = startPos;
        transform.localEulerAngles = startRot;
    }

    void MainMenuTransition()
    { anim.SetBool("(Title Screen) Proceed", true); }

    void CameraRotation(int dir)
    {
        rotClamp = Mathf.Clamp(rotClamp + (dir * 0.3f), -50, 50);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotClamp, transform.localEulerAngles.z);

        if (rotClamp > 0 && rotClamp <= 77 && optionsIndicator.activeSelf)
        {
            optionsIndicator.SetActive(false);
            craftingIndicator.SetActive(true);
        }

        else if (rotClamp < 0 && rotClamp >= -77 && !optionsIndicator.activeSelf)
        {
            optionsIndicator.SetActive(true);
            craftingIndicator.SetActive(false);
        }
    }

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

    void OptionsScrollLimit()
    {
        if (transform.position.y < minScrollPos)
            transform.position = new Vector3(transform.position.x, minScrollPos + 0.15f, transform.position.z);

        else if (transform.position.y > maxScrollPos)
            transform.position = new Vector3(transform.position.x, maxScrollPos - 0.15f, transform.position.z);

        else transform.Translate(Vector3.up * (Input.GetAxis("Mouse ScrollWheel") * 2));
    }

    // Have a static list of actions, then add actions to it from other scripts (scripts send actions to the list)
    // When pressed, the last indexed item action in the list gets executed, then removed
    public void GoBack()
    {
        if (backAction.Count > 0)
        {
            backAction[backAction.Count - 1].Invoke();
            backAction.RemoveAt(backAction.Count - 1);
        }
    }

    public void QuitGame()
    {Application.Quit();}
}