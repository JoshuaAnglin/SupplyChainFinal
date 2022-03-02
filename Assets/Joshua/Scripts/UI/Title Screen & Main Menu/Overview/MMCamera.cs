using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMCamera : MonoBehaviour
{
    Animator anim;
    static public int direction;

    float rotClamp;
    Vector3 startPos = new Vector3(-37.93f, 4.81f, -8.8f);
    Vector3 startRot = new Vector3(-10, -60.37f, 0);

    float minScrollPos = 9f;
    float maxScrollPos = 11.6f;

    [SerializeField] Animator animDoor;
    [SerializeField] GameObject optionsIndicator;
    [SerializeField] GameObject craftingIndicator;

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

    void Update()
    {
        // Options Area
        if (MainMenu.inOptions)
        {
            if (transform.position.y < minScrollPos)
            {
                transform.position = new Vector3(transform.position.x, minScrollPos + 0.15f, transform.position.z);
            }

            else if (transform.position.y > maxScrollPos)
            {
                transform.position = new Vector3(transform.position.x, maxScrollPos - 0.15f, transform.position.z);
            }

            else transform.Translate(Vector3.up * (Input.GetAxis("Mouse ScrollWheel") * 2));
        }
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
        rotClamp = Mathf.Clamp(rotClamp + (dir * 0.3f), -77, 77);
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

    public void GoToRadio()
    {anim.SetBool("ModifyingSound", true);}
}