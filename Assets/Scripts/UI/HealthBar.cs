using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject parent;
    
    public Sprite fullHealth;
    public Sprite twoHealth;
    public Sprite oneHealth;

    private Image _image;
    private int _health = 3;
    private int _tempHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        _image.sprite = fullHealth;
        EventManager.Instance.onPlayerHit.AddListener(UpdateToSuddenDeathHealth);
        EventManager.Instance.onPlayerCollectHealItem.AddListener(UpdateHealth);
        EventManager.Instance.onPlayerDie.AddListener(HideHealthBar);
    }

    private void UpdateToSuddenDeathHealth()
    {
        _health--;
        _image.sprite = oneHealth;
    }
    
    private void HideHealthBar()
    {
        parent.SetActive(false);
    }
    
    private void UpdateHealth()
    {
        switch (_health)
        {
            case 2:
                _image.sprite = twoHealth;
                break;
            case 1:
                _image.sprite = oneHealth;
                break;
            case 0:
                parent.SetActive(false);
                break;
        }
    }
}
