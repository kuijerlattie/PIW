using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hitpoints : MonoBehaviour {

    public int hitpoints;
    public int maxHitpoints;
    public bool godmode;
    public bool invincibleAfterHit;
    protected bool invincible;
    public float invincibleTime;
    protected float invincibleTimer;
    public bool alive;

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
        
        characterController = GetComponent<CharacterController2D>();
	}
	
    void OnSpawn()
    {
        hitpoints = maxHitpoints;
        alive = true;
        UpdateCharacterControllerEnabled();
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

    public void Damage(int iDamage) //add damage dealer for stats
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
                Die(); //RIP
            }
        }
    }

    public void Heal(int iAmount)
    {
        hitpoints += iAmount;
        hitpoints = Mathf.Clamp(hitpoints, 0, maxHitpoints);
    }

    private void Die()
    {
        alive = false;
        OnDeath();
        UpdateCharacterControllerEnabled();
        //do ragdoll code;
    }
    protected abstract void OnDeath();

    private void UpdateCharacterControllerEnabled()
    {
        if (characterController != null)
        {
            characterController.enabled = alive;
        }
    }
}
