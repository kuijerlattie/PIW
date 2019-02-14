using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveModeStarter : MonoBehaviour{
    public WaveSystemSO waveSystemSO;
	// Use this for initialization
	
	public void OnClick()
    {
        GameManager.instance.SetGameMode<WaveSystem>();
        WaveSystem system = (WaveSystem)GameManager.instance.gameMode;
        system.commonEnemies = waveSystemSO.CommonEnemyList;
        system.RareEnemies = waveSystemSO.RareEnemyList;
        system.BossEnemies = waveSystemSO.BossEnemyList;
        system.previousHub = SceneManager.GetActiveScene().name;
        GameManager.instance.savegameManager.Save();
        SceneManager.LoadScene(waveSystemSO.SceneName);
    }
}
