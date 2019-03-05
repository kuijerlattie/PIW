using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(KeyBindingManager))]
public class KeyBindingInspector : Editor
{
    private GUIStyle bold = new GUIStyle();
    private bool create = false;
    private KeyEditInfo keyEditInfo;
    private string actionName = "";
    private bool showDebugOutput = false;

    void OnEnable()
    {
        EditorApplication.modifierKeysChanged += this.Repaint;
    }

    void OnDisable()
    {
        EditorApplication.modifierKeysChanged -= this.Repaint;
    }

    public override void OnInspectorGUI()
    {
        bold = new GUIStyle(GUI.skin.label);
        bold.fontStyle = FontStyle.Bold;

        if (keyEditInfo.editing == false)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(5);
            GUILayout.Label("Debug Output", bold);
            GUILayout.FlexibleSpace();

            showDebugOutput = GUILayout.Toggle(showDebugOutput, "");

            if (showDebugOutput)
            {
                GameObject.Find("MainMenu").GetComponent<KeyBindingManager>().debugOutput = true;
            }
            else
            {
                GameObject.Find("MainMenu").GetComponent<KeyBindingManager>().debugOutput = false;
            }

            GUILayout.Space(185);
            GUILayout.EndHorizontal();

            if (KeyBindingManager.defaultKeys.Count > 0)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(5);
                GUILayout.Label("Action Name", bold);
                GUILayout.FlexibleSpace();
                GUILayout.Label("Primary", bold, GUILayout.Width(105));
                GUILayout.Space(185);
                GUILayout.EndHorizontal();
            }


            foreach (KeyValuePair<string, InputCode> pair in KeyBindingManager.defaultKeys)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(pair.Key);
                GUILayout.FlexibleSpace();

                if (GUILayout.Button(pair.Value.ToString(), GUILayout.Width(115)))
                {
                    keyEditInfo = new KeyEditInfo(pair.Key);
                    keyEditInfo.editing = true;
                }

                GUILayout.Space(120);

                if (GUILayout.Button("Delete"))
                {
                    deleteDictionaryEntry(pair);
                    break;
                }

                GUILayout.EndHorizontal();

            }
            if (!create)
            {
                if (GUILayout.Button("New"))
                {
                    create = true;
                }
            }
            else
            {
                GUILayout.BeginHorizontal();
                actionName = GUILayout.TextField(actionName, GUILayout.Width(150));
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Create", GUILayout.Width(137)))
                {
                    createDictionaryEntry();
                }
                if (GUILayout.Button("Cancel", GUILayout.Width(76)))
                {
                    create = false;
                }
                GUILayout.EndHorizontal();
            }
        }

        else
        {
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Press any key", bold);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
            if (GUILayout.Button("Reset"))
            {
                resetInputCode();

                if (keyEditInfo.editing)
                {
                    keyEditInfo.editing = pollForInput();
                }
            }
        }


        if (keyEditInfo.editing)
        {
            keyEditInfo.editing = pollForInput();
        }
    }

    private void resetInputCode()
    {
        InputCode key = KeyBindingManager.defaultKeys[keyEditInfo.actionName];
        
        key = InputCode.None;

        EditorUtility.SetDirty(target);
        keyEditInfo.editing = false;
    }

    private bool pollForInput()
    {
        InputCode poll = checkInput();
        if (poll != InputCode.None)
        {
            KeyBindingManager.BindKeyToAction(keyEditInfo.actionName, poll);
            EditorUtility.SetDirty(target);
            return false;
        }
        return true;
    }

    private void createDictionaryEntry()
    {
        actionName = actionName.Replace(" ", string.Empty);
        KeyBindingManager.CreateEntry(actionName);
        actionName = "";
        create = false;
    }

    private void deleteDictionaryEntry(KeyValuePair<string, InputCode> pair)
    {
        KeyBindingManager.RemoveEntry(pair.Key);
        EditorUtility.SetDirty(target);
    }

    private InputCode checkInput()
    {
        if (Event.current.shift)
        {
            return InputCode.LeftShift;
        }

        else if (Event.current.isKey)
        {
            return (InputCode)((int)Event.current.keyCode);
        }
        else if (Event.current.type == EventType.ScrollWheel)
        {
            if (Event.current.delta.y > 0)
            {
                return InputCode.MouseScrollDown;
            }
            else
            {
                return InputCode.MouseScrollUp;
            }
        }

        else if (Event.current.isMouse)
        {
            return (InputCode)323 + Event.current.button;
        }

        

        return InputCode.None;
    }
}


