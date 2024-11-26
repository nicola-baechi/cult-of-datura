using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    
    private int _projectileCount;
    public bool _isShieldActive;

    private void Update()
    {
        if (_projectileCount < 1) return;

        ShootProjectile(GetNearestRangedEnemyInRange());
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Heal":
                Debug.Log("Heal collected in ItemInteraction");
                EventManager.Instance.onPlayerCollectHealItem.Invoke();
                Destroy(other.gameObject);
                break;
            case "Projectile":
                AddProjectile();
                Destroy(other.gameObject);
                break;
            case "Shield":
                ActivateShield();
                Destroy(other.gameObject);
                break;
        }
    }

    private void AddProjectile()
    {
        _projectileCount++;
    }

    private void ShootProjectile(GameObject target)
    {
        Debug.Log("Shooting projectile at " + target.name);
        var projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<PlayerProjectile>().shoot(target.transform);
        --_projectileCount;
    }

    private GameObject GetNearestRangedEnemyInRange()
    {
        GameObject[] rangedEnemies = GameObject.FindGameObjectsWithTag("RangedEnemy");
        GameObject nearestObject = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject rangedEnemy in rangedEnemies)
        {
            float distance = Vector3.Distance(currentPosition, rangedEnemy.transform.position);

            // Check if the object is within range
            if (distance <= 8f && distance < minDistance)
            {
                minDistance = distance;
                nearestObject = rangedEnemy;
            }
        }

        return nearestObject;
    }
    
    private void ActivateShield()
    {
        _isShieldActive = true;
        EventManager.Instance.onPlayerCollectShieldItem.Invoke();
        Invoke(nameof(DeactivateShield), 5);
    }
    
    private void DeactivateShield()
    {
        _isShieldActive = false;
    }
}