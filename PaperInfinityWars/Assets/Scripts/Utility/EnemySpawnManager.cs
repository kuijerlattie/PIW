using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawnManager : MonoBehaviour {

    List<SpawnPoint> EnemySpawns;

    // Use this for initialization
    void OnEnable()
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

    public SpawnPoint GetRandomSpawnPoint()
    {
        return EnemySpawns[Random.Range(0, EnemySpawns.Count - 1)];
    }

    public SpawnPoint GetRandomSpawnPointInPlayerRange(float range)
    {
        List<SpawnPoint> tempspawnrange = new List<SpawnPoint>();
        tempspawnrange.AddRange(EnemySpawns.FindAll(X => Vector3.Distance(X.transform.position, GameManager.instance.player.transform.position) <= range && !CheckIfInView(X.transform.position)));
        if (tempspawnrange.Count != 0)
            return tempspawnrange[Random.Range(0, tempspawnrange.Count - 1)];
        else
            return GetRandomSpawnPoint();
    }

    private bool CheckIfInView(Vector3 oPosition)
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(oPosition);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        return onScreen;
    }
}
