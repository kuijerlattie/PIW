using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    [SerializeField]
    protected int damage = 0;
    float attackspeed = 0f;
    WeaponSlots owner;
    Animator animator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
