using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "SO/DontTouch/ItemDatabase", order = 1)]
public class ItemDatabase : ScriptableObject {

    public List<GameObject> weapons;

    public GameObject GetWeaponByID(int oID)
    {
        return weapons.Find(X => X.GetComponent<Weapon>().ID == oID);
    }

    public void LoadWeaponUnlocks(SaveData savedata = null)
    {
        foreach (GameObject w in weapons)
        {
            if (GameManager.instance.savegameManager.saveData.weaponUnlocks.ContainsKey(w.GetComponent<Weapon>().ID))
            {
                w.GetComponent<Weapon>().unlocked = true;
            }
            else
            {
                w.GetComponent<Weapon>().unlocked = false;
            }
        }
    }
}
