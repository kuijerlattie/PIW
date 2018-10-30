using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour {

    private int xP;
    public int currentLevel;
    private int xPForNextLevel;
    private int MaxLevel = 60;

	// Use this for initialization
	void Start () {
        CalculateCurrentLevel();
	}
	
	// Update is called once per frame
	void Update () {
	}

    void GainXP(int oXP)
    {
        xP += oXP;
        //show xp popup?
        CalculateCurrentLevel();
    }

    void CalculateCurrentLevel()
    {
        
    }
}
