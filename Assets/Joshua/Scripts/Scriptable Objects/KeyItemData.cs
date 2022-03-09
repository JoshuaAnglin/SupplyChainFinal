using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Key Item", menuName = "Items/Key Item")]
public class KeyItemData : ScriptableObject
{
    public int givenID;
    public string givenName;
    public string givenDescription;
    public GameObject givenKeyItem;
    public Sprite givenImage;
}