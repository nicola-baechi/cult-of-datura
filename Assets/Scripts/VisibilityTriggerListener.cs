using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VisibilityTriggerListener : MonoBehaviour
{
    [SerializeField] private UnityEvent onTriggerEnterEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        onTriggerEnterEvent?.Invoke();
    }
}
