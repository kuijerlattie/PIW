using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUIScript : MonoBehaviour {

    [SerializeField] Image hitpointsFill;
    [SerializeField] Image experienceFill;

    int hitpoints = 0;
    int maxhitpoints = 0;
    int minxp = 0;
    int xp = 0;
    int maxXp = 0;
    int level = 0;

    // Use this for initialization
    void Start() {
        GameManager.instance.eventManager.PlayerHitpointsChanged.AddListener(OnPlayerHitpointsChanged);
        GameManager.instance.eventManager.playerXPChanged.AddListener(OnPlayerXPChanged);
    }

    // Update is called once per frame
    void Update() {

    }

    void UpdateUI()
    {
        if (maxhitpoints != 0 && hitpoints != 0)
            hitpointsFill.fillAmount = (float)hitpoints / (float)maxhitpoints;
        else
            hitpointsFill.fillAmount = 0;

        if ((xp - minxp) != 0 && (maxXp - minxp != 0))
            experienceFill.fillAmount = (float)(xp - minxp) / (float)(maxXp - minxp);
        else
            experienceFill.fillAmount = 0;
    }

    void OnPlayerHitpointsChanged(KillablePawn player)
    {
        hitpoints = player.HitPoints;
        maxhitpoints = player.MaxHitpoints;
        UpdateUI();
    }

    void OnPlayerXPChanged(Player player)
    {
        xp = player.GetXP();
        maxXp = player.GetXPForNextLevel();
        minxp = player.GetXPForLevelStart();
        level = player.GetLevel();
        UpdateUI();
    }
}
