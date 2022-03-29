using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingMaterial : MonoBehaviour
{
    public CraftingMaterialData info;

    void OnEnable()
    {
        gameObject.name = info.GivenName;
    }

    void Update()
    {
        transform.Rotate(-Vector3.up, 45 * Time.deltaTime);
    }
}
