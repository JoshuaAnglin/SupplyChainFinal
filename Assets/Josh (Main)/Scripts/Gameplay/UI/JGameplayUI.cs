using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JGameplayUI : MonoBehaviour
{
    [SerializeField] Image fadeInScreen;
    [SerializeField] GameObject InventoryWhole;

    void Awake()
    {
        fadeInScreen.gameObject.SetActive(true);
    }

    void Update()
    {
        ControlInputs();

        fadeInScreen.ScreenFade(0, 0.8f);
    }

    #region UI Inputs

    void ControlInputs()
    {
        if (Input.GetKeyDown(GlobalScript.pauseUnpause)) SwitchPauseState();

        if (Input.GetKeyDown(GlobalScript.openInventory))
        {
            if (InventoryWhole.activeSelf) InventoryWhole.SetActive(false);
            else InventoryWhole.SetActive(true);
        }
    }

    #endregion

    #region ButtonEvents()

        #region Subscribed Button Events
    // (Play/Resume) Resume the game
    public void SwitchPauseState()
    {GameEventSystemGameplay.GESGameplay.PauseState();}

    // (Restart) Restart the stage
    public void RestartStage()
    {GameEventSystemGameplay.GESGameplay.Restart();}

    // (Options) Go Into Options
    public void Options()
    {GameEventSystemGameplay.GESGameplay.Options();}

    // (Stage Selector) Select a stage
    public void StageSelector()
    {GameEventSystemGameplay.GESGameplay.StageSelector();}

    // (Exit) Exit the Game
    public void ExitGame()
    {GameEventSystemGameplay.GESGameplay.ExitGame();}
        #endregion

        #region Normal Button Events

    public void SelectedStage(int stage)
    {
        SceneManager.LoadScene(stage);
    }

        #endregion

    #endregion
}
