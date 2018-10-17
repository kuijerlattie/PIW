using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour {

    List<SpawnPoint> EnemySpawns;

	// Use this for initialization
	void OnEnable () {
        EnemySpawns = new List<SpawnPoint>();
        GameManager.instance.enemySpawnManager = this;
	}
	
	// Update is called once per frame
	void Update () {
		
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
