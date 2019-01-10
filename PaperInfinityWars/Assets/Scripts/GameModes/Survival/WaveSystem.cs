﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveSystem : GameMode {

    public enum WaveSystemState
    {
        Warmup, //getting ready to start the game
        Spawning, //spawning enemies
        Waiting, //waiting for the last enemies to be killed
        Timeout, //time between rounds for player to prepare
        GameOver //Player died, game over.
    }

    #region General variables
    int round;
    List<KillablePawn> enemies;
    List<GameObject> spawnPool;
    public WaveSystemState gamestate;
    public float countdown = 0f;
    int normalsToSpawn;
    int raresToSpawn;
    int BossesToSpawn;
    EnemySpawnManager enemySpawnManager;
    bool LoadingHub = false;
    int countdownevent = 5;
    #endregion

    #region game stats
    float totalGameTimer = 0f; //time survived
    int killcounter = 0;
    #endregion

    #region Settings
    float warmupTime = 5f; //time before game starts in seconds
    float timeoutTime = 10f; //time between rounds in seconds
    float gameOverTime = 5f; //time after dying before returning to hubworld
    int maxSimultaniousEnemies = 30;
    float MaxSpawnRange = 60;
    #endregion

    #region EnemySettings
    public List<GameObject> commonEnemies = new List<GameObject>();
    public List<GameObject> RareEnemies = new List<GameObject>();
    public List<GameObject> BossEnemies = new List<GameObject>();
    #endregion

    // Use this for initialization
    void Start () {
        round = 0;
        enemies = new List<KillablePawn>();

        //these two lines to make sure that no on...ended is called yet. use changestate in other situations.
        gamestate = WaveSystemState.Warmup;
        OnWarmupStarted();
        GameManager.instance.eventManager.PlayerDeath.AddListener(OnPlayerDeath);
        GameManager.instance.eventManager.EnemyDeath.AddListener(OnEnemyDeath);
        GameManager.instance.eventManager.EndGame.AddListener(OnEndGame);
        enemySpawnManager = GameManager.instance.enemySpawnManager;
	}
	
    void ChangeState(WaveSystemState newState)
    {
        switch (gamestate)
        {
            case WaveSystemState.Warmup:
                OnWarmupEnded();
                break;
            case WaveSystemState.Spawning:
                OnSpawningEnded();
                break;
            case WaveSystemState.Waiting:
                OnWaitingEnded();
                break;
            case WaveSystemState.Timeout:
                OnTimeoutEnded();
                break;
            case WaveSystemState.GameOver:
                OnGameOverEnded();
                break;
            default:
                break;
        }

        switch (newState)
        {
            case WaveSystemState.Warmup:
                OnWarmupStarted();
                break;
            case WaveSystemState.Spawning:
                OnSpawningStarted();
                break;
            case WaveSystemState.Waiting:
                OnWaitingStarted();
                break;
            case WaveSystemState.Timeout:
                OnTimeoutStarted();
                break;
            case WaveSystemState.GameOver:
                OnGameOverStarted();
                break;
            default:
                break;
        }

        gamestate = newState;
    }

	// Update is called once per frame
	void Update () {
        switch (gamestate)
        {
            case WaveSystemState.Warmup:
                OnWarmup();
                break;
            case WaveSystemState.Spawning:
                OnSpawning();
                break;
            case WaveSystemState.Waiting:
                OnWaiting();
                break;
            case WaveSystemState.Timeout:
                OnTimeout();
                break;
            case WaveSystemState.GameOver:
                OnGameOver();
                break;
            default:
                break;
        }

        if (gamestate != WaveSystemState.GameOver)
            totalGameTimer += Time.deltaTime;
        countdown -= Time.deltaTime;
        if (countdown < countdownevent && countdownevent > 0)
        {
            GameManager.instance.eventManager.WMOnRoundCountdown.Invoke(countdownevent, gamestate);
            countdownevent--;
        }
    }

    void OnPlayerDeath(KillablePawn victim, KillablePawn killer, Weapon killerweapon)
    {
        ChangeState(WaveSystemState.GameOver);
    }

    void OnEnemyDeath(KillablePawn victim, KillablePawn killer, Weapon killerweapon)
    {
        enemies.RemoveAll(X => !X.alive);
        killcounter++;
    }

    void OnEndGame()
    {
        ChangeState(WaveSystemState.GameOver);
        GameManager.instance.eventManager.PlayerDeath.RemoveListener(OnPlayerDeath);
        GameManager.instance.player.GetComponent<CharacterController2D>().enabled = false;
    }

    #region Warmup
    void OnWarmupStarted()
    {
        countdown = warmupTime;
        if (commonEnemies.Count < 0)
            Debug.Log("Common enemies missing");
        if (RareEnemies.Count < 0)
            Debug.Log("Rare enemies missing");
        if (BossEnemies.Count < 0)
            Debug.Log("Boss enemies missing");
    }

    void OnWarmup()
    {
        if (countdown <= 0)
        {
            ChangeState(WaveSystemState.Spawning);
        }
    }

    void OnWarmupEnded()
    {
        totalGameTimer = 0f;
    }
    #endregion

    #region Spawning
    void OnSpawningStarted()
    {
        round++;
        CalculateWave();
        GameManager.instance.eventManager.WMOnRoundStart.Invoke(round);
    }

    void OnSpawning()
    {
        if (enemies.Count < maxSimultaniousEnemies)
        {
            if (spawnPool.Count > 0)
            {
                int randomspawnid = Random.Range(0, spawnPool.Count - 1);
                
                GameObject enemy = Instantiate(spawnPool[randomspawnid], enemySpawnManager.GetRandomSpawnPointInPlayerRange(MaxSpawnRange).transform.position, Quaternion.identity);
                KillablePawn commonKillablePawn = enemy.GetComponent<KillablePawn>();
                enemies.Add(commonKillablePawn);
                spawnPool.RemoveAt(randomspawnid);
            }

            if (spawnPool.Count == 0)
            {
                ChangeState(WaveSystemState.Waiting);
            }
        }
    }

    void OnSpawningEnded()
    {

    }
    #endregion

    #region Waiting
    void OnWaitingStarted()
    {

    }

    void OnWaiting()
    {
        if (enemies.Count == 0)
        {
            ChangeState(WaveSystemState.Timeout);
        }
    }

    void OnWaitingEnded()
    {

    }
    #endregion

    #region Timeout
    void OnTimeoutStarted()
    {
        countdown = timeoutTime;
        countdownevent = 5;
    }

    void OnTimeout()
    {
        if (countdown < 0)
        {
            ChangeState(WaveSystemState.Spawning);
        }
    }

    void OnTimeoutEnded()
    {

    }
    #endregion

    #region GameOver
    void OnGameOverStarted()
    {
        countdown = gameOverTime;

        GameManager.instance.savegameManager.saveData.wmTotalMatches += 1;
        GameManager.instance.savegameManager.saveData.wmTotalGameTime += totalGameTimer;
        if (GameManager.instance.savegameManager.saveData.wmLongestMatch < totalGameTimer) GameManager.instance.savegameManager.saveData.wmLongestMatch = totalGameTimer;
        GameManager.instance.savegameManager.saveData.wmTotalRounds += round-1;
        if (GameManager.instance.savegameManager.saveData.wmBestRound < round - 1) GameManager.instance.savegameManager.saveData.wmBestRound = round - 1;
        GameManager.instance.savegameManager.saveData.wmTotalKills += killcounter;
        if (GameManager.instance.savegameManager.saveData.wmTotalKills < killcounter) GameManager.instance.savegameManager.saveData.wmTotalKills = killcounter;
        GameManager.instance.savegameManager.Save();
    }

    void OnGameOver()
    {
        if (countdown <= 0)
        {
            SceneManager.LoadScene(previousHub);
            Destroy(this);
        }
    }

    void OnGameOverEnded()
    {

    }
    #endregion

    private void CalculateWave()
    {

        spawnPool = new List<GameObject>();
        int amount = 0; //result of enemy amount calculation
        amount = round * 3 + 3;
        for (int i = 0; i < amount; i++)
        {
            spawnPool.Add(commonEnemies[Random.Range(0, commonEnemies.Count - 1)]);
        }
        amount = Mathf.CeilToInt((round - 5f) / 6f);
        for (int i = 0; i < amount; i++)
        {
            spawnPool.Add(RareEnemies[Random.Range(0, commonEnemies.Count - 1)]);
        }

        if (round % 5 == 0)
        {

            amount = Mathf.FloorToInt(round + 5 / 10);
            for (int i = 0; i < amount; i++)
            {
                spawnPool.Add(BossEnemies[Random.Range(0, commonEnemies.Count - 1)]);
            }
        }

    }
}
