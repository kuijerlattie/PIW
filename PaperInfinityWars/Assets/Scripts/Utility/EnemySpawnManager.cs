using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour {

    List<SpawnPoint> EnemySpawns;

	// Use this for initialization
	void OnEnable () {
        EnemySpawns = new List<SpawnPoint>();
        Debug.Log("spawnlist initialized");
	}

    void Initialize()
    {

    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void AddSpawn(SpawnPoint oSpawnPoint)
    {
        Debug.Log("spawnpoint added");
        EnemySpawns.Add(oSpawnPoint);
    }

    public SpawnPoint GetSpawnPoint()
    {
        Debug.Log(EnemySpawns.Count);
        return EnemySpawns[0];
    }
}
