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
    public List<Button> hudInventorySlots = new List<Button>();
    [SerializeField] Text currentItem;

    static public HUD hud;

    void Awake()
    {
        GlobalScript.GameState = GlobalScript.GameplayStatus.inGame;
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
    public void AddToInventory(CraftingMaterial cm)
    {
        foreach (Button hudInventorySlot in hudInventorySlots)
        {
            Image btnSprite = hudInventorySlot.GetComponent<Image>();

            if (btnSprite.sprite == null)
            {
                btnSprite.sprite = cm.info.GivenSprite;
                break;
            }
        }
    }
}