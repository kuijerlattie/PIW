using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] Canvas notificationCanvas;

    private Queue<Notification> notificationQueue;

    void Start()
    {
        GameManager.instance.eventManager.ShowNotification.AddListener(ShowNotification);
    }

    void OnDisable()
    {
        GameManager.instance.eventManager.ShowNotification.RemoveListener(ShowNotification);
    }

    void ShowNotification(Notification oNotification)
    {
        notificationQueue.Enqueue(oNotification);

    }

    void OnNotificationDone()
    {
        if (notificationQueue.Count > 0)
        {
            //show next
        }
    }
}
