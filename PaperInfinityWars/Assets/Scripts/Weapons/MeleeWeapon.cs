using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.root != this.transform.root)
        {
            KillablePawn enemyhitpoints = collision.GetComponent<KillablePawn>();
            if (enemyhitpoints != null)
            {
                Debug.Log("enemy hitpoints arent null");
                enemyhitpoints.Damage(damage, this);
            }
        }
    }
}
