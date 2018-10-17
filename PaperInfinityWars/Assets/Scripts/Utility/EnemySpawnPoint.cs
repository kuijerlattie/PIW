using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : SpawnPoint {

	// Use this for initialization
	void Start () {
        if (GameManager.instance.enemySpawnManager != null)
            GameManager.instance.enemySpawnManager.AddSpawn(this);
        this.GetComponent<Renderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
