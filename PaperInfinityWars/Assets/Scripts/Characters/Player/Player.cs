using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : KillablePawn {

    private int xP = 0;
    private int currentLevel = 0;
    private int xPForNextLevel = 100;
    private int xPForLevelStart = 0;
    private int maxLevel = 60;

    private int coins;

    protected override void Start ()
    {
        base.Start();
        HitPoints = 3;
        MaxHitpoints = 3;
        GameManager.instance.SetPlayer(this);
        CalculateCurrentLevel();
        GameManager.instance.eventManager.XPDrop.AddListener(GainXP);
        GameManager.instance.eventManager.CoinsChanged.Invoke(coins);
    }

    void OnDisable()
    {
        GameManager.instance.eventManager.XPDrop.RemoveListener(GainXP);
    }
    
	protected override void Update () {
        base.Update();
        if (Input.GetKeyDown(KeyCode.I))
        {
            AddCoins(1);
            Debug.Log("coin added, total now: " + coins);
        }
    }

    protected override void OnDeath(KillablePawn victim, KillablePawn killer, Weapon weapon)
    {
        GameManager.instance.eventManager.PlayerDeath.Invoke(this, killer, weapon);
    }

    protected override void OnDamageTaken()
    {
        GameManager.instance.eventManager.PlayerHitpointsChanged.Invoke(this);
    }

    protected override void OnHitpointsChanged()
    {
        GameManager.instance.eventManager.PlayerHitpointsChanged.Invoke(this);
    }

    protected override void OnMaxHitpointsChanged()
    {
        GameManager.instance.eventManager.PlayerHitpointsChanged.Invoke(this);
    }

    void GainXP(int oXP, KillablePawn xpgiver)
    {
        xP += oXP;
        //show xp popup
        CalculateCurrentLevel();
    }

    void CalculateCurrentLevel()
    {
        //add level calculating code
        GameManager.instance.eventManager.PlayerXPChanged.Invoke(this);
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
        GameManager.instance.eventManager.CoinsChanged.Invoke(coins);
    }

    public void RemoveCoins(int amount)
    {
        coins -= amount;
        GameManager.instance.eventManager.CoinsChanged.Invoke(coins);
    }
}
