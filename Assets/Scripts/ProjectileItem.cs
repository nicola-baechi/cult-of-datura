using UnityEngine;

public class ProjectileItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            EventManager.Instance.PlayerCollectProjectileItem();
        }
    }
}