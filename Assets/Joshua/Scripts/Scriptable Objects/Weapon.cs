using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapons")]
public class Weapon : ScriptableObject
{
    public int givenID;
    public Object givenObject;
    public string givenName;
    public Sprite givenImage;
    public string givenDescription;
    // Something which takes in weapon's advantage and disadvantage
}
