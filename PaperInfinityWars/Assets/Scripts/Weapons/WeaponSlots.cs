using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlots : MonoBehaviour {

    [Header("Weapon settings")]
    public int weaponSlots = 4;
    [SerializeField] int selectedweapon = 1;
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

    public void NextWeapon()
    {
        if (currentweapon == null || (currentweapon != null && !currentweapon.isAttacking))
        {
            selectedweapon += 1;
            if (selectedweapon > weaponSlots)
                selectedweapon = 1;
            ShowActiveWeapon();
        }
    }

    public void PreviousWeapon()
    {
        if (currentweapon == null || (currentweapon != null && !currentweapon.isAttacking))
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
        RemoveWeapon();
        GameObject weapontoinstantiate = Instantiate(oWeapon, hand.transform);
        weaponlist[selectedweapon-1] = weapontoinstantiate.GetComponent<Weapon>();
        weaponlist[selectedweapon - 1].owner = this;
        weapontoinstantiate.GetComponent<Weapon>().SetAnimator(_animator);
        ShowActiveWeapon();
    }

    protected void EquipWeapon(int iSlot, GameObject oWeapon)
    {
        selectedweapon = iSlot - 1;
        EquipWeapon(oWeapon);
    }

    protected void RemoveWeapon()
    {
        RemoveWeapon(selectedweapon);
    }

    protected void RemoveWeapon(int iSlot)
    {
        if (weaponlist[iSlot -1] != null)
        {
            Destroy(weaponlist[iSlot - 1].gameObject);
            weaponlist[iSlot - 1] = null;
        }
    }

    protected void ShowActiveWeapon()
    {
        for (int i = 0; i < weaponSlots; i++)
        {
            if (weaponlist[i] != null)
            {
                weaponlist[i].gameObject.SetActive(i == selectedweapon -1 ? true : false);
                weaponlist[i].OnEquip();
            }
        }
    }

    public void Attack()
    {
        if (currentweapon != null)
        {
            currentweapon.Attack();
        }
    }

    public float GetRange()
    {
        if (currentweapon != null)
        {
            return currentweapon.range;
        }
        else
        {
            return float.PositiveInfinity;
        }
    }

    public void SaveEquipedWeapons()
    {
        GameManager.instance.savegameManager.saveData.weaponslot1 = weaponlist[0] == null ? 0 : weaponlist[0].ID;
        GameManager.instance.savegameManager.saveData.weaponslot2 = weaponlist[1] == null ? 0 : weaponlist[1].ID;
        GameManager.instance.savegameManager.saveData.weaponslot3 = weaponlist[2] == null ? 0 : weaponlist[2].ID;
        GameManager.instance.savegameManager.saveData.weaponslot4 = weaponlist[3] == null ? 0 : weaponlist[3].ID;
    }

    public void LoadEquipedWeapons(SaveData saveData)
    {
        if (saveData.weaponslot1 != 0)
        {
            EquipWeapon(1, GameManager.instance.itemManager.GetWeaponByID(saveData.weaponslot1));
        }
        if (saveData.weaponslot1 != 0)
        {
            EquipWeapon(2, GameManager.instance.itemManager.GetWeaponByID(saveData.weaponslot1));
        }
        if (saveData.weaponslot1 != 0)
        {
            EquipWeapon(3, GameManager.instance.itemManager.GetWeaponByID(saveData.weaponslot1));
        }
        if (saveData.weaponslot1 != 0)
        {
            EquipWeapon(4, GameManager.instance.itemManager.GetWeaponByID(saveData.weaponslot1));
        }
    }
}
