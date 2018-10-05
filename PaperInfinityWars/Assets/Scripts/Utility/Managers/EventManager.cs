using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {

    #region Saving
    public class OnSaveEvent : UnityEvent<SaveData> { }
    public OnSaveEvent OnSave = new OnSaveEvent();
    public class OnLoadEvent : UnityEvent<SaveData> { }
    public OnLoadEvent OnLoad = new OnLoadEvent();
    #endregion

    #region Combat
    public UnityEvent PlayerDeath = new UnityEvent();
    public UnityEvent EnemyDeath = new UnityEvent();
    #endregion
}
