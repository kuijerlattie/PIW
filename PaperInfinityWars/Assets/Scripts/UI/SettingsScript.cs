using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{

    public Button ControlsButton;

    public GameObject ControlPrefab;

    public RectTransform settingsOverviewCanvas;
    public ScrollRect settingsScrollView;

    private bool isEditingKey = false;
    private string currentAction = "";

    [Serializable]
    public enum SettingCategory
    {
        Controls
    };

    void OnEnable()
    {
        ControlsButton.enabled = true;
    }

    public void setCategory(int category)
    {
        SettingCategory settingsCategory = (SettingCategory)category;

        foreach (RectTransform r in settingsOverviewCanvas.transform.GetComponentInChildren<RectTransform>())
        {
            Destroy(r.gameObject);
        }

        if (settingsCategory == SettingCategory.Controls)
        {
            RenderControlSettings();
        }
    }

    public void RenderControlSettings()
    {
        Dictionary<string, InputCode> keys = KeyBindingManager.inputKeys;
        if (keys.Count != KeyBindingManager.defaultKeys.Count)
            keys = KeyBindingManager.defaultKeys;

        foreach(KeyValuePair<string, InputCode> pair in keys)
        {
            GameObject instance = Instantiate(ControlPrefab, settingsOverviewCanvas);

            instance.GetComponent<KeyBindingScript>().setAction(pair.Key);
            instance.GetComponent<KeyBindingScript>().keyButton.onClick.RemoveAllListeners();
            instance.GetComponent<KeyBindingScript>().keyButton.onClick.AddListener(delegate { SetActionKey(pair.Key); });
        }
    }

    public void SetActionKey(string actionName)
    {
        isEditingKey = true;
        currentAction = actionName;
    }

    void FixedUpdate()
    {
        if (isEditingKey)
        {
            settingsScrollView.vertical = false;

            if (Input.GetKey((KeyCode)InputCode.Escape))
            {
                isEditingKey = false;
                currentAction = "";
                settingsScrollView.vertical = true;
                return;
            }

            else if (Input.anyKey)
            {
                foreach (InputCode availableKey in Enum.GetValues(typeof(InputCode)))
                {
                    if (Input.GetKey((KeyCode)availableKey))
                    {
                        KeyBindingManager.BindKeyToAction(currentAction, availableKey);
                        isEditingKey = false;
                        currentAction = "";
                        setCategory((int)SettingCategory.Controls);

                        settingsScrollView.vertical = true;

                        GameManager.instance.eventManager.OnSettingsChange.Invoke();

                        return;
                    }
                }
            }

            else if (Input.mouseScrollDelta.y != 0)
            {
                InputCode inputCode = InputCode.None;
                if (Input.mouseScrollDelta.y < 0)
                {
                    inputCode = InputCode.MouseScrollDown;
                }
                else
                {
                    inputCode = InputCode.MouseScrollUp;
                }

                KeyBindingManager.BindKeyToAction(currentAction, inputCode);
                isEditingKey = false;
                currentAction = "";
                setCategory((int)SettingCategory.Controls);

                settingsScrollView.vertical = true;

                GameManager.instance.eventManager.OnSettingsChange.Invoke();

                return;
            }
        }
        
    }
}
