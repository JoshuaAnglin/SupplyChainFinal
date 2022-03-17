using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon")]
public class WeaponData : ScriptableObject
{
    public int givenID;
    public string givenName;
    public string givenDescription;
    public GameObject givenWeapon;
    public Sprite givenImage;
    [Space][Space]
    public int givenAttack;
    public int givenDefense;

    void SpecialAbility()
    {

    }
}