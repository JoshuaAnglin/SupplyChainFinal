using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStats : MonoBehaviour,idamage
{
    // this is player stats script

    public int strenght, maxHp, currentHP, maxLevel = 10, playerLvL = 1, currentXP, baseXP = 100, wpnPower, minhealth = 0;
    bool hasDied;
    public int[] expToNextLevel;
    public int[] hpLvLBonus;
    public static BasicStats instance;
    public bool isPlayer;
    [SerializeField]
    Component healthbar;
    
   
    void Start()
    {
        instance = this;
        isPlayer = true;
        currentHP = maxHp;
        updatehealth();

        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseXP;

        for(int i = 2; i < expToNextLevel.Length; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.2f);

        }

    }
    public void AddExp(int xpToAdd)
    {
        currentXP += xpToAdd;
       
        if (playerLvL < maxLevel)
        {
            if (currentXP >= expToNextLevel[playerLvL])
            {
                currentXP -= expToNextLevel[playerLvL];
                playerLvL++;
                if(playerLvL % 2 == 0)
                {
                    strenght += 5;
                }else
                {
                    maxHp += 150;
                }
            }
        }
        if(playerLvL >= maxLevel)
        {
            currentXP = 0;
        }
    }
    public void addhealth(int amount)
    {
        currentHP += amount;
        if (currentHP < minhealth) currentHP = minhealth;
        if (currentHP > maxHp) currentHP = maxHp;
        updatehealth();
    }
    public void updatehealth()
    {
        healthbar.transform.localScale = new Vector3((float)(currentHP - minhealth) / (maxHp - minhealth), 1.0f);
    }
   
    public void Update()
    {
        if(Input.GetKeyUp(KeyCode.K))
        {
            AddExp(100);
        }
    }
}
