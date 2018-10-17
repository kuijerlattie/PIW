﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    public static GameManager instance;


    [HideInInspector]
    public Player player;
    [HideInInspector]
    public SavegameManager savegameManager;
    [HideInInspector]
    public EventManager eventManager;
    [HideInInspector]
    public EnemySpawnManager enemySpawnManager;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
