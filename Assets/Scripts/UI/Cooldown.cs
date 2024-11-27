using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    public GameObject parent; 
    public Image indicator;
    public float MaxTime { set; get; }
    public float remainingTime;

    private bool _isCooldownStarted;

    private void Start()
    {
        remainingTime = MaxTime;
        parent.SetActive(false);
    }

    private void Update()
    {
        if (!_isCooldownStarted) return;
        
        if(remainingTime > 0)
        {
            parent.SetActive(true);
            remainingTime -= Time.deltaTime;
            indicator.fillAmount = remainingTime / MaxTime;
        }
        else
        {
            parent.SetActive(false);
            _isCooldownStarted = false;
            remainingTime = MaxTime;
        }
    }
    
    public void StartCooldown()
    {
        _isCooldownStarted = true;
    }
}
