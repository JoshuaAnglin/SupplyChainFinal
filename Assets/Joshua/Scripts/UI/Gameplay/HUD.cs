using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    // Right now, it's serialized. Later on, make it so that the items are already picked within the overworld, then transferred to this list
    [SerializeField] List<Weapon> weapons;

    [SerializeField] Text currentWeaponName;
    [SerializeField] GameObject weaponInventory;

    int currentWeaponRange = 0;

    int selectedItemIndex;

    void Start()
    {
        SwitchWeapon(ref currentWeaponRange, 0);
    }

    void Update()
    {
        // Weapon Switching
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(ref currentWeaponRange, -1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(ref currentWeaponRange, 1);
        }
    }

    void SwitchWeapon(ref int d, int offset)
    {
        d += offset;

        if (currentWeaponRange == weapons.Count) currentWeaponRange = 0;
        else if (currentWeaponRange < 0) currentWeaponRange = weapons.Count - 1;

        for (int a = 0; a < weaponInventory.transform.childCount; a++)
        {
            weaponInventory.transform.GetChild(a).GetChild(0).GetComponent<Image>().sprite = weapons[currentWeaponRange].givenImage;

            if (a == selectedItemIndex)
            {
                currentWeaponName.text = weapons[currentWeaponRange].name;
            }

            currentWeaponRange++;
        }
    }
}
