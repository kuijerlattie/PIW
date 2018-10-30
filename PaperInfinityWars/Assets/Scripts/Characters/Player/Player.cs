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
        GameManager.instance.SetPlayer(this);
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();	
	}

    protected override void OnDeath(KillablePawn victim, KillablePawn killer, Weapon weapon)
    {
        GameManager.instance.eventManager.PlayerDeath.Invoke(this, killer, weapon);
    }
}
