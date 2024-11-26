using UnityEngine;

public class HealRespawnListener : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EventManager.Instance.onPlayerMissHealItem.Invoke();
        }
    }
}