using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Holdable Item", menuName = "Items/Holdable")]
public class HoldableItem : ScriptableObject
{
    public int givenID;
    public string givenName;
    public Sprite givenImage;
    public string givenDescription;

    // Something which takes in weapon's advantage and disadvantage
}