using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Materials", menuName = "Items/Materials")]
public class Materials : ScriptableObject
{
    public int givenID;
    public string givenName;
    public Sprite givenImage;
    public string givenDescription;
}