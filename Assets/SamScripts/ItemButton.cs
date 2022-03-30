using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    public Image buttonImage;
    public TextMeshProUGUI amountText;
    public int buttonvalue;

    public void Press()
    {
        if(GameMenu.instance.theMenu.activeInHierarchy)
        {
            if(GameManager.instance.itemsHeld[buttonvalue] != "")
            {
                GameMenu.instance.SelectItem(GameManager.instance.GetItemDetail(GameManager.instance.itemsHeld[buttonvalue]));
            }
        }
    }
}
