using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    int animationState;

    [Header("(Default) Screen Text")] [SerializeField] Text areaNameTitle, areaNameDescription;

    [Header("(Default) UI Position")][SerializeField] GameObject overview;
    [Header("Within An Area")] [SerializeField] GameObject inArea;

    [Header("Within a Specific Area")]
    [SerializeField] GameObject aaStageSelection;
    [SerializeField] GameObject aaCrafting;
    [SerializeField] GameObject aaCredits;
    [SerializeField] GameObject aaOptions;
    [Space] [Space] [Space]
    
    static public MainMenu inst;

    void Awake()
    {
        Time.timeScale = 1;
        inst = this;
    }

    void OnEnable()
    {
        GameEventSystemMainMenu.GESMainMenu.onDefaultState += DefaultPosition;
        GameEventSystemMainMenu.GESMainMenu.onWithinAnArea += WithinArea;
        GameEventSystemMainMenu.GESMainMenu.onPlayStage += PickedStage;
    }

    void OnDisable()
    {
        GameEventSystemMainMenu.GESMainMenu.onDefaultState -= DefaultPosition;
        GameEventSystemMainMenu.GESMainMenu.onWithinAnArea -= WithinArea;
        GameEventSystemMainMenu.GESMainMenu.onPlayStage -= PickedStage;
    }

    void Update()
    {
        // Within Area
        if (GlobalScript.gs == GlobalScript.GameStatus.inMainMenu && Input.GetKeyDown(KeyCode.Escape) && animationState != -1)
        {
            BackToDefaultPosition();
            MMCamera.backAction.Clear();
        }
    }

    public void setScreenText(string Title, string Description)
    {
        areaNameTitle.text = Title;
        areaNameDescription.text = Description;
    }

    #region 3D Object Buttons

    public void StageSelector()
    {
        inArea.SetActive(true);
        aaStageSelection.SetActive(true);  
    }

    public void PlayStage()
    {
        GameEventSystemMainMenu.GESMainMenu.PlayStage();
        GlobalScript.mms = GlobalScript.MainMenuStatus.inPlay;
    }

    public void ExitGame()
    {
        GameEventSystemMainMenu.GESMainMenu.WithinAnArea(animationState = 1);
        GlobalScript.mms = GlobalScript.MainMenuStatus.inExit;
    }

    public void Crafting()
    {
        GameEventSystemMainMenu.GESMainMenu.WithinAnArea(animationState = 2);
        GlobalScript.mms = GlobalScript.MainMenuStatus.inCrafting;
        MMCamera.backAction.Add(BackToDefaultPosition);
    }

    public void Options()
    {
        GameEventSystemMainMenu.GESMainMenu.WithinAnArea(animationState = 3);
        GlobalScript.mms = GlobalScript.MainMenuStatus.inOptions;
        MMCamera.backAction.Add(BackToDefaultPosition);
    }

    public void Scenery()
    {
        GameEventSystemMainMenu.GESMainMenu.WithinAnArea(animationState = 4);
        GlobalScript.mms = GlobalScript.MainMenuStatus.inScenery;
        MMCamera.backAction.Add(BackToDefaultPosition);
    }

    public void Credits()
    {
        GameEventSystemMainMenu.GESMainMenu.WithinAnArea(animationState = 5);
        GlobalScript.mms = GlobalScript.MainMenuStatus.inCredits;
        MMCamera.backAction.Add(BackToDefaultPosition);
    }

    public void ModifyOptions()
    {
        Debug.Log("Options");
        GameEventSystemMainMenu.GESMainMenu.WithinAnArea(animationState = 31);
        MMCamera.backAction.Add(BackToOptions);
    }

    public void GoToRadio()
    {
        Debug.Log("Options");
        GameEventSystemMainMenu.GESMainMenu.WithinAnArea(animationState = 32);
        MMCamera.backAction.Add(BackToOptions);
    }

    #endregion

    public void BackToDefaultPosition()
    {
        GameEventSystemMainMenu.GESMainMenu.WithinAnArea(animationState = -1);
        GameEventSystemMainMenu.GESMainMenu.DefaultState();
    }

    void BackToOptions()
    {GameEventSystemMainMenu.GESMainMenu.WithinAnArea(animationState = 3);}

    void WithinArea(int animState)
    {
        overview.SetActive(false);
        inArea.SetActive(true);
    }

    public void DefaultPosition()
    {
        GlobalScript.mms = GlobalScript.MainMenuStatus.Default;
        overview.SetActive(true);
        inArea.SetActive(false);
    }

    void PickedStage()
    {
        GameEventSystemMainMenu.GESMainMenu.WithinAnArea(animationState = 0);
        inArea.SetActive(false);
    }
}