using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class ChallengeBase
{
    public enum ChallengeCategory
    {
        Bootcamp,
        Killer,
        Survival
    }

    public int challengeID = 0;
    public string challengeName = "";
    public string challengeDescription = "";
    public ChallengeCategory challengeCategory = ChallengeCategory.Bootcamp;
    public ChallengeNotification notificationObject;
    
    protected void GetNotification()
    {
        GameManager.instance.notificationDataBase.GetChallengeNotificationByChallengeID(challengeID);
    }

    public abstract void RegisterForEvents();
    public abstract void DeRegisterForEvents();
    public abstract void GetProgress();
    public abstract void GetCurrentTier();
}
