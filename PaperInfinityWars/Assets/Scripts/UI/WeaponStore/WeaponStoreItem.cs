using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponStoreItem : MonoBehaviour {

    public Image image;
    public Text text;

    [HideInInspector]
    public Weapon weapon;
    WeaponStoreScript weaponStore;

    public void SetWeapon(Weapon oWeapon, WeaponStoreScript oWeaponStore)
    {
        weapon = oWeapon;
        image.sprite = weapon.equipImage;
        text.text = weapon.storeName;
        weaponStore = oWeaponStore;
    }

    public void OnClick()
    {
        weaponStore.ShowSelectedItem(weapon);
    }
}
