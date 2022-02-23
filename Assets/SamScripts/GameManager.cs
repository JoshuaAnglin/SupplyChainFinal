using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string[] itemsHeld;
    public int[] numberOfItems;
    public Items[] refItems;
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

   public Items GetItemDetail(string itemToTake)
    {
        for(int i = 0; i < refItems.Length; i++)
        {
            if(refItems[i].itemName == itemToTake)
            {
                return refItems[i];
            }
        }
        return null;
    }

    public void SortingItems()
    {
        bool itemSpace = true;

        while(itemSpace)
        {
            itemSpace = false;
            for (int i = 0; i < itemsHeld.Length - 1; i++)
            {
                if(itemsHeld[i] == "")
                {
                    itemsHeld[i] = itemsHeld[i + 1];
                    itemsHeld[i + 1] = "";

                    numberOfItems[i] = numberOfItems[i + 1];
                    numberOfItems[i + 1] = 0;

                    if(itemsHeld[i] != "")
                    {
                        itemSpace = true;
                    }
                }
            }
        }
    }
    public void AddItem(string itemToAdd)
    {
        int newItemPos = 0;
        bool isSpace = false;

        for(int i = 0; i < itemsHeld.Length; i++)
        {
            if(itemsHeld[i] == "" || itemsHeld[i] == itemToAdd)
            {
                newItemPos = i;
                i = itemsHeld.Length;
                isSpace = true;
            }
        }
        if(isSpace)
        {
            bool itemExist = false;
            for (int i = 0; i < refItems.Length; i++)
            {
                if (refItems[i].itemName == itemToAdd)
                {
                    itemExist = true;
                    i = refItems.Length;
                }

            }
            if (itemExist)
            {
                itemsHeld[newItemPos] = itemToAdd;
                numberOfItems[newItemPos]++;
            }
            else
            {
                Debug.LogError(itemToAdd + " Does not exist!!");
            }
            // here need to call the function from gameMenu to show the items
            GameMenu.instance.ShowItems();
        }
    }

    public void RemoveItem(string itemToRemove)
    {
        bool foundItem = false;
        int itemPosition = 0;

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == itemToRemove)
            {
                foundItem = true;
                itemPosition = i;

                i = itemsHeld.Length;

            }
        }
        if (foundItem)
        {
            numberOfItems[itemPosition]--;

            if (numberOfItems[itemPosition] <= 0)
            {
                itemsHeld[itemPosition] = "";
            }

            GameMenu.instance.ShowItems();
        }
        else
        {
            Debug.LogError("Couldn not find " + itemToRemove);
        }
    }
}
