using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject healItemPrefab;

    private int _health = 3;
    private bool _isHypnotized;
    private GameObject _healItemInstance;
    
    [SerializeField]
    private UnityEvent onGameOver;

    private void OnEnable()
    {
        gameObject.GetComponent<EventManager>().OnPlayerEnterTrigger += SetHypnotizedState;
        gameObject.GetComponent<EventManager>().OnPlayerCollectHealItem += ResetHypnotizedState;
        gameObject.GetComponent<EventManager>().OnPlayerCollectProjectileItem += updateInventory;
    }

    private void OnDisable()
    {
        gameObject.GetComponent<EventManager>().OnPlayerEnterTrigger -= SetHypnotizedState;
        gameObject.GetComponent<EventManager>().OnPlayerCollectHealItem -= ResetHypnotizedState;
        gameObject.GetComponent<EventManager>().OnPlayerCollectProjectileItem -= updateInventory;
    }

    public void SetHypnotizedState()
    {
        _health--;
        Debug.Log("health reduced to: " + _health);
        if(_health <= 0)
        {
            OnGameOver();
            return;
        }
        if (_isHypnotized) return;

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
        _healItemInstance = Instantiate(
            healItemPrefab,
            player.transform.position + new Vector3(0, -5, 0),
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