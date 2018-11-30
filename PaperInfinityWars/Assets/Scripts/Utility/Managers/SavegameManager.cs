using System;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class SavegameManager : MonoBehaviour {

    public SaveData saveData = new SaveData();
    private string _SaveGameString = "SaveGame.sav";
    // Use this for initialization
    void Start () {
        Load();
        GameManager.instance.eventManager.GameOver.AddListener(GameOverListener);
	}

	void OnDisable()
    {
        GameManager.instance.eventManager.GameOver.RemoveListener(GameOverListener);
    }
    
    void GameOverListener(GameMode gameMode)
    {
        Save();
    }

    public void Save()
    {
        GameManager.instance.eventManager.OnSave.Invoke();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Path.Combine(Application.persistentDataPath, _SaveGameString));
        bf.Serialize(file, saveData);
        file.Close();
    }

    public void Load()
    {
        LoadData();
        GameManager.instance.eventManager.OnLoad.Invoke(saveData);
    }

    private void LoadData()
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, _SaveGameString)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Path.Combine(Application.persistentDataPath, _SaveGameString), FileMode.Open);
            saveData = (SaveData)bf.Deserialize(file);
            file.Close();
        }
    }
}

[Serializable]
public class SaveData
{
    public int xp = 0;
    public int coins = 0;

    //wavemode stats;
    public int wmBestRound = 0;
    public int wmTotalRounds = 0;
    public int wmTotalMatches = 0;
    public int wmBestKills = 0;
    public int wmTotalKills = 0;
    public float wmTotalGameTime = 0f;
    public float wmLongestMatch = 0f;
}

