using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour {

    private int xP = 0;
    private int currentLevel = 0;
    private int xPForNextLevel = 100;
    private int xPForLevelStart = 0;
    private int maxLevel = 60;

    private int coins;


    // Use this for initialization
    void Start () {
        GameManager.instance.eventManager.XPDrop.AddListener(GainXP);
        GameManager.instance.eventManager.OnSave.AddListener(SetSaveData);
        GameManager.instance.eventManager.OnLoad.AddListener(SetLoadData);
    }
	
    void OnDisable()
    {
        GameManager.instance.eventManager.XPDrop.RemoveListener(GainXP);
        GameManager.instance.eventManager.OnSave.RemoveListener(SetSaveData);
        GameManager.instance.eventManager.OnLoad.RemoveListener(SetLoadData);
    }

	// Update is called once per frame
	void Update () {

    }

    void GainXP(int oXP, KillablePawn xpgiver)
    {
        xP += oXP;
        //show xp popup
        if (xP >= xPForNextLevel)
        {
            CalculateCurrentLevel();
        }
    }

    void CalculateCurrentLevel()
    {
        if (currentLevel < maxLevel)
        {
            int difference = 0;
            int temptotal = 0;
            int tempold = 0;
            for (int i = 1; i <= maxLevel+1; i++)
            {
                tempold = temptotal;
                difference = LevelFormula(i);
                temptotal += difference;
                if (temptotal > xP)
                {
                    xPForLevelStart = tempold;
                    xPForNextLevel = temptotal;
                    if (currentLevel < i)
                    {
                        currentLevel = i;
                        GameManager.instance.eventManager.LevelUp.Invoke(this);
                    }
                    break;
                }
            }
        }

    GameManager.instance.eventManager.PlayerCurrencyChanged.Invoke(this);

    }

    static int LevelFormula(int level)
    {

        int xp = 80;
        xp += 15 * (int)Mathf.Pow(level, 1.2f);

        return xp;
    }

    public int GetXP()
    {
        return xP;
    }

    public int GetXPForLevelStart()
    {
        return xPForLevelStart;
    }

    public int GetXPForNextLevel()
    {
        return xPForNextLevel;
    }

    public int GetLevel()
    {
        return currentLevel;
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        GameManager.instance.eventManager.PlayerCurrencyChanged.Invoke(this);
    }

    public int GetCoins()
    {
        return coins;
    }

    private void SetSaveData()
    {
        GameManager.instance.savegameManager.saveData.coins = coins;
        GameManager.instance.savegameManager.saveData.xp = xP;
    }

    private void SetLoadData(SaveData saveData)
    {
        coins = saveData.coins;
        xP = saveData.xp;
        CalculateCurrentLevel();
        GameManager.instance.eventManager.PlayerCurrencyChanged.Invoke(this);
    }
}
