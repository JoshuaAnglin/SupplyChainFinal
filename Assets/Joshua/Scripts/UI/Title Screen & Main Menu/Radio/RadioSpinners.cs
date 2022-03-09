using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioSpinners : MonoBehaviour
{
    void Start()
    {
        // set rotations;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.localRotation *= new Quaternion(-1, 0, 0, 1);
        }

        else if (Input.GetKey(KeyCode.A))
        {
            transform.localEulerAngles = new Vector3(1, 0, 0);
        }
    }
}
