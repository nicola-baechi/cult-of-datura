using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public event Action OnPlayerEnterTrigger;

    public event Action OnPlayerCollectHealItem;

    public event Action OnPlayerCollectShieldItem;

    public event Action OnPlayerMissHealItem; 
    
    public event Action OnPlayerCollectProjectileItem;

    public event Action OnPlayerReachStart;

    public void PlayerEnterTrigger()
    {
        OnPlayerEnterTrigger?.Invoke();
    }
    
    public void PlayerCollectHealItem()
    {
        OnPlayerCollectHealItem?.Invoke();
    }
    
    public void PlayerCollectShieldItem()
    {
        OnPlayerCollectShieldItem?.Invoke();
    }
    
    public void PlayerMissHealItem()
    {
        OnPlayerMissHealItem?.Invoke();
    }
    
    public void PlayerCollectProjectileItem()
    {
        OnPlayerCollectProjectileItem?.Invoke();
    }
    
    public void PlayerReachStart()
    {
        OnPlayerReachStart?.Invoke();
    }
}