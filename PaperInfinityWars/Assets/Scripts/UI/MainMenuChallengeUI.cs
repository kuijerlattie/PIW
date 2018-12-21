using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuChallengeUI : MonoBehaviour {

    public Image image;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI tierText;
    public TextMeshProUGUI rewardText;
    public Image progressbar;
    public TextMeshProUGUI progressText;

    public void SetSelectedChallenge(ChallengeBase oChallenge)
    {
        Debug.Log("challenge progress for: " + oChallenge.challengeName + " | progress: " + oChallenge.GetProgress() + " goal: " + oChallenge.GetGoal());
        titleText.text = oChallenge.challengeName.ToString();
        descriptionText.text = oChallenge.GetDescription();
        tierText.text = "Tier " + oChallenge.currentTier + " of " + oChallenge.highestTier;
        rewardText.text = oChallenge.GetReward().ToString() + "XP";
        progressText.text = oChallenge.GetProgress().ToString() + " / " + oChallenge.GetGoal().ToString();
        progressbar.fillAmount = (float)oChallenge.GetProgress() / (float)oChallenge.GetGoal();

        if (oChallenge.highestTier < 2)
        {
            tierText.enabled = false;
        }
        else
        {
            tierText.enabled = true;
        }
    }
}
