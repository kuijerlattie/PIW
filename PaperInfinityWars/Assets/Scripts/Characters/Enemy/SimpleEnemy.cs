using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class SimpleEnemy : WeaponWielder {
    GameObject player = null;
    [Range(0,1)]
    public float movementspeed = 0.5f;
    private float jumptimer = 0f;
    private float timebetweenjumps = 0.3f;

	// Use this for initialization
	protected override void Start () {
        base.Start();
        if (player == null)
        {
                
        }
	}

    // Update is called once per frame
    protected override void Update()
    {
        bool shouldjump = false;
        base.Update();
        if (player == null)
            player = GameManager.instance.player.gameObject;
        //cheapest ai ever
        float movement = 0;

        if (Vector2.Distance(player.transform.position, this.transform.position) < 10)
        {
            if (player.transform.position.x < this.transform.position.x)
                movement = -movementspeed;
            else
                movement = movementspeed;
        }
        if (characterController.m_ForwardFree != true)
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

    protected override void OnDeath()
    {
        Debug.Log("enemy died");
    }
}
