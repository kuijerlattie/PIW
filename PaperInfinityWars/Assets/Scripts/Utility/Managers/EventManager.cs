using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {

    #region Saving
    public class OnSaveEvent : UnityEvent { }
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
    public class PlayerCurrencyChangedEvent : UnityEvent<CurrencyManager> { }
    public PlayerCurrencyChangedEvent PlayerCurrencyChanged = new PlayerCurrencyChangedEvent();
    public class XPDropEvent : UnityEvent<int> { }
    public XPDropEvent XPDrop = new XPDropEvent();
    public class LevelUpEvent : UnityEvent<CurrencyManager> { }
    public LevelUpEvent LevelUp = new LevelUpEvent();
    #endregion

    #region GameStates
    public class GameOverEvent : UnityEvent<GameMode>{}
    public GameOverEvent GameOver = new GameOverEvent();
    public class EndGameEvent : UnityEvent { }
    public EndGameEvent EndGame = new EndGameEvent();
    #endregion

    #region WaveModeEvents
    public class WMOnRoundEndEvent : UnityEvent<int> { }
    public WMOnRoundEndEvent WMOnRoundEnd = new WMOnRoundEndEvent();
    public class WMOnRoundCountdownEvent : UnityEvent<int, WaveSystem.WaveSystemState> { }
    public WMOnRoundCountdownEvent WMOnRoundCountdown = new WMOnRoundCountdownEvent();
    public class WMOnRoundStartEvent : UnityEvent<int> { }
    public WMOnRoundStartEvent WMOnRoundStart = new WMOnRoundStartEvent();
    #endregion

    #region Notifications
    public class ShowNotificationEvent : UnityEvent<Notification> { }
    public ShowNotificationEvent ShowNotification = new ShowNotificationEvent();
    #endregion
}
