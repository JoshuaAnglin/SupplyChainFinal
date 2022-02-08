using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thing : MonoBehaviour
{
    void Update()
    {
        gameObject.transform.Rotate(-Vector3.up, 45 * Time.deltaTime);
    }
}
