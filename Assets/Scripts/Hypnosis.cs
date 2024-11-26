using UnityEngine;
using UnityEngine.Events;
using Image = UnityEngine.UI.Image;

public class Hypnosis : MonoBehaviour
{
    public UnityEvent onPlayerHit;
    public UnityEvent onPlayerFullyHypnotized;

    [SerializeField] private int health = 3;
    [SerializeField] private GameObject vignette;

    private bool _isHypnotized;
    
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
                onPlayerFullyHypnotized.Invoke();
            }
            else
            {
                onPlayerHit.Invoke();
            }

            _isHypnotized = true;
            vignette.GetComponent<Image>().enabled = true;
        }
    }
}