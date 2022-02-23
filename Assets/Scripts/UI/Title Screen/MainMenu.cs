using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject overview;
    [SerializeField] GameObject craftingArea;
    [Space] [Space] [Space]
    public Text anP1Text;
    public Text aNP2Text;

    public Animator vCamAnim;
    string animState = "AreaState";

    static public MainMenu inst;

    public enum MenuState
    {
        TitleScreen,
        MainMenu,
        MainMenuWithinArea
    }

    public MenuState menuState = MenuState.TitleScreen;

    void Awake()
    {
        SwitchState(menuState);
        Time.timeScale = 1.0f;
        inst = this;
    }

    void OnEnable()
    {
        GameEventSystem.GES.onWithinAnArea += WithinArea;
    }

    void OnDisable()
    {
        GameEventSystem.GES.onWithinAnArea -= WithinArea;
    }

    void WithinArea()
    {
        overview.SetActive(false);
    }

    #region 3D Object Buttons

    public void PlayStage()
    {
        // Camera Goes to Door
        // Door slowly opens

        GameEventSystem.GES.WithinAnArea();
        vCamAnim.enabled = true;
        SceneManager.LoadScene(1);
        vCamAnim.SetInteger(animState, 0);
    }

    public void StageSelector()
    {
        // Stage selector screen opens
        // UI Focuses on Map   
    }

    public void ExitGame()
    {
        //GameEventSystem.GES.WithinAnArea();
        //vCamAnim.SetInteger(animState, 1);
        Application.Quit();
    }

    public void Crafting()
    {
        GameEventSystem.GES.WithinAnArea();
        vCamAnim.enabled = true;
        vCamAnim.SetInteger(animState, 2);
    }

    public void Options()
    {
        GameEventSystem.GES.WithinAnArea();
        vCamAnim.enabled = true;
        vCamAnim.SetInteger(animState, 3);
    }

    public void Scenery()
    {
        GameEventSystem.GES.WithinAnArea();
        vCamAnim.enabled = true;
        vCamAnim.SetInteger(animState, 4);
    }

    public void Credits()
    {
        GameEventSystem.GES.WithinAnArea();
        vCamAnim.enabled = true;
        vCamAnim.SetInteger(animState, 5);
    }

    public void SwitchState(MenuState newState)
    {
        switch (newState)
        {
            case MenuState.TitleScreen:
                gameObject.SetActive(false);
                break;

            case MenuState.MainMenu:
                gameObject.SetActive(true);
                break;

            case MenuState.MainMenuWithinArea:
                break;
        }
    }

    #endregion

    
}