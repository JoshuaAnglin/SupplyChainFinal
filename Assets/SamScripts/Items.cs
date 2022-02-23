using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    BasicStats playerStats;
    public static Items instance;
    public bool canPickUp;
    [Header("Item Type")]
    public bool isWeapon;
    public bool isItem;
    // need more items to use then can expand this code
    [Header("Item Details")]
    public int extraStats;
    public Sprite itemSprite;
    public int weaponStrenght;
    public bool affectHP, affectStr;
    public string itemName;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(canPickUp && Input.GetButtonDown("Fire1"))
        {
            GameManager.instance.AddItem(GetComponent<Items>().itemName);
            Destroy(gameObject);
        }
    }
    public void UseItem()
    {
        // if the data is saved then need line under
        //BasicStats player = GameManager.instance.playerStats;

        if(affectHP)
        {
            // if the data will be saved need to chage the instance
            BasicStats.instance.maxHp += extraStats;
        }if(affectStr)
        {
            BasicStats.instance.strenght += extraStats;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            canPickUp = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canPickUp = false;
        }
    }
}
