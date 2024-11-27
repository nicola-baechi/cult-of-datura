using UnityEngine;

public class EndListener : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EventManager.Instance.onPlayerReachEnd.Invoke();
        }
    }
}