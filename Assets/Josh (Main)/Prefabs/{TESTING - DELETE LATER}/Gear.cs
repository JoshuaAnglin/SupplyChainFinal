using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour, IPickUp
{
    [SerializeField] List<GameObject> gears = new List<GameObject>();
    [SerializeField] int[] spinDirection = new int[] { };
    Rigidbody rb;
    [SerializeField] float launchSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        BeginRotation();
    }

    public float LaunchSpeed()
    {return launchSpeed;}

    bool IPickUp.CanRotate()
    {return true;}

    public GameObject ThisObject()
    {return gameObject;}

    void BeginRotation()
    {
        int a;

        for(int i = 0; i < gears.Count; i++)
        {
            a = i;
            if (a == 1)
            {
                gears[i].transform.Rotate(-transform.up, 10 * Time.deltaTime, Space.World);
                a = 0;
            }
            else gears[i].transform.Rotate(transform.up, 10 * Time.deltaTime, Space.World);
        }
    }
}