using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    [HideInInspector]
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

    public enum WeaponType
    {
        Melee,
        Gun,
        Explosive
    }

    public WeaponType weaponType = WeaponType.Melee;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
	}

    public void setanimator(Animator oAnimator)
    {
        _animator = oAnimator;
    }

    public void OnEquip()
    {
        _animator.runtimeAnimatorController = animationOverrideController;
    }

    public void AnimationOver()
    {
        isAttacking = false;
        attackCooldown = attackCooldownTime;
    }

    public void AnimationStarted()
    {
        isAttacking = true;
    }

    public virtual void Attack() { }
}
