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

	// Use this for initialization
	void Start () {
        ShowWeapons();
	}
	
	// Update is called once per frame
	void Update () {
		
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
        selectedWeapon.SetSelectedWeapon(oWeapon);
    }
}
