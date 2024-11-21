using System;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;
    private GameObject player;
    private int _projectileCount;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if(_projectileCount < 1) return;
        
        // Check for enemies within range
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 10f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("RangedEnemy"))
            {
                // Enemy found within range, shoot projectile
                ShootProjectile(hitCollider.gameObject);
                break;
            }
        }
    }
    
    private void ShootProjectile(GameObject target)
    {
        Debug.Log("Shooting projectile at " + target.name);
        var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<PlayerProjectile>().shoot(target.transform);
        --_projectileCount;
    }
    
    private void OnDrawGizmosSelected()
    {
        // Draw the detection radius in the editor for visualization
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 10f);
    }

    public void AddProjectile()
    {
        _projectileCount++;
    }
}