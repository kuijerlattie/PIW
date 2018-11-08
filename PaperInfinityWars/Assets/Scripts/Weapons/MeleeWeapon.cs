using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	protected override void Update ()
    {         
        base.Update();
    }

    public override void Attack()
    {
        if (attackCooldown <= 0)
        {
            isAttacking = true;
            if (_animator != null)
                _animator.SetBool("Attack", true);
            attackCooldown = attackspeed;
        }
        else if (isAttacking)
        {
            //attack chains here i guess;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttacking)
        {
            if (collision.transform.root != this.transform.root)
            {
                KillablePawn enemyhitpoints = collision.GetComponent<KillablePawn>();
                if (enemyhitpoints != null)
                {
                    Debug.Log("enemy hitpoints arent null");
                    enemyhitpoints.Damage(damage, this);
                }
            }
        }
    }
}
