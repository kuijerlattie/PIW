using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownUIScript : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI CountdownText;
    float countdown = 0f;

    void OnEnable()
    {
        GameManager.instance.eventManager.WMOnRoundCountdown.AddListener(OnRoundCountdown);
    }

    void OnDisable()
    {
        GameManager.instance.eventManager.WMOnRoundCountdown.RemoveListener(OnRoundCountdown);
    }

    void OnRoundCountdown(int roundnumber, WaveSystem.WaveSystemState wavestate)
    {
        if (wavestate == WaveSystem.WaveSystemState.Timeout || wavestate == WaveSystem.WaveSystemState.Warmup)
        {
            if (roundnumber != 0)
            {
                CountdownText.text = roundnumber.ToString();
                CountdownText.enabled = true;
                countdown = 0.5f;
            }
        }
    }

    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown < 0)
        {
            CountdownText.enabled = false;
        }
    }
}
