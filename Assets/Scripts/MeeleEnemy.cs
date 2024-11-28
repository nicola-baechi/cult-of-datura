using System;
using System.Collections;
using UnityEngine;

public class MeeleEnemy : MonoBehaviour
{
    public float initialSpeed = 1f;

    private GameObject _player;
    private float _distance;
    private float currentSpeed;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        currentSpeed = initialSpeed;
    }

    private void Update()
    {
        _distance = Vector2.Distance(transform.position, _player.transform.position);
        Vector2 direction = _player.transform.position - transform.position;
        direction.Normalize();


        if (_distance < 6)
        {
            transform.position = Vector2.MoveTowards(
                this.transform.position,
                _player.transform.position,
                currentSpeed * Time.deltaTime
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // disable box collider for 3s to prevent multiple hits
            StartCoroutine(DisableCollider());
        }
    }

    private IEnumerator DisableCollider()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(3f);
        GetComponent<BoxCollider2D>().enabled = true;
    }
}