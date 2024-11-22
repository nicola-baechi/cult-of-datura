using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public PlayerMovement player;
    public GameObject healItemPrefab;

    [SerializeField] private UnityEvent onGameOver;
    [SerializeField] private GameObject vignette;
    private int _health = 3;
    private bool _isHypnotized;
    private bool _isShieldActive;
    
    private void OnEnable()
    {
        gameObject.GetComponent<EventManager>().OnPlayerEnterTrigger += HandlePlayerHitByEnemy;
        gameObject.GetComponent<EventManager>().OnPlayerCollectHealItem += ResetHypnotizedState;
        gameObject.GetComponent<EventManager>().OnPlayerCollectShieldItem += ActivateShield;
        gameObject.GetComponent<EventManager>().OnPlayerMissHealItem += SpawnHealItem;
        gameObject.GetComponent<EventManager>().OnPlayerCollectProjectileItem += updateInventory;
        gameObject.GetComponent<EventManager>().OnPlayerReachStart += OnGameOver;
    }

    private void OnDisable()
    {
        gameObject.GetComponent<EventManager>().OnPlayerEnterTrigger -= HandlePlayerHitByEnemy;
        gameObject.GetComponent<EventManager>().OnPlayerCollectHealItem -= ResetHypnotizedState;
        gameObject.GetComponent<EventManager>().OnPlayerCollectShieldItem -= ActivateShield;
        gameObject.GetComponent<EventManager>().OnPlayerMissHealItem -= SpawnHealItem;
        gameObject.GetComponent<EventManager>().OnPlayerCollectProjectileItem -= updateInventory;
        gameObject.GetComponent<EventManager>().OnPlayerReachStart -= OnGameOver;
    }

    public void HandlePlayerHitByEnemy()
    {
        if (_isShieldActive)
        {
            return;
        }
        
        _health--;
        Debug.Log("health reduced to: " + _health);
        if (_isHypnotized || _health <= 0)
        {
            player.SetFullyHypnotized();
            OnGameOver();
            return;
        }

        vignette.GetComponent<SpriteRenderer>().enabled = true;
        player.ReverseVerticalMoveSpeed();
        _isHypnotized = true;
        SpawnHealItem();
    }

    private void ResetHypnotizedState()
    {
        _isHypnotized = false;
        player.ReverseVerticalMoveSpeed();
        vignette.GetComponent<SpriteRenderer>().enabled = false;
        
        DestroyAllHealItems();
    }
    
    private void DestroyAllHealItems()
    {
        GameObject[] healItems = GameObject.FindGameObjectsWithTag("Heal");
        foreach (GameObject healItem in healItems)
        {
            Destroy(healItem);
        }
    }

    private void SpawnHealItem()
    {
        Debug.Log("respawning heal item");
        Vector3 spawnPosition;
        do
        {
            float randomX = Random.Range(-6f, 6f);
            float randomY = Random.Range(-10f, -5f);
            spawnPosition = player.transform.position + new Vector3(randomX, randomY, 0);
        } while (Physics2D.OverlapCircle(spawnPosition, 0.5f) != null);
        Instantiate(
            healItemPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }

    private void updateInventory()
    {
        player.GetComponent<PlayerAction>().AddProjectile();
    }

    private void ActivateShield()
    {
        _isShieldActive = true;
        Invoke(nameof(DeactivateShield), 5);
    }
    
    private void DeactivateShield()
    {
        _isShieldActive = false;
    }

    public void OnGameOver()
    {
        onGameOver?.Invoke();
    }
}