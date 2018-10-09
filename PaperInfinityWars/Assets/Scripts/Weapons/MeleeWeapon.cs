using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour {

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
            Hitpoints enemyhitpoints = collision.GetComponent<Hitpoints>();
            if (enemyhitpoints != null)
            {
                enemyhitpoints.Damage(1);
            }
        }
    }
}
