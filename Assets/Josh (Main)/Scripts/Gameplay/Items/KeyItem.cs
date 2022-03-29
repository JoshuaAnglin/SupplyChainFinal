using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour, IPickUp
{
    public KeyItemData info;
    Rigidbody rb;
    [SerializeField] float launchSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        launchSpeed = LaunchSpeed();
    }

    public float LaunchSpeed()
    { return launchSpeed; }

    public void Rotation()
    {
        throw new System.NotImplementedException();
    }

    public bool CanRotate()
    {
        throw new System.NotImplementedException();
    }

    public GameObject ThisObject()
    {
        throw new System.NotImplementedException();
    }
}