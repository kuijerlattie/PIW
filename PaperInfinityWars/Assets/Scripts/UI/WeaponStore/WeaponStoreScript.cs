using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponStoreScript : MonoBehaviour {

    public Button buttonWeapons;
    public Button buttonEquipment;

    public RectTransform Content;

    public GameObject storeItem;

    public List<GameObject> storeInventory;

    public SelectedWeaponStoreScript selectedWeapon;

    bool showingWeapons = true;

    Weapon selectedweapon;


    public GameObject buyCanvas;
    public GameObject equipWeaponCanvas;
    public GameObject equipEquipmentCanvas;
    public GameObject EquipSlotCanvas;

    // Use this for initialization
    void OnEnable()
    {
        ResetStore();
        ShowWeapons();
    }

	// Update is called once per frame
	void Update () {
		
	}

    void ResetStore()
    {
        selectedweapon = null;
        ShowSelectedItem(selectedweapon);
        
    }

    void ClearOldItems()
    {
        //destroy all old objects
        foreach (RectTransform r in Content.transform.GetComponentInChildren<RectTransform>())
        {
            Destroy(r.gameObject);
        }
    }

    public void ShowWeapons()
    {
        ClearOldItems();

        foreach (GameObject item in storeInventory)
        {
            if (!item.GetComponent<Weapon>().isEquipment)
            {
                GameObject instance = Instantiate(storeItem, Content);
                instance.GetComponent<WeaponStoreItem>().SetWeapon(item.GetComponent<Weapon>(), this);
            }
        }
        showingWeapons = true;
    }

    public void ShowEquipment()
    {
        ClearOldItems();

        foreach (GameObject item in storeInventory)
        {
            if (item.GetComponent<Weapon>().isEquipment)
            {
                GameObject instance = Instantiate(storeItem, Content);
                instance.GetComponent<WeaponStoreItem>().SetWeapon(item.GetComponent<Weapon>(), this);
            }
        }
        showingWeapons = false;
    }

    public void ShowSelectedItem(Weapon oWeapon)
    {
        selectedweapon = oWeapon;
        selectedWeapon.SetSelectedWeapon(oWeapon);

        //make sure everything else is hidden
        equipEquipmentCanvas.SetActive(false);
        equipWeaponCanvas.SetActive(false);
        buyCanvas.SetActive(false);
        EquipSlotCanvas.SetActive(false);
        if (selectedweapon == null)
            return;

        Debug.Log(selectedweapon.unlocked);
        if (!selectedweapon.unlocked)
        {
            //show buy button
            if (GameManager.instance.currencyManager.GetCoins() >= selectedweapon.storeCost)
            {
                buyCanvas.gameObject.SetActive(true);
            }
            else
            {
                //show get more coins here
            }
        }
        else
        {
            if (selectedweapon.isEquipment)
            {
                //show equip equipment button
                equipEquipmentCanvas.SetActive(true);
            }
            else
            {
                //show equip weapon button
                equipWeaponCanvas.SetActive(true);
            }
        }
    }

    public void EquipSelectedWeapon(int slot)
    {
        GameManager.instance.player.GetComponent<WeaponSlots>().EquipWeapon(slot, GameManager.instance.itemManager.GetWeaponByID(selectedweapon.ID));
    }

    public void EquipSelectedEquipment()
    {
        GameManager.instance.player.GetComponent<WeaponSlots>().EquipEquipment(GameManager.instance.itemManager.GetWeaponByID(selectedweapon.ID));
    }

    public void ShowEquipWeaponScreen()
    {
        EquipSlotCanvas.SetActive(true);
    }

    public void HideEquipWeaponScreen()
    {
        EquipSlotCanvas.SetActive(false);
    }

    public void BuyWeapon()
    {
        //remove money, unlock weapon
        GameManager.instance.currencyManager.AddCoins(-selectedweapon.storeCost);
        GameManager.instance.savegameManager.saveData.weaponUnlocks.Add(selectedweapon.ID, true);
        GameManager.instance.savegameManager.Save();
        selectedweapon.unlocked = true;
        ShowSelectedItem(selectedweapon);
    }
}
