using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindingScript : MonoBehaviour {

    public string actionName = "";
    public InputCode key = InputCode.None;

    public Button keyButton;
    public Text keyLabel;

    public void setAction(string action)
    {
        actionName = action;
        key = KeyBindingManager.defaultKeys[action];

        keyButton.GetComponentInChildren<Text>().text = key.ToString();
        keyLabel.text = actionName;
    }
}
