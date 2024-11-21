using System;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private int projectileCount;
    
    public void AddProjectile()
    {
        projectileCount++;
    }

    private void Update()
    {
        // find gameobject with tag "RangedEnemy" in a range of 10
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("RangedEnemy"))
            {
                if (projectileCount > 0)
                {
                    // shoot projectile
                    projectileCount--;
                    Debug.Log("Projectile shot!");
                }
            }
        }
    }
}
