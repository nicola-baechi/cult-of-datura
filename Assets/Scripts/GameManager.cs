using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject healItemPrefab;

    [SerializeField] private UnityEvent onGameOver;
    
    private GameObject _healItemInstance;
    private int _health = 3;
    private bool _isHypnotized;
    
    private void OnEnable()
    {
        gameObject.GetComponent<EventManager>().OnPlayerEnterTrigger += HandleHypnotizedState;
        gameObject.GetComponent<EventManager>().OnPlayerCollectHealItem += ResetHypnotizedState;
        gameObject.GetComponent<EventManager>().OnPlayerCollectProjectileItem += updateInventory;
    }

    private void OnDisable()
    {
        gameObject.GetComponent<EventManager>().OnPlayerEnterTrigger -= HandleHypnotizedState;
        gameObject.GetComponent<EventManager>().OnPlayerCollectHealItem -= ResetHypnotizedState;
        gameObject.GetComponent<EventManager>().OnPlayerCollectProjectileItem -= updateInventory;
    }

    public void HandleHypnotizedState()
    {
        _health--;
        Debug.Log("health reduced to: " + _health);
        if (_health <= 0 || _isHypnotized)
        {
            OnGameOver();
            return;
        }

        _isHypnotized = true;
        player.GetComponent<PlayerMovement>().ReverseVerticalMoveSpeed();
        SpawnHealItem();
    }

    private void ResetHypnotizedState()
    {
        _isHypnotized = false;
        player.GetComponent<PlayerMovement>().ReverseVerticalMoveSpeed();
        Destroy(_healItemInstance);
    }

    private void SpawnHealItem()
    {
        Vector3 spawnPosition;
        do
        {
            float randomX = Random.Range(-7f, 7f); // Adjust the range as needed
            float randomY = Random.Range(-10f, -5f); // Adjust the range as needed
            spawnPosition = player.transform.position + new Vector3(randomX, randomY, 0);
        } while (Physics2D.OverlapCircle(spawnPosition, 0.5f) != null);
        _healItemInstance = Instantiate(
            healItemPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }

    private void updateInventory()
    {
        player.GetComponent<PlayerAction>().AddProjectile();
    }

    public void OnGameOver()
    {
        onGameOver?.Invoke();
    }
}