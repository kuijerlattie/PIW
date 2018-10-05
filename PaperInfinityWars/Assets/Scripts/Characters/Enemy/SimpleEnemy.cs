using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class SimpleEnemy : Hitpoints {
    GameObject player = null;
    CharacterController2D characterController;
    [Range(0,1)]
    public float movementspeed = 0.5f;

	// Use this for initialization
	void Start () {
        characterController = GetComponent<CharacterController2D>();
        if (player == null)
        {
            Debug.Log("player wasnt there yet :(");
        }
	}

    // Update is called once per frame
    protected override void Update()
    {
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
        characterController.Move(movement, !characterController.m_ForwardFree);
    }

    protected override void OnDeath()
    {
        throw new System.NotImplementedException();
    }
}
