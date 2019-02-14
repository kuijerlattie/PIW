using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon {

	// Use this for initialization
	void Start () {
        weaponType = WeaponType.Melee;
	}
	
	// Update is called once per frame
	protected override void Update ()
    {         
        base.Update();
    }

    public override void Attack()
    {
        if (attackCooldown <= 0 && !isAttacking)
        {
            isAttacking = true;
            if (_animator != null)
            {
                _animator.SetBool("Attack", true);
                _animator.SetInteger("AttackChain", 0);
            }
            attackCooldown = attackCooldownTime;

        }
        if (isAttacking)
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
                _animator.SetInteger("AttackChain", 1);

            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
                _animator.SetInteger("AttackChain", 2);
        }
    }

    //attack1, attack2, attack3. if not in time, cooldown -> attack1 again

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttacking)
        {
            if (collision.transform.root != this.transform.root)
            {
                KillablePawn enemyhitpoints = collision.GetComponent<KillablePawn>();
                if (enemyhitpoints != null)
                {
                    enemyhitpoints.Damage(damage, this);
                }
            }
        }
    }
}
