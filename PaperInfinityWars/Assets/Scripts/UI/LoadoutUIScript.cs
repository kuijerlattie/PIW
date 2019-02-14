using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

class LoadoutUIScript : MonoBehaviour
{
    public Image currentWeaponIcon;
    public Image currentEquipmentIcon;
    public Image currentEquipmentCharge;
    public TextMeshProUGUI currentEquipmentCount;

    void OnEnable()
    {
        GameManager.instance.eventManager.WeaponChanged.AddListener(UpdateWeaponInfo);
        GameManager.instance.eventManager.EquipmentChanged.AddListener(UpdateEquipmentInfo);
    }

    void OnDisable()
    {
        GameManager.instance.eventManager.WeaponChanged.RemoveListener(UpdateWeaponInfo);
        GameManager.instance.eventManager.EquipmentChanged.AddListener(UpdateEquipmentInfo);
    }

    void UpdateWeaponInfo(Weapon oWeapon)
    {
        Debug.Log("weapon ui updated");
        currentWeaponIcon.sprite = oWeapon.equipImage;
    }

    void UpdateEquipmentInfo(Weapon oEquipment)
    {
        currentEquipmentIcon.sprite = oEquipment.equipImage;
        currentEquipmentCharge.fillAmount = oEquipment.chargeprogress;
        currentEquipmentCount.text = oEquipment.currentUses.ToString();
    }
}