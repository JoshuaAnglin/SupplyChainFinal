using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
abstract public class KeyItem : MonoBehaviour, IPickUp
{
    public KeyItemData infoKID;
    public Rigidbody rb;
    public float launchSpeed;

    /*public void VariableAssign(int KeyItemID)
    {
        rb = GetComponent<Rigidbody>();
        infoKID = ItemRegistration.ir.GetKeyItem(KeyItemID);
    }*/

    public float LaunchSpeed()
    {return launchSpeed;}

    public bool CanRotate()
    {return true;}

    public GameObject ThisObject()
    {return rb.gameObject;}
}