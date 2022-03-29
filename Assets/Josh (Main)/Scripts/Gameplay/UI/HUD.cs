using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    // Health & Level
    [SerializeField] Image imgHealth, imgLevel;
    [SerializeField] Text txtHealth, txtLevel;

    // Time & Whole Inventory
    [SerializeField] Button btnTime;
    float minutes = 5;
    float seconds = 00;
    [Space]
    [Space]

    // HUD Inventory

    [SerializeField] List<Image> hudInventorySlots = new List<Image>();
    [SerializeField] Text currentItem;

    static public HUD hud;

    void Awake()
    {
        GlobalScript.gs = GlobalScript.GameStatus.inGame;
        hud = GetComponent<HUD>();
    }

    void Update()
    {
        Timer();
        if (Input.GetKeyDown(KeyCode.Alpha4)) SceneManager.LoadScene(0);

        imgHealth.fillAmount = Mathf.Lerp(imgHealth.fillAmount, 0, 2f * Time.deltaTime);
        txtHealth.text = "HP (" + (imgHealth.fillAmount * 100).ToString("00") + "%)";
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

    // HUD Inventory
    public void AddToInventory<T>(T cm) where T : MonoBehaviour
    {
        for (int a = 0; a < ItemRegistration.ir.storageWIPickUp.Length; a++)
        {
            if (ItemRegistration.ir.storageWIPickUp[a] == -1)
            {
                if (cm.GetComponent<CraftingMaterial>())
                {ItemRegistration.ir.storageWIPickUp[a] = cm.GetComponent<CraftingMaterial>().info.GivenID;}

                else if (cm.GetComponent<KeyItem>())
                {ItemRegistration.ir.storageWIPickUp[a] = cm.GetComponent<KeyItem>().info.GivenID; }

                RefreshGameplayHUD();
                break;
            }
        }
    }

    public void RemoveFromInventory(int pos)
    {
        hudInventorySlots[pos].GetComponent<Image>().sprite = null;
        ItemRegistration.ir.storageWIPickUp[pos] = -1;   
    }

    void RefreshGameplayHUD()
    {
        List<Sprite> images = new List<Sprite>();

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
        {hudInventorySlots[b].sprite = images[b];}
    }
}