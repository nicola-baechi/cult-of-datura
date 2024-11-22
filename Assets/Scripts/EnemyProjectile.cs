using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed;
    
    private GameObject player;
    private Rigidbody2D rb;
    private float timer;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        
        // rotate the projectile to face the player
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation + 90);
    }
    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit:" + other.gameObject.name);
            EventManager.Instance.PlayerEnterTrigger();
            Destroy(gameObject);
        }
    }
}