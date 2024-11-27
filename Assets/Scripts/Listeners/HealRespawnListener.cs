using UnityEngine;

public class HealRespawnListener : MonoBehaviour
{

    private bool respawnTriggered;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (respawnTriggered) return;
            respawnTriggered = true;
            EventManager.Instance.onPlayerMissHealItem.Invoke();
            Debug.Log("Player missed heal item");
        }
    }
}