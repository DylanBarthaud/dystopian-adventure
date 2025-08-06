using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private static EventManager instance;
    public static EventManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (EventManager.instance == null)
        {
            EventManager.instance = this;
        }
    }

    #region Events
    public event Action<PlayerState> onPlayerStateChange;
    public void OnPlayerStateChange(PlayerState playerState)
    {
        if (onPlayerStateChange != null)
        {
            onPlayerStateChange(playerState);
        }
    }
    #endregion
}
