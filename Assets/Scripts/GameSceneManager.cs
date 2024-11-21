using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    private readonly string START_SCENE = "Start";
    private readonly string MAIN_SCENE = "Main";
    private readonly string GAME_OVER_SCENE = "GameOver";

    private void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        
        if (currentScene == START_SCENE && Input.GetKeyDown(KeyCode.Return))
        {
            LoadMainScene();
        }

        if (currentScene == GAME_OVER_SCENE)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                LoadStartScene();
            }
            
            else if (Input.GetKeyDown(KeyCode.R))
            {
                LoadMainScene();
            }
        }
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(START_SCENE, LoadSceneMode.Single);
    }
    
    public void LoadMainScene()
    {
        SceneManager.LoadScene(MAIN_SCENE, LoadSceneMode.Single);
    }

    public void LoadGameOverScene()
    {
        SceneManager.LoadScene(GAME_OVER_SCENE, LoadSceneMode.Single);
    }
}