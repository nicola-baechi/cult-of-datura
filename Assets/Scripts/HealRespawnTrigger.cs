using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealRespawnTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        EventManager.Instance.PlayerMissHealItem();
    }
}
