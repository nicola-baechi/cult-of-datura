using UnityEngine;

public class EndListener : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        EventManager.Instance.onPlayerReachEnd.Invoke();
    }
}
