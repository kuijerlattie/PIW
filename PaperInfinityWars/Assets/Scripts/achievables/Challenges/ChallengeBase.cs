using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class ChallengeBase
{
    [Serializable]
    public enum ChallengeCategory
    {
        Career,
        Killer,
        Survival
    }

    public int challengeID = 0;
    public string challengeName = "";
    public string challengeDescription = "";
    public ChallengeCategory challengeCategory;
    public ChallengeNotification notificationObject;
    public int currentTier = 0;
    public int highestTier = 1;
    public bool completed = false;
    
    protected void GetNotification()
    {
        GameManager.instance.notificationDataBase.GetChallengeNotificationByChallengeID(challengeID);
    }

    public abstract void RegisterForEvents();
    public abstract void DeRegisterForEvents();
    public abstract int GetProgress();
    public abstract int GetGoal();
    public abstract int GetReward();
    public abstract string GetDescription();
}
