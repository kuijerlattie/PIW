using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Hitpoints {

    void Awake()
    {
    }

    // Use this for initialization
    void Start ()
    {
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
