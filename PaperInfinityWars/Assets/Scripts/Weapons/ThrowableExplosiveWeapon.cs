using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableExplosiveWeapon : Weapon {

    public float fuseTime = 3f;
    private float cooldownTime = 30f;
    private float cooldownTimer = 0f;
    public float throwstrength = 500f;

    bool cooldown = false;
    public GameObject throwable;

	// Use this for initialization
	void Start () {

	}

    protected override void Update()
    {
        base.Update();
        if (cooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
                cooldown = false;
        }
    }

    public override void Attack()
    {
        if (!cooldown)
        {
            cooldownTimer = cooldownTime;
            //create grenade
            GameObject nade = Instantiate(throwable, this.transform.position, Quaternion.identity);
            nade.GetComponent<GrenadeScript>().Initialize(this, this.damage, this.range, this.fuseTime);

            Vector3 playerpos = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);
            Vector2 throwdirection = Input.mousePosition - playerpos;
            nade.GetComponent<Rigidbody2D>().AddForce(throwdirection.normalized * throwstrength); //replace with mouse position based force (so you can aim the nade with your mouse)
            cooldown = true;
        }
        //else 
        //{
        //grenade not ready yet
        //}
    }
}
