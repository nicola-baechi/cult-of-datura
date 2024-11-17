using UnityEngine;
using UnityEngine.Events;

public class TriggerListener : MonoBehaviour
{
    [SerializeField] private UnityEvent onTriggerEnterEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        onTriggerEnterEvent?.Invoke();

    }
}