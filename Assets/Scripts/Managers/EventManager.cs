using System;
using UnityEngine;
using UnityEngine.Events;

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

    public UnityEvent onPlayerMissHealItem;
    public UnityEvent onPlayerReachStart;
    public UnityEvent OnPlayerHitRangedEnemy;

}