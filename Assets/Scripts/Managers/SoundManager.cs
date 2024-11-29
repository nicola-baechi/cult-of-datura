using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioSource hypnotizedSound;

    [SerializeField] private AudioSource hitSound;

    [SerializeField] private AudioSource healSound;

    [SerializeField] private AudioSource shieldSound;

    [SerializeField] private AudioSource projectileItemSound;
    
    [SerializeField] private AudioSource projectileShootSound;

    [SerializeField] private AudioSource startSceneSound;

    [SerializeField] private AudioSource mainSceneSound;

    [SerializeField] private AudioSource gameOverSound;
    
    [SerializeField] private AudioSource endSceneSound;
    
    [SerializeField] private AudioSource loreSceneSound;

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
        GameSceneManager.Instance.onSceneChangeToStart.AddListener(PlayStartSceneSound);
        GameSceneManager.Instance.onSceneChangeToMain.AddListener(PlayMainSceneSound);
        GameSceneManager.Instance.onSceneChangeToGameOver.AddListener(PlayGameOverSound);
        GameSceneManager.Instance.onSceneChangeToEnd.AddListener(PlayEndSound);
        GameSceneManager.Instance.onSceneChangeToLore.AddListener(PlayLoreSceneSound);
            
        EventManager.Instance.onPlayerCollectHealItem.AddListener(PlayHealItemSound);
        EventManager.Instance.onPlayerCollectHealItem.AddListener(StopHyponotizedSound);
        EventManager.Instance.onPlayerCollectHealItem.AddListener(PlayMainSceneSound);
        EventManager.Instance.onPlayerCollectShieldItem.AddListener(PlayShieldItemSound);
        EventManager.Instance.onPlayerCollectProjectileItem.AddListener(PlayProjectileItemSound);
        EventManager.Instance.onPlayerShootProjectile.AddListener(PlayProjectileShootSound);
        
        EventManager.Instance.onPlayerHit.AddListener(PlayHyponotizedSound);
        EventManager.Instance.onPlayerHit.AddListener(StopMainSceneSound);
        EventManager.Instance.onPlayerDie.AddListener(PlayHyponotizedSound);
        EventManager.Instance.onPlayerDie.AddListener(StopMainSceneSound);
        EventManager.Instance.onPlayerHitRangedEnemy.AddListener(PlayRangedEnemyHitSound);
    }

    private void OnDisable()
    {
        GameSceneManager.Instance.onSceneChangeToStart.RemoveListener(PlayStartSceneSound);
        GameSceneManager.Instance.onSceneChangeToMain.RemoveListener(PlayMainSceneSound);
        GameSceneManager.Instance.onSceneChangeToEnd.RemoveListener(PlayEndSound);
        GameSceneManager.Instance.onSceneChangeToGameOver.RemoveListener(PlayGameOverSound);
        GameSceneManager.Instance.onSceneChangeToLore.RemoveListener(PlayLoreSceneSound);
        
        EventManager.Instance.onPlayerCollectHealItem.RemoveListener(PlayHealItemSound);
        EventManager.Instance.onPlayerCollectHealItem.RemoveListener(StopHyponotizedSound);
        EventManager.Instance.onPlayerCollectShieldItem.RemoveListener(PlayShieldItemSound);
        EventManager.Instance.onPlayerCollectProjectileItem.RemoveListener(PlayProjectileItemSound);
        EventManager.Instance.onPlayerShootProjectile.RemoveListener(PlayProjectileShootSound);
        
        EventManager.Instance.onPlayerHit.RemoveListener(PlayHyponotizedSound);
        EventManager.Instance.onPlayerHit.RemoveListener(StopMainSceneSound);
        EventManager.Instance.onPlayerDie.RemoveListener(PlayHyponotizedSound);
        EventManager.Instance.onPlayerDie.RemoveListener(StopMainSceneSound);
        EventManager.Instance.onPlayerHitRangedEnemy.RemoveListener(PlayRangedEnemyHitSound);
    }

    public void PlayHyponotizedSound()
    {
        if (hypnotizedSound != null && !hypnotizedSound.isPlaying)
        {
            hypnotizedSound.Play();
        }
    }

    public void StopHyponotizedSound()
    {
        if (hypnotizedSound != null && hypnotizedSound.isPlaying)
        {
            hypnotizedSound.Stop();
        }
    }

    public void PlayRangedEnemyHitSound()
    {
        if (hitSound != null && !hitSound.isPlaying)
        {
            hitSound.Play();
        }
    }

    public void PlayHealItemSound()
    {
        if (healSound != null && !healSound.isPlaying)
        {
            healSound.Play();
        }
    }

    public void PlayShieldItemSound()
    {
        if (shieldSound != null && !shieldSound.isPlaying)
        {
            shieldSound.Play();
            StartCoroutine(StopShieldSoundAfterDelay());
        }
    }

    public void PlayStartSceneSound()
    {
        StopAllBackgroundSounds();
        if (startSceneSound != null && !startSceneSound.isPlaying && startSceneSound.enabled)
        {
            startSceneSound.loop = true;
            startSceneSound.Play();
        }
    }

    public void PlayMainSceneSound()
    {
        StopAllBackgroundSounds();
        if (mainSceneSound != null && !mainSceneSound.isPlaying)
        {
            mainSceneSound.loop = true;
            mainSceneSound.Play();
        }
    }

    public void StopMainSceneSound()
    {
        if (mainSceneSound != null && mainSceneSound.isPlaying)
        {
            mainSceneSound.Stop();
        }
    }

    public void PlayGameOverSound()
    {
        StopAllBackgroundSounds();
        if (gameOverSound != null && !gameOverSound.isPlaying)
        {
            gameOverSound.loop = true;
            gameOverSound.Play();
        }
    }

    public void PlayEndSound()
    {
        StopAllBackgroundSounds();
        if (endSceneSound != null && !endSceneSound.isPlaying)
        {
            endSceneSound.loop = true;
            endSceneSound.Play();
        }
    }
    
    public void PlayLoreSceneSound()
    {
        StopAllBackgroundSounds();
        if (loreSceneSound != null && !loreSceneSound.isPlaying)
        {
            loreSceneSound.loop = true;
            loreSceneSound.Play();
        }
    }
    
    public void PlayProjectileItemSound()
    {
        if (projectileItemSound != null && !projectileItemSound.isPlaying)
        {
            projectileItemSound.Play();
        }
    }

    public void PlayProjectileShootSound()
    {
        if (projectileShootSound != null && !projectileShootSound.isPlaying)
        {
            projectileShootSound.Play();
        }
    }

    private void StopAllBackgroundSounds()
    {
        if (startSceneSound != null && startSceneSound.isPlaying)
        {
            startSceneSound.Stop();
        }

        if (mainSceneSound != null && mainSceneSound.isPlaying)
        {
            mainSceneSound.Stop();
        }
        
        if(hypnotizedSound != null && hypnotizedSound.isPlaying)
        {
            hypnotizedSound.Stop();
        }

        if (gameOverSound != null && gameOverSound.isPlaying)
        {
            gameOverSound.Stop();
        }
        
        if (loreSceneSound != null && loreSceneSound.isPlaying)
        {
            loreSceneSound.Stop();
        }
    }
    
    private IEnumerator StopShieldSoundAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        shieldSound.Stop();
    }
}