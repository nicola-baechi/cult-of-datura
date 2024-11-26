using UnityEngine;

public class StartListener : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        EventManager.Instance.onPlayerReachStart.Invoke();
    }
}
