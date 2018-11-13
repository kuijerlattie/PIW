﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    public static GameManager instance;
    public bool DebugMenu = false;
    public GameObject UI;
    protected GameObject UIinstance;

    [HideInInspector]
    public Player player;
    [HideInInspector]
    public SavegameManager savegameManager;
    [HideInInspector]
    public EventManager eventManager;
    [HideInInspector]
    public EnemySpawnManager enemySpawnManager;
    [HideInInspector]
    public GameMode gameMode;
    [HideInInspector]
    public CameraFollowScript cameraFollowScript;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(this.gameObject);

        //add managers
        savegameManager = gameObject.AddComponent<SavegameManager>();
        eventManager = gameObject.AddComponent<EventManager>();
        enemySpawnManager = gameObject.AddComponent<EnemySpawnManager>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (UIinstance == null)
        {
            UIinstance = GameObject.Instantiate(UI);
        }
    }

    public void SetGameMode<T>() where T : GameMode
    {
        Destroy(GetComponent<GameMode>());
        gameMode = gameObject.AddComponent<T>();
    }

    public void SetCameraFollowScript(CameraFollowScript oCameraFollowScript)
    {
        cameraFollowScript = oCameraFollowScript;
        if (player != null)
        {
            cameraFollowScript.target = player.gameObject;
            cameraFollowScript.Initialize();
        }
    }

    public void SetPlayer(Player oPlayer)
    {
        player = oPlayer;
        if (cameraFollowScript != null)
        {
            cameraFollowScript.target = player.gameObject;
            cameraFollowScript.Initialize();
        }
    }
}
