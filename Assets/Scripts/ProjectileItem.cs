using UnityEngine;

public class ProjectileItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        EventManager.Instance.PlayerCollectProjectileItem();
    }
}
