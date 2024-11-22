using UnityEngine;

public class ShieldTriggerListener : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        EventManager.Instance.PlayerCollectShieldItem();
        Destroy(gameObject);
    }
}
