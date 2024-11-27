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
    public UnityEvent onPlayerHitRangedEnemy;
    public UnityEvent onPlayerCollectHealItem;
    public UnityEvent onPlayerCollectShieldItem;
    public UnityEvent onPlayerCollectProjectileItem;
    public UnityEvent onPlayerShootProjectile;
    public UnityEvent onPlayerHit;
    public UnityEvent onPlayerDie;
    public UnityEvent onPlayerReachEnd;
    public UnityEvent onShieldBlockDamage;
}