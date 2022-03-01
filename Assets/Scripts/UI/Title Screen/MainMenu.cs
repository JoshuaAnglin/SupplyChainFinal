using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    int animationState;

    [SerializeField] GameObject overview, inArea, aaStageSelection, aaCrafting, aaCredits, aaOptions;
    [Space] [Space] [Space]
    public Text anP1Text, aNP2Text;

    static public MainMenu inst;

    void Awake()
    {
        Time.timeScale = 1;
        inst = this;
    }

    void OnEnable()
    {
        GameEventSystem.GES.onDefaultState += DefaultPosition;
        GameEventSystem.GES.onWithinAnArea += WithinArea;
        GameEventSystem.GES.onPlayStage += PickedStage;
    }

    void OnDisable()
    {
        GameEventSystem.GES.onDefaultState -= DefaultPosition;
        GameEventSystem.GES.onWithinAnArea -= WithinArea;
        GameEventSystem.GES.onPlayStage -= PickedStage;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && animationState != -1) BackToDefaultPosition();
    }

    public void BackToDefaultPosition()
    {GameEventSystem.GES.DefaultState();}

    void WithinArea(int animState)
    {
        overview.SetActive(false);
        inArea.SetActive(true);
    }

    public void DefaultPosition()
    {
        overview.SetActive(true);
        inArea.SetActive(false);
    }

    void PickedStage()
    {
        GameEventSystem.GES.WithinAnArea(animationState = 0);
        inArea.SetActive(false);
    }

    #region 3D Object Buttons

    public void StageSelector()
    {
        inArea.SetActive(true);
        aaStageSelection.SetActive(true);  
    }

    public void PlayStage()
    {
        GameEventSystem.GES.PlayStage();
    }

    public void ExitGame()
    {
        GameEventSystem.GES.WithinAnArea(animationState = 1);
        Application.Quit();
    }

    public void Crafting()
    {GameEventSystem.GES.WithinAnArea(animationState = 2);}

    public void Options()
    {GameEventSystem.GES.WithinAnArea(animationState = 3);}

    public void Scenery()
    {GameEventSystem.GES.WithinAnArea(animationState = 4);}

    public void Credits()
    {GameEventSystem.GES.WithinAnArea(animationState = 5);}

    public void SwitchState()
    {
        switch (GlobalScript.GameState)
        {
            case GlobalScript.GameplayStatus.inTitleScreen:
                foreach (Transform go in transform)
                { 
                    go.gameObject.SetActive(false);
                    transform.GetChild(0).gameObject.SetActive(true);
                }
                break;

            case GlobalScript.GameplayStatus.inMainMenu:
                foreach (Transform go in transform)
                {
                    go.gameObject.SetActive(true);
                }
                inArea.SetActive(false);
                break;
        }
    }
    #endregion

    #region Entering & Coming Back From Stage

    void WelcomeBack()
    {

    }

    #endregion
}