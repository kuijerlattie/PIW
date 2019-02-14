using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NotificationDataBase", menuName = "SO/DontTouch/NotificationDataBase", order = 1)]
public class NotificationDataBase : ScriptableObject
{

    public List<ChallengeNotification> challengeNotifications;

    public ChallengeNotification GetChallengeNotificationByChallengeID(int oID)
    {
        ChallengeNotification notification = challengeNotifications.Find(X => X.ChallengeID == oID);
        if (notification == null)
            notification = challengeNotifications.Find(X => X.ChallengeID == 0);
        return notification;
    }
}