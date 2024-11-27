using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance { get; private set; }

    public UnityEvent onSceneChangeToMain;
    public UnityEvent onSceneChangeToStart;
    public UnityEvent onSceneChangeToGameOver;

    private readonly string START_SCENE = "Start";
    private readonly string MAIN_SCENE = "Main";
    private readonly string GAME_OVER_SCENE = "GameOver";
    private readonly string END_SCENE = "End";

    public void Awake()
    {
        if (Instance && Instance != this)
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
        // NOTE: has to be invoked here because if it was invoked in Awake, it would be invoked before the listeners are added
        onSceneChangeToStart.Invoke();
        EventManager.Instance.onPlayerDie.AddListener(LoadGameOverScene);
        EventManager.Instance.onPlayerReachStart.AddListener(LoadGameOverScene);
        EventManager.Instance.onPlayerReachEnd.AddListener(LoadEndScene);
    }

    private void OnDisable()
    {
        EventManager.Instance.onPlayerDie.RemoveListener(LoadGameOverScene);
        EventManager.Instance.onPlayerReachStart.RemoveListener(LoadGameOverScene);
        EventManager.Instance.onPlayerReachEnd.RemoveListener(LoadEndScene);
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
    
    public void LoadEndScene()
    {
        SceneManager.LoadScene(END_SCENE, LoadSceneMode.Single);
    }
}