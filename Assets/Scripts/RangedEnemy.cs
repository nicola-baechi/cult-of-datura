using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public GameObject projectile;
    public Transform projectilePosition;
    private GameObject player;

    [SerializeField] private float cooldown = 2;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer > 6) return;

        timer += Time.deltaTime;

        if (timer > cooldown)
        {
            timer = 0;
            shoot();
        }
    }

    private void shoot()
    {
        Instantiate(projectile, projectilePosition.position, Quaternion.identity);
    }
}