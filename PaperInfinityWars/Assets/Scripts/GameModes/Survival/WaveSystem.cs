using System.Collections;
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
    public WaveSystemState gamestate;
    public float countdown = 0f;
    int normalsToSpawn;
    int raresToSpawn;
    int BossesToSpawn;
    EnemySpawnManager enemySpawnManager;
    bool LoadingHub = false;
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
    }

    void OnSpawning()
    {
        if (enemies.Count < maxSimultaniousEnemies)
        {
            if (normalsToSpawn > 0)
            {
                //spawn normal enemy
                GameObject enemy = Instantiate(commonEnemies[0], enemySpawnManager.GetRandomSpawnPointInPlayerRange(MaxSpawnRange).transform.position, Quaternion.identity);
                KillablePawn commonKillablePawn = enemy.GetComponent<KillablePawn>();
                enemies.Add(commonKillablePawn);
                normalsToSpawn--;
            }

            if (raresToSpawn > 0)
            {
                //spawn rare
                GameObject enemy = Instantiate(RareEnemies[0], enemySpawnManager.GetRandomSpawnPointInPlayerRange(MaxSpawnRange).transform.position, Quaternion.identity);
                KillablePawn commonKillablePawn = enemy.GetComponent<KillablePawn>();
                enemies.Add(commonKillablePawn);
                raresToSpawn--;
            }

            if (BossesToSpawn > 0)
            {
                //spawn boss
                GameObject enemy = Instantiate(BossEnemies[0], enemySpawnManager.GetRandomSpawnPointInPlayerRange(MaxSpawnRange).transform.position, Quaternion.identity);
                KillablePawn commonKillablePawn = enemy.GetComponent<KillablePawn>();
                enemies.Add(commonKillablePawn);
                BossesToSpawn--;
            }

            if (normalsToSpawn == 0 && raresToSpawn == 0 && BossesToSpawn == 0)
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
        normalsToSpawn = round * 3 + 3;
        raresToSpawn = Mathf.CeilToInt((round - 5f) / 6f);
        if (raresToSpawn < 0) raresToSpawn = 0;

        if (round % 5 == 0)
        {
            BossesToSpawn = Mathf.FloorToInt(round + 5 / 10);
            if (BossesToSpawn < 0) BossesToSpawn = 0;
        }
        else
        {
            //no bosses this round;
            BossesToSpawn = 0;
        }

    }
}
