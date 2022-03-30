using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEventSystemGameplay : MonoBehaviour
{
    bool inOptions;
    bool inStageSelector;

    static public GameEventSystemGameplay GESGameplay;

    [SerializeField] GameObject uiInventoryScreen;
    [SerializeField] GameObject uiPauseMenu;
    [SerializeField] GameObject uiOptions;
    [SerializeField] GameObject uiStageSelector;

    //[SerializeField] List<Image> inventoryPickUps = new List<Image>();
    //[SerializeField] List<Image> inventoryWeapons = new List<Image>();

    void Awake()
    {
        Time.timeScale = 1;

        GESGameplay = this; 
        uiPauseMenu.SetActive(false);
        uiOptions.SetActive(false);
        uiStageSelector.SetActive(false);
    }

    #region Event Methods

        #region UI

    public event Action onUnpaused;
    public event Action onPaused;
    public event Action onRestart;
    public event Action onOptions;
    public event Action onStageSelector;
    public event Action onExitGame;

    // Player unpauses the game
    public void PauseState()
    {
        switch (GlobalScript.gps)
        {
            case GlobalScript.GameplayStatus.Paused:
                pauseAction(GlobalScript.GameplayStatus.Gameplay, 1, CursorLockMode.Locked, false, false, onUnpaused);
                break;

            case GlobalScript.GameplayStatus.Gameplay:
                pauseAction(GlobalScript.GameplayStatus.Paused, 0, CursorLockMode.None, true, true, onPaused);
                break;
        }

        void pauseAction(GlobalScript.GameplayStatus state, int timeScale, CursorLockMode cursorLM, bool cursorVisible, bool pauseMenu, Action givenAction)
        {
            GlobalScript.gps = state;
            Time.timeScale = timeScale;
            Cursor.lockState = cursorLM;
            Cursor.visible = cursorVisible;
            uiPauseMenu.SetActive(pauseMenu);
            givenAction?.Invoke();
        }
    }
    
    // Restart the stage
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //if (onRestart != null) onRestart();
    }

    // Options section
    public void Options()
    {
        if (!inOptions) uiOptions.SetActive(true);
        else uiOptions.SetActive(false);

        //if (onOptions != null) onOptions();
    }

    // Stage selector is opened
    public void StageSelector()
    {
        if (!inStageSelector) uiStageSelector.SetActive(true);
        else uiStageSelector.SetActive(false);

        //if (onStageSelector != null) onStageSelector();
    }

    // Exiting the game
    public void ExitGame()
    {
        if (onExitGame != null) onExitGame();
        Application.Quit();
    }

        #endregion

        #region Gameplay

    public event Action onEnteringStage;
    public event Action onPickUpKeyItem;
    public event Action onDropKeyItem;
    public event Action onSwitchInventory;
    public event Action onOpenInventory;

    public void EnteringStage()
    {
        if (onEnteringStage != null) onEnteringStage();
    }

    public void PickUpKeyItem()
    {
        if (onPickUpKeyItem != null) onPickUpKeyItem();
    }

    public void DropKeyItem()
    {
        if (onDropKeyItem != null) onDropKeyItem();
    }

    public void SwitchInventory()
    {
        //Inventory();
        if (onOpenInventory != null) onOpenInventory();
    }

    public void OpenInventory()
    {
        //Inventory();
        if (onOpenInventory != null) onOpenInventory();
    }

    #endregion

    #endregion

    #region Self-Methods

    /*void Inventory()
    {
        if (invetoryScreen.activeSelf) invetoryScreen.SetActive(false);

        else if (!invetoryScreen.activeSelf) {
            invetoryScreen.SetActive(true);

            for(int a = 0; a < inventoryPickUps.Count; a++)
            {
                int id = ItemRegistration.ir.storageWIPickUp[a];

                inventoryPickUps[a].sprite = id != -1 ? ItemRegistration.ir.GetCraftingMaterial(id).GivenSprite : null;
            }
        }
    }*/

    #endregion
}

// ESCAPE TO GO BACK TO PREVIOUS SECTION