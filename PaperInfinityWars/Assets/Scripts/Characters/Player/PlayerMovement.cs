using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;

    float horizontalMove = 0f;
    bool jump = false;

    void OnEnable()
    {
        GameManager.instance.eventManager.OnSave.AddListener(SetSaveData);
    }

    void Start()
    {
        GetComponent<WeaponSlots>().LoadEquipedWeapons(GameManager.instance.savegameManager.saveData);
    }

    void OnDisable()
    {
        GameManager.instance.eventManager.OnSave.RemoveListener(SetSaveData);
    }

    // Update is called once per frame
    void Update () {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
        if (Input.GetMouseButtonDown(0) && controller.enabled)
        {
            GetComponent<WeaponSlots>().Attack();
        }
        if (Input.GetMouseButtonDown(1) && controller.enabled)
        {
            GetComponent<WeaponSlots>().UseEquipment();
        }
        if (Input.GetKeyDown(KeyCode.Q) && controller.enabled)
        {
            //q scroll weapon back
            GetComponent<WeaponSlots>().NextWeapon();
        }
        if (Input.GetKeyDown(KeyCode.E) && controller.enabled)
        {
            //e to scroll weapon forward
            GetComponent<WeaponSlots>().PreviousWeapon();
        }
    }

    void SetSaveData()
    {
        GetComponent<WeaponSlots>().SaveEquipedWeapons();
    }

    void SetLoadData(SaveData saveData)
    {
        GetComponent<WeaponSlots>().LoadEquipedWeapons(saveData);
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove, jump);
        jump = false;
    }
}
