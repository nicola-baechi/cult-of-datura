using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float speed;
    
    [SerializeField]
    private Rigidbody2D rb;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10)
        {
            Destroy(gameObject);
        }
    }

    public void shoot(Transform targetPosition)
    {
        Debug.Log("targetPosition: " + targetPosition);
        Vector3 direction = targetPosition.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        
        // rotate the projectile to face the player
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation + 90);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("RangedEnemy"))
        {
            Debug.Log("hit:" + other.gameObject.name);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}