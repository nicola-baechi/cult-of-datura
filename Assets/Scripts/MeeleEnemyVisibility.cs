using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeeleEnemyVisibility : MonoBehaviour
{
    [SerializeField] private UnityEvent onTriggerEnterEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onTriggerEnterEvent?.Invoke();
        }
    }
}