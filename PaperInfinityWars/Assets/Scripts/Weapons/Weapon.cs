using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    [SerializeField]
    protected int damage = 0;
    [SerializeField]
    protected float attackspeed = 1f;
    public float range = 2f;
    protected float attackCooldown = 0f;
    public bool isAttacking = false;
    public WeaponSlots owner;
    protected Animator _animator;

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

    public void AnimationOver()
    {
        isAttacking = false;
    }

    public virtual void Attack() { }
}
