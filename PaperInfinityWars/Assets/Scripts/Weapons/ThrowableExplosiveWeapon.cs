using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableExplosiveWeapon : Weapon {

    public float fuseTime = 3f;
    private float cooldownTime = 30f;
    private float cooldownTimer = 0f;
    public float throwstrength = 500f;

    private float ChargeRequired = 30f; //in seconds

    bool cooldown = false;
    public GameObject throwable;

	// Use this for initialization
	void Start () {

	}

    protected override void Update()
    {
        if (currentUses < maxStackedUses)
        {
            //charge next use
            _chargeprogress += Time.deltaTime;
            if (_chargeprogress > ChargeRequired)
            {
                currentUses++;
                _chargeprogress = 0f;
            }
            chargeprogress = _chargeprogress / ChargeRequired;

            GameManager.instance.eventManager.EquipmentChanged.Invoke(this);
        }
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
        if (currentUses > 0)
        {
            //create grenade
            GameObject nade = Instantiate(throwable, this.transform.position, Quaternion.identity);
            nade.GetComponent<GrenadeScript>().Initialize(this, this.damage, this.range, this.fuseTime);

            Vector3 playerpos = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);
            Vector2 throwdirection = Input.mousePosition - playerpos;
            nade.GetComponent<Rigidbody2D>().AddForce(throwdirection.normalized * throwstrength); //replace with mouse position based force (so you can aim the nade with your mouse)
            currentUses--;
        }
        //else 
        //{
        //grenade not ready yet
        //}
    }
}
