using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour {

    float range = 1;
    int damage = 1;
    Weapon weapon;
    float timer = 0f;

    [FMODUnity.EventRef]
    public string explosionSound = "";

    public void Initialize(Weapon owner, int oDamage, float oRange, float fusetime)
    {
        weapon = owner;
        damage = oDamage;
        range = oRange;
        timer = fusetime;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Explode();
            //do explotion
            FMODUnity.RuntimeManager.PlayOneShot(explosionSound, transform.position);
            Destroy(gameObject);
        }
	}

    void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, range);
        foreach (Collider2D collider in hits)
        {
            if (collider.GetComponent<KillablePawn>() != null)
            {
                collider.GetComponent<KillablePawn>().Damage(damage, weapon);
            }
        }
    }
}
