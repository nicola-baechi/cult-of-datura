using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public PlayerMovement player;
    public GameObject healItemPrefab;

    [SerializeField] private UnityEvent onGameOver;
    
    public void SpawnHealItem()
    {
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
        onGameOver?.Invoke();
    }

    public void DestroyAllHealItems()
    {
        GameObject[] healItems = GameObject.FindGameObjectsWithTag("Heal");
        foreach (GameObject healItem in healItems)
        {
            Destroy(healItem);
        }
    }
}