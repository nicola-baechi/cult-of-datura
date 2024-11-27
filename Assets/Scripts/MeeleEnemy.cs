using System.Collections;
using UnityEngine;

public class MeeleEnemy : MonoBehaviour
{
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
