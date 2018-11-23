using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTable : MonoBehaviour {

    [System.Serializable]
    public class DropTableEntry
    {
        public GameObject item;
        public int dropchance;
    }

    public List<DropTableEntry> dropTable = new List<DropTableEntry>();

    public void Drop()
    {
        foreach (DropTableEntry entry in dropTable)
        {
            if (Random.Range(1,100) <= entry.dropchance)
            {
                Instantiate(entry.item, gameObject.transform.position, Quaternion.identity);
            }
        }
    }
}
