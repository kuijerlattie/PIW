using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectedWeaponStoreScript : MonoBehaviour {

    public Image weaponImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI priceText;

	// Use this for initialization
	void Start () {
		//use to hide empty inspection thing
	}

    public void SetSelectedWeapon(Weapon oWeapon)
    {
        Debug.Log("showing " + oWeapon.storeName);
        weaponImage.sprite = oWeapon.equipImage;
        nameText.text = oWeapon.storeName;
        descriptionText.text = oWeapon.StoreDescription;
        priceText.text = oWeapon.storeCost.ToString();
    }
}
