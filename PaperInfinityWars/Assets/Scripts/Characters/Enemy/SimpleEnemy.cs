﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class SimpleEnemy : KillablePawn {
    GameObject player = null;
    [Range(0,1)]
    public float movementspeed = 0.5f;
    private float jumptimer = 0f;
    private float timebetweenjumps = 0.3f;
    int xpforkill = 10;
    WeaponSlots weaponSlots = null;

	// Use this for initialization
	protected override void Start () {
        base.Start();
	}

    // Update is called once per frame
    protected override void Update()
    {
        bool shouldjump = false;
        base.Update();
        if (player == null)
        {
            player = GameManager.instance.player.gameObject;
            if (player == null)
            {
                return;
            }
        }
        //cheapest ai ever
        float movement = 0;

        if (Vector2.Distance(player.transform.position, this.transform.position) < 10)
        {
            if (player.transform.position.x < this.transform.position.x)
                movement = -movementspeed;
            else
                movement = movementspeed;
        }
        if (characterController._forwardFree != true)
        {
            if (jumptimer <= 0)
            {
                shouldjump = true;
                jumptimer = timebetweenjumps;
            }
        }
        jumptimer -= Time.deltaTime;

        characterController.Move(movement, shouldjump);

        
    }

    protected override void OnDeath(KillablePawn victim, KillablePawn killer, Weapon weapon)
    {
        Debug.Log(this);
        Debug.Log(killer);
        Debug.Log(weapon);
        GameManager.instance.eventManager.EnemyDeath.Invoke(this, killer, weapon);
        Debug.Log("enemy died");

        GameManager.instance.eventManager.XPDrop.Invoke(xpforkill, this);
        Destroy(gameObject, 15f);
    }
}
