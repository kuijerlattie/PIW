using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : KillablePawn {
    protected override void Start ()
    {
        base.Start();
        HitPoints = 3;
        MaxHitpoints = 3;
        GameManager.instance.SetPlayer(this);
    }
    
	protected override void Update () {
        base.Update();
    }

    protected override void OnDeath(KillablePawn victim, KillablePawn killer, Weapon weapon)
    {
        GameManager.instance.eventManager.PlayerDeath.Invoke(this, killer, weapon);
    }

    protected override void OnDamageTaken()
    {
        GameManager.instance.eventManager.PlayerHitpointsChanged.Invoke(this);
    }

    protected override void OnHitpointsChanged()
    {
        GameManager.instance.eventManager.PlayerHitpointsChanged.Invoke(this);
    }

    protected override void OnMaxHitpointsChanged()
    {
        GameManager.instance.eventManager.PlayerHitpointsChanged.Invoke(this);
    }
}
