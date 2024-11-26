using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance { get; private set; }

    [SerializeField] private UnityEvent onSceneChangeToMain;
    [SerializeField] private UnityEvent onSceneChangeToStart;
    [SerializeField] private UnityEvent onSceneChangeToGameOver;

    private readonly string START_SCENE = "Start";
    private readonly string MAIN_SCENE = "Main";
    private readonly string GAME_OVER_SCENE = "GameOver";

    public void Awake()
    {
        if (Instance != null && Instance != this)
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
        onSceneChangeToStart.Invoke();
    }

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
        onSceneChangeToStart.Invoke();
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(MAIN_SCENE, LoadSceneMode.Single);
        onSceneChangeToMain.Invoke();
    }

    public void LoadGameOverScene()
    {
        Invoke(nameof(_loadGameOverScene), 3);
    }

    private void _loadGameOverScene()
    {
        SceneManager.LoadScene(GAME_OVER_SCENE, LoadSceneMode.Single);
        onSceneChangeToGameOver.Invoke();
    }
}