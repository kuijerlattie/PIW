using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : WeaponWielder {

    void Awake()
    {
    }

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        GameManager.instance.player = this;
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();	
	}

    protected override void OnDeath()
    {
        //trigger playerdeathevent
    }
}
