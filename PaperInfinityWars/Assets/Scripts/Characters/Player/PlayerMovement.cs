using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;

    float horizontalMove = 0f;
    bool jump = false;

    void OnEnable()
    {
        Debug.Log("registered for load");
        GameManager.instance.eventManager.OnSave.AddListener(SetSaveData);
        GameManager.instance.eventManager.OnLoad.AddListener(SetLoadData);
    }

    void OnDisable()
    {
        GameManager.instance.eventManager.OnSave.RemoveListener(SetSaveData);
        GameManager.instance.eventManager.OnLoad.RemoveListener(SetLoadData);
    }

    // Update is called once per frame
    void Update () {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<WeaponSlots>().Attack();
        }
        if (Input.GetMouseButtonDown(1))
        {
            GetComponent<WeaponSlots>().UseEquipment();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //q scroll weapon back
            GetComponent<WeaponSlots>().NextWeapon();
        }
        if (Input.GetKeyDown(KeyCode.E))
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
