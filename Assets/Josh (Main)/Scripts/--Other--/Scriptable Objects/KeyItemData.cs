using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Key Item", menuName = "Items/Key Item")]
public class KeyItemData : ScriptableObject
{
    [SerializeField] int givenID;
    [SerializeField] string givenName;
    [SerializeField] string givenDescription;
    [SerializeField] GameObject givenKeyItem;
    [SerializeField] Sprite givenSprite;

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

    public GameObject GivenKeyItem
    {
        get { return givenKeyItem; }
    }

    public Sprite GivenSprite
    {
        get { return givenSprite; }
    }
}