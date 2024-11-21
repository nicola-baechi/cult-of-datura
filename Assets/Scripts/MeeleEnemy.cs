using UnityEngine;
using UnityEngine.Events;

public class MeeleEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EventManager.Instance.PlayerEnterTrigger();
        }
    }
}