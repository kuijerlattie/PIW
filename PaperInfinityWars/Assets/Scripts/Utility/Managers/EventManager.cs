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
    /// <summary>
    /// passes victim, killer, weapon of killer
    /// </summary>
    public class OnPawnDeathEvent : UnityEvent<KillablePawn, KillablePawn, Weapon> { }
    public OnPawnDeathEvent PlayerDeath = new OnPawnDeathEvent();
    public OnPawnDeathEvent EnemyDeath = new OnPawnDeathEvent();
    #endregion

    #region Player
    public class PlayerHitpointsChangedEvent : UnityEvent<Player> { }
    public PlayerHitpointsChangedEvent PlayerHitpointsChanged = new PlayerHitpointsChangedEvent();
    public class PlayerXPChangedEvent : UnityEvent<Player> { }
    public PlayerXPChangedEvent PlayerXPChanged = new PlayerXPChangedEvent();
    public class XPDropEvent : UnityEvent<int, KillablePawn> { }
    public XPDropEvent XPDrop = new XPDropEvent();
    public class CoinsChangedEvent : UnityEvent<int> { }
    public CoinsChangedEvent CoinsChanged = new CoinsChangedEvent();
    #endregion
}
