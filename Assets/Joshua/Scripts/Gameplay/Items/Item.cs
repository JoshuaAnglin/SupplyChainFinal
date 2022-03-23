using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IPickUp
{
    Rigidbody rb;
    public float launchSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

    }

    public void Effect()
    {

    }
}
