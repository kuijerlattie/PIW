using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ItemDatabaseEnumerator {

    static ItemDatabase itemDatabase;

    [MenuItem("Tools/Enumerate ItemDatabase")]
    public static void EnumerateItemDatabase()
    {
        itemDatabase = Resources.Load("ScriptableObjects/ItemDatabase", typeof(ItemDatabase)) as ItemDatabase;

        int topweaponid = 0;
        foreach (GameObject gameobject in itemDatabase.weapons)
        {
            Weapon weapon = gameobject.GetComponent<Weapon>();
            if (weapon.ID != 0 && weapon.ID > topweaponid) topweaponid = weapon.ID;
        }

        foreach (GameObject gameobject in itemDatabase.weapons)
        {
            Weapon weapon = gameobject.GetComponent<Weapon>();
            if (weapon.ID == 0 && weapon.ID > topweaponid)
            {
                weapon.ID = topweaponid + 1;
                topweaponid++;
            }
        }
        
        //weapons all got ID now;
    }
}
