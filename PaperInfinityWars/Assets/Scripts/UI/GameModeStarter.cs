using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeStarter : MonoBehaviour {

    public GameMode gameMode;
    public string sceneName;

	public void LoadSceneWithGamemode()
    {
        GameManager.instance.SetGameMode<gameMode.GetType>();
        WaveSystem system = (WaveSystem)GameManager.instance.gameMode;
        system.commonEnemies.Add(enemy);
        system.RareEnemies.Add(enemy);
        system.BossEnemies.Add(enemy);
        SceneManager.LoadScene(sceneName);
        //SceneManager.LoadScene("W1C1");
    }
}
