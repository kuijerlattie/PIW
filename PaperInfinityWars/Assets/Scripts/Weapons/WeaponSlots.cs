using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlots : MonoBehaviour {

    [Header("Weapon settings")]
    public int weaponSlots = 4;
    int selectedweapon = 1;
    Weapon[] weaponlist;
    protected Animator _animator;
    public GameObject testweapon;

    public Weapon currentweapon
    {
        get { return weaponlist[selectedweapon - 1]; }
    }

    public GameObject hand;

    void Start()
    {
        weaponlist = new Weapon[weaponSlots];
        _animator = GetComponentInChildren<Animator>();
        selectedweapon = 1;
        EquipWeapon(testweapon);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //q scroll weapon back
            NextWeapon();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //e to scroll weapon forward
            PreviousWeapon();
        }
    }

    protected void NextWeapon()
    {
        if (!currentweapon.isAttacking)
        {
            selectedweapon += 1;
            if (selectedweapon > weaponSlots)
                selectedweapon = 1;
            ShowActiveWeapon();
        }
    }

    protected void PreviousWeapon()
    {
        if (!currentweapon.isAttacking)
        {
            selectedweapon -= 1;
            if (selectedweapon <= 0)
                selectedweapon = weaponSlots;
            ShowActiveWeapon();
        }
    }

    protected void EquipWeapon(GameObject oWeapon)
    {
        //instantize weapon to hand
        GameObject weapontoinstantiate = Instantiate(oWeapon, hand.transform);
        weaponlist[selectedweapon-1] = weapontoinstantiate.GetComponent<Weapon>();
        weaponlist[selectedweapon - 1].owner = this;
        weapontoinstantiate.GetComponent<Weapon>().setanimator(_animator);
        ShowActiveWeapon();
    }

    protected void EquipWeapon(int iSlot, GameObject oWeapon)
    {
        selectedweapon = iSlot - 1;
        EquipWeapon(oWeapon);
    }

    protected void RemoveWeapon()
    {
        weaponlist[selectedweapon] = null;
    }

    protected void RemoveWeapon(int iSlot)
    {
        weaponlist[iSlot] = null;
    }

    protected void ShowActiveWeapon()
    {
        for (int i = 0; i < weaponSlots; i++)
        {
            if (weaponlist[i] != null)
            {
                weaponlist[i].gameObject.SetActive(i == selectedweapon -1 ? true : false);
            }
        }
    }
}
