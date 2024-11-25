using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance { get; private set; }

    public AudioSource enterKeySound;
    public AudioSource startScreenSound;
    public AudioSource gameOverSound;
    public AudioSource mainSceneSound;
    
    private readonly string START_SCENE = "Start";
    private readonly string MAIN_SCENE = "Main";
    private readonly string GAME_OVER_SCENE = "GameOver";

    public void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(START_SCENE, LoadSceneMode.Single);
    }

    private void Start()
    {
        PlayStartScreenSound();
    }
    
    private void PlayStartScreenSound()
    {
        if (startScreenSound != null && !startScreenSound.isPlaying)
        {
            startScreenSound.loop = true;
            startScreenSound.Play();
        }
    }
    
    private void PlayMainSceneSound()
    {
        if (mainSceneSound != null && !mainSceneSound.isPlaying)
        {
            mainSceneSound.loop = true;
            mainSceneSound.Play();
        }
    }
    
    private void StopMainSceneSound()
    {
        if (mainSceneSound != null && mainSceneSound.isPlaying)
        {
            mainSceneSound.Stop();
        }
    }
    
    private void PlayGameOverSound()
    {
        if (gameOverSound != null)
        {
            Debug.Log("Game Over Sound is assigned");
            if (!gameOverSound.isPlaying)
            {
                Debug.Log("Playing Game Over Sound");
                gameOverSound.Play();
            }
            else
            {
                Debug.Log("Game Over Sound is already playing");
            }
        }
        else
        {
            Debug.Log("Game Over Sound is not assigned");
        }
    }
    
    private void PlayEnterKeySound()
    {
        if (enterKeySound != null && !enterKeySound.isPlaying)
        {
            enterKeySound.Play();
        }
    }
    
    private IEnumerator HandleEnterKey()
    {
        enterKeySound.Play();
        yield return new WaitForSeconds(enterKeySound.clip.length); //Wait for the sound to finish playing

        if (startScreenSound != null && startScreenSound.isPlaying)
        {
            startScreenSound.Stop();
        }
        LoadMainScene();
    }

    private void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        
        if (currentScene == START_SCENE && Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(HandleEnterKey());
        }

        if (currentScene == GAME_OVER_SCENE)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayEnterKeySound();
                LoadStartScene();
            }
            
            else if (Input.GetKeyDown(KeyCode.R))
            {
                PlayEnterKeySound();
                LoadMainScene();
            }
        }
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(START_SCENE, LoadSceneMode.Single);
        PlayStartScreenSound();
        StopMainSceneSound();
    }
    
    public void LoadMainScene()
    {
        SceneManager.LoadScene(MAIN_SCENE, LoadSceneMode.Single);
        PlayMainSceneSound();
    }

    public void LoadGameOverScene()
    {
        Debug.Log("LoadGameOverScene called");
        if (gameOverSound == null)
        {
            Debug.Log("Game Over Sound is not assigned before scene transition");
        }
        Invoke(nameof(_loadGameOverScene), 3);
        PlayGameOverSound();
        StopMainSceneSound();
    }
    
    private void _loadGameOverScene()
    {
        Debug.Log("_loadGameOverScene called");
        if (gameOverSound == null)
        {
            Debug.Log("Game Over Sound is not assigned after scene transition");
        }
        SceneManager.LoadScene(GAME_OVER_SCENE, LoadSceneMode.Single);
        PlayGameOverSound();
        StopMainSceneSound();
    }
}