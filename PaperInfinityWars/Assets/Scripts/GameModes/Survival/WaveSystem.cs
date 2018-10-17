using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour {

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
    #endregion

    #region game stats
    float totalGameTimer = 0f; //time survived
    int killcounter = 0;
    #endregion

    #region Settings
    float warmupTime = 15f; //time before game starts in seconds
    float timeoutTime = 10f; //time between rounds in seconds
    int maxSimultaniousEnemies = 30;
    #endregion

    #region EnemySettings
    public List<GameObject> commonEnemies;
    public List<GameObject> RareEnemies;
    public List<GameObject> BossEnemies;
    #endregion

    // Use this for initialization
    void Start () {
        round = 0;
        enemies = new List<KillablePawn>();
        
        //these two lines to make sure that no on...ended is called yet. use changestate in other situations.
        gamestate = WaveSystemState.Warmup;
        OnWarmupStarted();
        //follow playerdeath event
        //follow enemydeath event
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

        totalGameTimer += Time.deltaTime;
        countdown -= Time.deltaTime;
    }

    void OnPlayerDeath()
    {
        ChangeState(WaveSystemState.GameOver);
    }

    void OnEnemyDeath()
    {
        enemies.RemoveAll(X => !X.alive);
    }

    #region Warmup
    void OnWarmupStarted()
    {

    }

    void OnWarmup()
    {

    }

    void OnWarmupEnded()
    {

    }
    #endregion

    #region Spawning
    void OnSpawningStarted()
    {

    }

    void OnSpawning()
    {

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

    }

    void OnWaitingEnded()
    {

    }
    #endregion

    #region Timeout
    void OnTimeoutStarted()
    {

    }

    void OnTimeout()
    {

    }

    void OnTimeoutEnded()
    {

    }
    #endregion

    #region GameOver
    void OnGameOverStarted()
    {

    }

    void OnGameOver()
    {

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
