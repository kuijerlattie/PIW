﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour {

    public int ID = 0;
    [SerializeField]
    protected int damage = 0;
    [SerializeField]
    protected float attackCooldownTime = 1f;
    public float range = 2f;
    protected float attackCooldown = 0f;
    [HideInInspector]
    public bool isAttacking = false;
    [HideInInspector]
    public WeaponSlots owner;
    protected Animator _animator;
    public AnimatorOverrideController animationOverrideController;

    public Image equipImage;
    public int storeCost;
    public string storeName;
    public string StoreDescription;


    [FMODUnity.EventRef]
    public string attackSoundPath = "";
    [FMODUnity.EventRef]
    public string hitSoundPath = "";
    [FMODUnity.EventRef]
    public string equipSoundPath = "";

    public enum WeaponType
    {
        Melee,
        Gun,
        Explosive
    }

    public WeaponType weaponType = WeaponType.Melee;
	
	// Update is called once per frame
	protected virtual void Update () {
		if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
	}

    public void SetAnimator(Animator oAnimator)
    {
        _animator = oAnimator;
    }

    public void OnEquip()
    {
        _animator.runtimeAnimatorController = animationOverrideController;
        FMODUnity.RuntimeManager.PlayOneShot(equipSoundPath, owner.transform.position);
        GameManager.instance.eventManager.WeaponChanged.Invoke(this);
    }

    public void AnimationOver()
    {
        isAttacking = false;
        attackCooldown = attackCooldownTime;
    }

    public void AnimationStarted()
    {
        isAttacking = true;
        FMODUnity.RuntimeManager.PlayOneShot(attackSoundPath, owner.transform.position);
    }

    public virtual void Attack() { }
}
