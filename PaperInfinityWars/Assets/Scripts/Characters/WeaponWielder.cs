using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponWielder : Hitpoints {

    [Header("Weapon settings")]
    public int WeaponSlots = 4;
    int selectedweapon = 1;
    List<Weapon> weaponlist;

    protected void NextWeapon()
    {
        selectedweapon += 1;
        if (selectedweapon > WeaponSlots)
            selectedweapon = 1;
    }

    protected void PreviousWeapon()
    {
        selectedweapon -= 1;
        if (selectedweapon <= 0)
            selectedweapon = WeaponSlots;
    }

    protected void EquipWeapon(Weapon oWeapon)
    {
        weaponlist[selectedweapon] = oWeapon;
    }

    protected void EquipWeapon(int iSlot, Weapon oWeapon)
    {
        weaponlist[iSlot] = oWeapon;
    }

    protected void RemoveWeapon()
    {
        weaponlist[selectedweapon] = null;
    }

    protected void RemoveWeapon(int iSlot)
    {
        weaponlist[iSlot] = null;
    }

}
