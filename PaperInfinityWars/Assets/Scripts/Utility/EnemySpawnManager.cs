using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawnManager : MonoBehaviour {

    List<SpawnPoint> EnemySpawns;

	// Use this for initialization
	void OnEnable ()
    {
        EnemySpawns = new List<SpawnPoint>();
        SceneManager.sceneLoaded += OnSceneLoaded;
	}
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        EnemySpawns = new List<SpawnPoint>();
    }

    public void AddSpawn(SpawnPoint oSpawnPoint)
    {
        EnemySpawns.Add(oSpawnPoint);
    }

    public SpawnPoint GetSpawnPoint()
    {
        return EnemySpawns[0];
    }
}
