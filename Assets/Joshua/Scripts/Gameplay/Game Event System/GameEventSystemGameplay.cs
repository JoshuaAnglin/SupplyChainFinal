using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEventSystemGameplay : MonoBehaviour
{
    static public GameEventSystemGameplay GESGameplay;

    [SerializeField] GameObject invetoryScreen;
    [SerializeField] List<Image> inventoryPickUps = new List<Image>();
    [SerializeField] List<Image> inventoryWeapons = new List<Image>();

    public event Action onEnteringGame;
    public event Action onOpenInventory;
    public event Action onDropItem;

    void Awake()
    { GESGameplay = this; }

    #region Event Methods
    public void OpenInventory()
    {
        Inventory();
        if (onEnteringGame != null) onEnteringGame(); 
    }
    #endregion

    #region Own Methods
    // Self-Methods
    void Inventory()
    {
        if (invetoryScreen.activeSelf) invetoryScreen.SetActive(false);

        else if (!invetoryScreen.activeSelf) {
            invetoryScreen.SetActive(true);

            for(int a = 0; a < inventoryPickUps.Count; a++)
            {
                int id = ItemRegistration.ir.storageWIPickUp[a];

                inventoryPickUps[a].sprite = id != -1 ? ItemRegistration.ir.GetCraftingMaterial(id).GivenSprite : null;

                Debug.Log(inventoryPickUps[a]);
            }
        }
    }
    #endregion
}
