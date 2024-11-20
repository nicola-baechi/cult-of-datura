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

    public void PlayerEnterTrigger()
    {
        OnPlayerEnterTrigger?.Invoke();
    }
    
    public void PlayerCollectHealItem()
    {
        OnPlayerCollectHealItem?.Invoke();
    }
}