using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemRegistration : MonoBehaviour
{
    public static ItemRegistration itemRegister { get; private set; }

    [SerializeField] List<KeyItemData> KeyItems = new List<KeyItemData>();
    [Space]
    [SerializeField] List<CraftingMaterialData> CraftingMaterials = new List<CraftingMaterialData>();
    [Space]
    [SerializeField] List<WeaponData> Weapons = new List<WeaponData>();

    public event Action onUpdate;

    void Awake()
    {
        if (itemRegister != null && itemRegister != this) Destroy(this);
        else itemRegister = this;

        DontDestroyOnLoad(this);
    }

    void Start()
    {
        GameObject s = Instantiate(CraftingMaterials[0].GivenCraftingMaterial, new Vector3(0, 2, 0), Quaternion.identity);
    }
}
