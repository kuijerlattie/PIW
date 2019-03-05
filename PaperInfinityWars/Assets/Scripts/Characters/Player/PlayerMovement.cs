using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;

    public float horizontalMove = 0f;
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
        if(KeyBindingManager.GetInput("MoveLeft"))
        {
            horizontalMove = -1;
        }
        if (KeyBindingManager.GetInput("MoveRight"))
        {
            horizontalMove = 1;
        }

        if(KeyBindingManager.GetInputUp("MoveLeft") && !KeyBindingManager.GetInputDown("MoveRight"))
        {
            horizontalMove = 0;
        }

        if (KeyBindingManager.GetInputUp("MoveRight") && !KeyBindingManager.GetInputDown("MoveLeft"))
        {
            horizontalMove = 0;
        }

        if (KeyBindingManager.GetInputDown("Jump"))
        {
            jump = true;
        }
        if (KeyBindingManager.GetInputDown("Attack") && controller.enabled)
        {
            GetComponent<WeaponSlots>().Attack();
        }
        if (KeyBindingManager.GetInputDown("Equipment") && controller.enabled)
        {
            GetComponent<WeaponSlots>().UseEquipment();
        }
        if (KeyBindingManager.GetInputDown("NextWeapon") && controller.enabled)
        {
            //q scroll weapon back
            GetComponent<WeaponSlots>().NextWeapon();
        }
        if (KeyBindingManager.GetInputDown("PreviousWeapon") && controller.enabled)
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
