using UnityEngine;
using Image = UnityEngine.UI.Image;

public class Hypnosis : MonoBehaviour
{
    [SerializeField] private int health = 3;

    private GameObject vignette;
    private bool _isHypnotized;

    private void OnDisable()
    {
        EventManager.Instance.onPlayerCollectHealItem.RemoveListener(ResetHypnosis);
    }

    private void Start()
    {
        EventManager.Instance.onPlayerCollectHealItem.AddListener(ResetHypnosis);
        vignette = GameObject.FindGameObjectWithTag("Vignette");
    }

    public void ResetHypnosis()
    {
        _isHypnotized = false;
        vignette.GetComponent<Image>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Damaging"))
        {
            if (GetComponent<ItemInteraction>()._isShieldActive) return;

            health--;
            if (health <= 0 || _isHypnotized)
            {
                Debug.Log("Player died");
                EventManager.Instance.onPlayerDie.Invoke();
            }
            else
            {
                EventManager.Instance.onPlayerHit.Invoke();
            }

            _isHypnotized = true;
            vignette.GetComponent<Image>().enabled = true;
        }
    }
}