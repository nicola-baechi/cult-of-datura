using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject healItemPrefab;

    private int _playerHitAmount;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Cursor.visible = false;
        
        EventManager.Instance.onPlayerHit.AddListener(HandlePlayerHit);
        EventManager.Instance.onPlayerCollectHealItem.AddListener(DestroyAllHealItems);
        EventManager.Instance.onPlayerMissHealItem.AddListener(SpawnHealItem);
        EventManager.Instance.onPlayerDie.AddListener(HandleGameOver);
        EventManager.Instance.onPlayerReachStart.AddListener(HandleGameOver);
    }
    
    private void OnDisable()
    {
        EventManager.Instance.onPlayerHit.RemoveListener(HandlePlayerHit);
        EventManager.Instance.onPlayerCollectHealItem.RemoveListener(DestroyAllHealItems);
        EventManager.Instance.onPlayerMissHealItem.RemoveListener(SpawnHealItem);
        EventManager.Instance.onPlayerDie.RemoveListener(HandleGameOver);
        EventManager.Instance.onPlayerReachStart.RemoveListener(HandleGameOver);
    }

    private void HandlePlayerHit()
    {
        SpawnHealItem();
        _playerHitAmount++;
    }

    public void SpawnHealItem()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("respawning heal item");
        Vector3 spawnPosition;
        do
        {
            float randomX = Random.Range(-6f, 6f);
            float randomY = Random.Range(-10f, -5f);
            spawnPosition = player.transform.position + new Vector3(randomX, randomY, 0);
        } while (Physics2D.OverlapCircle(spawnPosition, 0.5f) != null);

        Debug.Log(player.transform.position);
        Instantiate(
            healItemPrefab,
            spawnPosition,
            Quaternion.identity
        );
    }

    public void HandleGameOver()
    {
        DestroyAllHealItems();
    }

    public void DestroyAllHealItems()
    {
        GameObject[] healItems = GameObject.FindGameObjectsWithTag("Heal");
        foreach (GameObject healItem in healItems)
        {
            Destroy(healItem);
        }
    }
    
    public int GetPlayerHitAmount()
    {
        return _playerHitAmount;
    }
}