using UnityEngine;

public class StartTriggerListener : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        EventManager.Instance.PlayerReachStart();
    }
}
