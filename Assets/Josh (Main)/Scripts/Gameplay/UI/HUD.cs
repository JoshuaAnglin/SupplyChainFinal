using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    // Health & Level
    [Header("Left-Hand Side")]
    [SerializeField] Image imgHealth, imgLevel;
    [SerializeField] Text txtHealth, txtLevel;
    [Space]
    [Space]

    // Time & Whole Inventory
    [Header("Middle")]
    [SerializeField] Button btnTime;
    float minutes = 5;
    float seconds = 00;
    [Space]
    [Space]

    // HUD Inventory
    [Header("Right-Hand Side")]
    [SerializeField] Text currentItem;
    [SerializeField] GameObject sectionKeyItem;
    [SerializeField] GameObject sectionWeapon;

    void Awake()
    {
        GlobalScript.state = GlobalScript.GameState.inGame;
        sectionKeyItem.SetActive(true);
    }

    void Update()
    {
        Timer();

        //imgHealth.fillAmount = Mathf.Lerp(imgHealth.fillAmount, 0, 2f * Time.deltaTime);
        //txtHealth.text = "HP (" + (imgHealth.fillAmount * 100).ToString("00") + "%)";
    }

    // Countdown Timer
    void Timer()
    {
        seconds -= Time.deltaTime;

        TimeSystem(seconds <= -1, -1, 59);
        TimeSystem(seconds >= 60, 1, 00);

        if (minutes <= 0 && seconds <= 0) seconds = minutes = 0;

        btnTime.transform.GetChild(0).GetComponent<Text>().text = minutes.ToString("00") + ":" + (Mathf.CeilToInt(seconds)).ToString("00");

        void TimeSystem(bool Decider, int newMin, int newSec)
        {
            if (Decider)
            {
                minutes += newMin;
                seconds = newSec;
            }
        }
    }

    #region Adding To HUD
    // Switch between both right HUDs
    void SwitchRightHUD(bool isKeyItemActive, bool isWeaponActive)
    {
        sectionKeyItem.SetActive(isKeyItemActive);
        sectionWeapon.SetActive(isWeaponActive);
    }

    // Add Key Item to HUD
    public void AddToHUD(KeyItem ki)
    {
        SwitchRightHUD(true, false);
        sectionKeyItem.transform.GetChild(0).GetComponent<Image>().sprite = ki.infoKID.GivenSprite;
        currentItem.text = ki.infoKID.GivenName;
    }

    // Remove Key Item to HUD
    public void RemoveFromHUD()
    {
        sectionKeyItem.transform.GetChild(0).GetComponent<Image>().sprite = null;
        currentItem.text = "";
    }
    #endregion

    public void RemoveFromInventory(int pos)
    {
        //hudInventorySlots[pos].GetComponent<Image>().sprite = null;
        //ItemRegistration.ir.storageWIPickUp[pos] = -1;   
    }

    void RefreshGameplayHUD()
    {
        /*List<Sprite> images = new List<Sprite>();

        for(int a = 0; a < hudInventorySlots.Count; a++)
        {
            int ID = ItemRegistration.ir.storageWIPickUp[a];

            switch (ID)
            {
                case int x when ID >= 0 && ID < 100:
                    images.Add(ItemRegistration.ir.GetCraftingMaterial(ID).GivenSprite);
                    break;

                case int x when ID >= 100 && ID < 200:
                    //ItemRegistration.ir.GetKeyItem(ID);
                    break;
            }
        }

        for(int b = 0; b < images.Count; b++)
        {hudInventorySlots[b].sprite = images[b];} */
    }
}