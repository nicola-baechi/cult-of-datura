using UnityEngine;

public class HealTriggerListener : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        EventManager.Instance.PlayerCollectHealItem();
    }
}
