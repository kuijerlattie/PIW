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

    public void SetAnimator(Animator oAnimator)
    {
        _animator = oAnimator;
    }

    public void OnEquip()
    {
        _animator.runtimeAnimatorController = animationOverrideController;
        FMODUnity.RuntimeManager.PlayOneShot(equipSoundPath, owner.transform.position);
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
