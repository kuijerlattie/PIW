using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public Settings settings = new Settings();
    private string _SettingsString = "Settings.ini";
    public bool SettingsExists = false;

    void Start()
    {
        Debug.Log("Loading Settings!");
        Load();
        GameManager.instance.eventManager.OnSettingsChange.AddListener(SettingChangedListener);
    }

    private void SettingChangedListener()
    {
        Save();
    }

    private void Save()
    {
        GameManager.instance.eventManager.OnSettingsSave.Invoke();
        using (StreamWriter sw = new StreamWriter(Path.Combine(Application.persistentDataPath, _SettingsString)))
        {
            sw.WriteLine("[KeyBindings]");
            foreach(KeyValuePair<string, InputCode> valuePair in settings.keyBindings)
            {
                sw.WriteLine(valuePair.Key + "=" + (int)valuePair.Value);
            }
        }
    }

    private void Load()
    {
        LoadData();
        GameManager.instance.eventManager.OnSettingsLoad.Invoke(settings);
    }

    private void LoadData()
    {
        String line = "";
        int offset = 0;
        try
        {
            using (StreamReader sr = new StreamReader(Path.Combine(Application.persistentDataPath, _SettingsString)))
            {
                line = sr.ReadLine();
                if (line == "[KeyBindings]")
                {
                    line = sr.ReadLine();
                    while (!line.StartsWith("[") && line != String.Empty)
                    {
                        offset = line.IndexOf("=");
                        if (offset > 0)
                            settings.keyBindings.Add(line.Substring(0, offset), (InputCode)int.Parse(line.Substring(offset + 1)));
                        line = sr.ReadLine();
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
}

[Serializable]
public class Settings
{
    public Dictionary<string, InputCode> keyBindings = new Dictionary<string, InputCode>();
}