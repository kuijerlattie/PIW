using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : KillablePawn {

    void Awake()
    {
    }

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        GameManager.instance.player = this;
        animator = GetComponent<Animator>();
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
