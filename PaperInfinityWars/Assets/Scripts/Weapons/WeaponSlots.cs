using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlots : MonoBehaviour {

    [Header("Weapon settings")]
    public int weaponSlots = 4;
    [SerializeField] // remove later
    int selectedweapon = 1;
    [SerializeField] //remove later
    Weapon[] weaponlist;
    protected Animator animator;
    public GameObject testweapon;

    public GameObject hand;

    void Start()
    {
        weaponlist = new Weapon[weaponSlots];
        selectedweapon = 2;
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
        selectedweapon += 1;
        if (selectedweapon > weaponSlots)
            selectedweapon = 1;
        ShowActiveWeapon();
    }

    protected void PreviousWeapon()
    {
        selectedweapon -= 1;
        if (selectedweapon <= 0)
            selectedweapon = weaponSlots;
        ShowActiveWeapon();
    }

    protected void EquipWeapon(GameObject oWeapon)
    {
        //instantize weapon to hand
        GameObject weapontoinstantiate = Instantiate(oWeapon, hand.transform);
        weaponlist[selectedweapon-1] = weapontoinstantiate.GetComponent<Weapon>();
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
