using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Worldswitcher : MonoBehaviour {

    public GameObject enemy;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                //switch scene to w1c1
                //GameManager.instance.SetGameMode<WaveSystem>();
                WaveSystem system = (WaveSystem) GameManager.instance.gameMode;
                system.commonEnemies.Add(enemy);
                system.RareEnemies.Add(enemy);
                system.BossEnemies.Add(enemy);
                SceneManager.LoadScene("W1C1");
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                //switch scene to w2c2
                SceneManager.LoadScene("W1C2");

            }
        }
    }
}
