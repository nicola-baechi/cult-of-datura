using UnityEngine;

public class BackgroundChanger : MonoBehaviour
{
    [SerializeField] private Sprite normalMap;
    [SerializeField] private Sprite hypnotizedMap;
    
    private SpriteRenderer _spriteRenderer;

    private void OnDisable()
    {
        EventManager.Instance.onPlayerHit.RemoveListener(SetHypnotizedMap);
        EventManager.Instance.onPlayerDie.RemoveListener(SetHypnotizedMap);
        EventManager.Instance.onPlayerReachStart.RemoveListener(SetHypnotizedMap);
        
        EventManager.Instance.onPlayerCollectHealItem.RemoveListener(SetNormalMap);
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        EventManager.Instance.onPlayerHit.AddListener(SetHypnotizedMap);
        EventManager.Instance.onPlayerDie.AddListener(SetHypnotizedMap);
        EventManager.Instance.onPlayerReachStart.AddListener(SetHypnotizedMap);
        
        EventManager.Instance.onPlayerCollectHealItem.AddListener(SetNormalMap);
        SetNormalMap();
    }

    private void SetNormalMap()
    {
        _spriteRenderer.sprite = normalMap;
    }

    private void SetHypnotizedMap()
    {
        _spriteRenderer.sprite = hypnotizedMap;
    }
}
