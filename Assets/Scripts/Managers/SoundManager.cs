using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioSource hypnotizedSound;

    [SerializeField] private AudioSource hitSound;

    [SerializeField] private AudioSource healSound;

    [SerializeField] private AudioSource shieldSound;

    [SerializeField] private AudioSource startSceneSound;

    [SerializeField] private AudioSource mainSceneSound;

    [SerializeField] private AudioSource gameOverSound;

    [SerializeField] private AudioSource enterKeySound;


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

    public void PlayHyponotizedSound()
    {
        if (hypnotizedSound != null && !hypnotizedSound.isPlaying)
        {
            hypnotizedSound.enabled = true;
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
        CheckAudioSourceIsEnabled(startSceneSound);
        if (startSceneSound != null && !startSceneSound.isPlaying && startSceneSound.enabled)
        {
            Debug.Log("Playing start scene sound");
            startSceneSound.loop = true;
            startSceneSound.Play();
            Debug.Log(startSceneSound.enabled);
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

    public void PlayGameOverSound()
    {
        StopAllBackgroundSounds();
        if (gameOverSound != null && !gameOverSound.isPlaying)
        {
            gameOverSound.loop = true;
            gameOverSound.Play();
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

        if (gameOverSound != null && gameOverSound.isPlaying)
        {
            gameOverSound.Stop();
        }
    }

    public void PlaySceneChangeOnInputSound()
    {
        if (enterKeySound != null && !enterKeySound.isPlaying)
        {
            enterKeySound.Play();
        }
    }

    private IEnumerator StopShieldSoundAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        shieldSound.Stop();
    }

    private void CheckAudioSourceIsEnabled(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            if (!audioSource.gameObject.activeInHierarchy)
            {
                audioSource.gameObject.SetActive(true);
            }

            if (!audioSource.enabled)
            {
                audioSource.enabled = true;
            }
        }
    }
}