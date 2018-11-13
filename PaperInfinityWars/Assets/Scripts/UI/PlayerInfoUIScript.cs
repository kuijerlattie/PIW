using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfoUIScript : MonoBehaviour {

    public bool showHitpointsText = true;
    public bool showXPText = true;

    [SerializeField] Image hitpointsFill;
    [SerializeField] Image experienceFill;
    [SerializeField] TextMeshProUGUI levelNumber;
    [SerializeField] TextMeshProUGUI hitpointsText;
    [SerializeField] TextMeshProUGUI xpText;
    [SerializeField] Image coinsImage;
    [SerializeField] TextMeshProUGUI coinsText;


    int hitpoints = 0;
    int maxhitpoints = 0;
    int minxp = 0;
    int xp = 0;
    int maxXp = 0;
    int level = 0;

    int coins = 0;
    float coinsFadeTimer = 0;

    // Use this for initialization
    void OnEnable()
    {
        GameManager.instance.eventManager.PlayerHitpointsChanged.AddListener(OnPlayerHitpointsChanged);
        GameManager.instance.eventManager.PlayerXPChanged.AddListener(OnPlayerXPChanged);
        GameManager.instance.eventManager.CoinsChanged.AddListener(OnCoinsChanged);
    }

    void OnDisable()
    {
        GameManager.instance.eventManager.PlayerHitpointsChanged.RemoveListener(OnPlayerHitpointsChanged);
        GameManager.instance.eventManager.PlayerXPChanged.RemoveListener(OnPlayerXPChanged);
        GameManager.instance.eventManager.CoinsChanged.RemoveListener(OnCoinsChanged);
    }

    // Update is called once per frame
    void Update() {
        UpdatePlayerInfoUI();
        UpdateCoinUI();
    }

    void UpdatePlayerInfoUI()
    {
        if (maxhitpoints != 0 && hitpoints != 0)
            hitpointsFill.fillAmount = (float)hitpoints / (float)maxhitpoints;
        else
            hitpointsFill.fillAmount = 0;

        hitpointsText.text = hitpoints.ToString() + " / " + maxhitpoints.ToString();
        hitpointsText.enabled = showHitpointsText;

        if ((xp - minxp) != 0 && (maxXp - minxp != 0))
            experienceFill.fillAmount = (float)(xp - minxp) / (float)(maxXp - minxp);
        else
            experienceFill.fillAmount = 0;

        xpText.text = xp.ToString() + " / " + maxXp.ToString();
        xpText.enabled = showXPText;

        levelNumber.SetText(level.ToString());
    }

    void UpdateCoinUI()
    {
        coinsText.SetText(coins.ToString());
        if (coinsFadeTimer <= 0 && coinsImage.gameObject.activeInHierarchy)
        {
            coinsImage.gameObject.SetActive(false);
        }
        else
        {
            coinsFadeTimer -= Time.deltaTime;
        }
    }

    void OnPlayerHitpointsChanged(Player player)
    {
        hitpoints = player.HitPoints;
        maxhitpoints = player.MaxHitpoints;
    }

    void OnPlayerXPChanged(Player player)
    {
        xp = player.GetXP();
        maxXp = player.GetXPForNextLevel();
        minxp = player.GetXPForLevelStart();
        level = player.GetLevel();
    }

    void OnCoinsChanged(int newcoins)
    {
        coins = newcoins;
        coinsFadeTimer = 5f;
        coinsImage.gameObject.SetActive(true);
    }
}
