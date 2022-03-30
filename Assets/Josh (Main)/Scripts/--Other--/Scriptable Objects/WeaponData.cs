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
    public Sprite givenSprite;
    [Space][Space]
    public int givenAttack;
    public int givenDefense;

    public int GivenID
    {
        get { return givenID; }
    }

    public string GivenName
    {
        get { return givenName; }
    }

    public string GivenDescription
    {
        get { return givenDescription; }
    }

    public GameObject GivenWeapon
    {
        get { return givenWeapon; }
    }

    public Sprite GivenSprite
    {
        get { return givenSprite; }
    }

    public Sprite GivenAttack
    {
        get { return givenSprite; }
    }

    public Sprite GivenDefense
    {
        get { return givenSprite; }
    }

    void SpecialAbility()
    {

    }
}