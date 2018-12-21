using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ChallengesScript : MonoBehaviour
{
    public Button CarreerButton;
    public Image CarreerLockImage;
    public Button KillerButton;
    public Image KillerLockImage;
    public Button SurvivalButton;
    public Image SurvivalLockImage;

    public RectTransform challengeOverviewCanvas;
    public GameObject ChallengePrefab;
    public MainMenuChallengeUI challengeUI;

    void OnEnable()
    {
        if (GameManager.instance.currencyManager.GetLevel() >= GameManager.instance.challengeManager.careerLevel)
        {
            CarreerButton.enabled = true;
            CarreerLockImage.gameObject.SetActive(false);
        }
        if (GameManager.instance.currencyManager.GetLevel() >= GameManager.instance.challengeManager.killerLevel)
        {
            KillerButton.enabled = true;
            KillerLockImage.gameObject.SetActive(false);
        }
        if (GameManager.instance.currencyManager.GetLevel() >= GameManager.instance.challengeManager.survivalLevel)
        {
            SurvivalButton.enabled = true;
            SurvivalLockImage.gameObject.SetActive(false);
        }
    }

    public void SetCategory(int category)
    {
        ChallengeBase.ChallengeCategory challengeCategory = (ChallengeBase.ChallengeCategory)category;
        
        //destroy all old objects
        foreach (RectTransform r in challengeOverviewCanvas.transform.GetComponentInChildren<RectTransform>())
        {
            Destroy(r.gameObject);
        }


        foreach (ChallengeBase challenge in GameManager.instance.challengeManager.GetAllChallenges())
        {
            if (challenge.challengeCategory == challengeCategory)
            {
                GameObject instance = Instantiate(ChallengePrefab, challengeOverviewCanvas);
                instance.GetComponent<ChallengeHoverScript>().SetChallengeID(challenge.challengeID);
                instance.GetComponent<ChallengeHoverScript>().SetOwner(this);
            }
        }
    }

    public void ShowChallengeWithID(int cID)
    {
        challengeUI.SetSelectedChallenge(GameManager.instance.challengeManager.GetChallengeByID(cID));
    }
}
