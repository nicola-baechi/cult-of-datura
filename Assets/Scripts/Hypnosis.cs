using UnityEngine;
using Image = UnityEngine.UI.Image;

public class Hypnosis : MonoBehaviour
{
    [SerializeField] private int health = 3;

    private GameObject vignette;
    private bool _isHypnotized;
    private bool isFullyHypnotized;

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
        if (other.CompareTag("Damaging") || other.CompareTag("MeeleEnemy"))
        {
            if (GetComponent<ItemInteraction>()._isShieldActive)
            {
                EventManager.Instance.onShieldBlockDamage.Invoke();
                return;
            }

            if (isFullyHypnotized) return;

            health--;
            if (health <= 0 || _isHypnotized)
            {
                EventManager.Instance.onPlayerDie.Invoke();
                isFullyHypnotized = true;
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