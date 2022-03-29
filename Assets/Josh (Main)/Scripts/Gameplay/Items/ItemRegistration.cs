using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemRegistration : MonoBehaviour
{
    static public ItemRegistration ir { get; private set; }

    [SerializeField] List<CraftingMaterialData> CraftingMaterials = new List<CraftingMaterialData>();
    [Space]
    [SerializeField] List<KeyItemData> KeyItems = new List<KeyItemData>();
    [Space]
    [SerializeField] List<WeaponData> Weapons = new List<WeaponData>();

    [HideInInspector] public int[] storageWIPickUp = new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1};
    [HideInInspector] public int[] storageWIWeapons = new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };

    void Awake()
    {ir = this;}

    void Start()
    {
        //Instantiate(CraftingMaterials[0].GivenCraftingMaterial, new Vector3(0, 2, 0), Quaternion.identity);
    }

    public CraftingMaterialData GetCraftingMaterial(int id)
    {
        foreach (CraftingMaterialData cm in CraftingMaterials)
        {
            if (id == cm.GivenID) return cm;
        }

        return null;
    }
}