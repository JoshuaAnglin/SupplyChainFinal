using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour, IPickUp
{
    Rigidbody rb;
    float launchSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        launchSpeed = LaunchSpeed();
    }

    public float LaunchSpeed()
    {return launchSpeed;}

    public void C()
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