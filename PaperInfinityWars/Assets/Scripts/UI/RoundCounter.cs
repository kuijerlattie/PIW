using UnityEngine;
using TMPro;

public class RoundCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI RoundNumberText;
    [SerializeField] TextMeshProUGUI RoundText;

    void OnEnable()
    {
        GameManager.instance.eventManager.WMOnRoundStart.AddListener(OnRoundStart);
    }

    void OnDisable()
    {
        GameManager.instance.eventManager.WMOnRoundStart.RemoveListener(OnRoundStart);
    }

    void OnRoundStart(int round)
    {
        if (RoundText.enabled == false)
        {
            RoundText.enabled = true;
            RoundNumberText.enabled = true;
        }
        RoundNumberText.text = round.ToString();
    }
}
