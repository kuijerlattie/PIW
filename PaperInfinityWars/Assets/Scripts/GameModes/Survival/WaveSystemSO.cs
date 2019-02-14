using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveModeSO", menuName = "SO/GameModes/WaveModeTemplate", order = 1)]
public class WaveSystemSO : ScriptableObject {
    public string SceneName = "";
    public List<GameObject> CommonEnemyList = new List<GameObject>();
    public List<GameObject> RareEnemyList = new List<GameObject>();
    public List<GameObject> BossEnemyList = new List<GameObject>();
}
