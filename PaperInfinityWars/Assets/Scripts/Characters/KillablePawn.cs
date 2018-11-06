using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public abstract class KillablePawn : MonoBehaviour {

    public int hitpoints;
    public int maxHitpoints;
    public bool godmode;
    public bool invincibleAfterHit;
    protected bool invincible;
    public float invincibleTime;
    protected float invincibleTimer;
    public bool alive;
    protected Animator animator;

    protected CharacterController2D characterController;

    // Use this for initialization
    protected virtual void Start () {
        hitpoints = 3;
        maxHitpoints = 3;
        godmode = false;
        invincibleAfterHit = true;
        invincible = false;
        invincibleTime = 0.5f;
        invincibleTimer = 0;
        alive = true;

        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController2D>();
	}
	
    void OnSpawn()
    {
        hitpoints = maxHitpoints;
        alive = true;
        UpdateCharacterControllerEnabled();
        SetKinematic(true);
    }

	// Update is called once per frame
	protected virtual void Update () {
		if (invincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0)
            {
                invincible = false;
            }
        }
	}

    public void Damage(int iDamage, Weapon weapon) //add damage dealer for stats
    {
        if (!invincible && !godmode) //dont deal damage when invincible or in godmode;
        {
            hitpoints -= iDamage;
            if (invincibleAfterHit) //if you are invincible after being hit, start invincible timer and set invincible to true.
            {
                invincibleTimer = invincibleTime;
                invincible = true;
            }

            if (hitpoints <= 0)
            {
                if (weapon != null) //null if using debug for now
                {
                    Debug.Log(weapon.owner);
                    Die(weapon.owner.GetComponent<KillablePawn>(), weapon); //RIP
                }
            }
        }
    }

    public void Heal(int iAmount)
    {
        hitpoints += iAmount;
        hitpoints = Mathf.Clamp(hitpoints, 0, maxHitpoints);
    }

    private void Die(KillablePawn killer, Weapon weapon)
    {
        alive = false;
        characterController.enabled = false;
        OnDeath(this, killer, weapon);
        UpdateCharacterControllerEnabled();
        EnableRagdoll();
    }
    protected abstract void OnDeath(KillablePawn victim, KillablePawn killer, Weapon weapon);

    private void UpdateCharacterControllerEnabled()
    {
        if (characterController != null)
        {
            characterController.enabled = alive;
        }
    }

    private void EnableRagdoll()
    {
        SetKinematic(false);
        //GetComponent<BoxCollider2D>().enabled = false;
        //GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        animator.enabled = false;
    }

    protected void SetKinematic(bool value)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = value;
            rb.useGravity = !value;
        }
    }
}
