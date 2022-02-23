using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameMenu : MonoBehaviour
{

    public GameObject theMenu;
    public ItemButton[] itemButtons;
    public Items activeItems;
    public static GameMenu instance;
    public TextMeshProUGUI useButtonText, lvlTxt;
    public Slider xpBar;

    void Start()
    {
        instance = this;

       
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            theMenu.gameObject.SetActive(!theMenu.gameObject.activeSelf);
            ShowItems();
            GameManager.instance.SortingItems();
        }
    }
    public void ShowItems()
    {
        GameManager.instance.SortingItems();
        for(int i =0; i < itemButtons.Length; i++)
        {
            itemButtons[i].buttonvalue = i;
            if(GameManager.instance.itemsHeld[i] != "")
            {
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                itemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetail(GameManager.instance.itemsHeld[i]).itemSprite;
                itemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            }else
            {
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        }
    }
    public void SelectItem(Items newItem)
    {
        activeItems = newItem;
        if(activeItems.isItem)
        {
            useButtonText.text = "Use";
        }
    }
    public void DiscardItem()
    {
        if(activeItems != null)
        {
            GameManager.instance.RemoveItem(activeItems.itemName);
        }
    }
  public void UseItem()
    {
        activeItems.UseItem();
    }
    public void updateXPBar()
    {
        float x, y;
        x = BasicStats.instance.currentXP;
        y = BasicStats.instance.expToNextLevel[BasicStats.instance.playerLvL];

        xpBar.maxValue = y;
        xpBar.value = x;
        
        for(int i = 1; i <= BasicStats.instance.playerLvL;i++)
        {
            lvlTxt.text = "Level:" + i;
        }
    }
    public void LateUpdate()
    {
        updateXPBar();
    }
}
