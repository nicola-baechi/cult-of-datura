using UnityEngine;
using UnityEngine.Events;

public class TriggerListener : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        EventManager.Instance.PlayerEnterTrigger();
    }
}