using UnityEngine;

public class StartListener : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        EventManager.Instance.onPlayerReachStart.Invoke();
    }
}
