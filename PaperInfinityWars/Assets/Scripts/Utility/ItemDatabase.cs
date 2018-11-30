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
}
