using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : KillablePawn {

    private int xP;
    private int currentLevel;
    private int xPForNextLevel;
    private int xPForLevelStart;
    private int MaxLevel = 60;

    void Awake()
    {
    }

    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        HitPoints = 3;
        MaxHitpoints = 3;
        GameManager.instance.SetPlayer(this);
        CalculateCurrentLevel();
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();	
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

    void GainXP(int oXP)
    {
        xP += oXP;
        //show xp popup
        CalculateCurrentLevel();
    }

    void CalculateCurrentLevel()
    {
        GameManager.instance.eventManager.playerXPChanged.Invoke(this);
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
}
