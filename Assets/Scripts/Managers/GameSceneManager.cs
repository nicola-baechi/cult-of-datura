using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance { get; private set; }

    public UnityEvent onSceneChangeToMain;
    public UnityEvent onSceneChangeToStart;
    public UnityEvent onSceneChangeToGameOver;
    public UnityEvent onSceneChangeToEnd;
    public UnityEvent onSceneChangeToLore;

    private readonly string LORE_SCENE = "Lore";
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
        // the delay of 0.1 is for the listeners which are added in Start() -> ugly workaround :(  but works :)
        StartCoroutine(InvokeSceneChangeToStart());
        EventManager.Instance.onPlayerDie.AddListener(LoadGameOverScene);
        EventManager.Instance.onPlayerReachStart.AddListener(LoadGameOverScene);
        EventManager.Instance.onPlayerReachEnd.AddListener(LoadEndScene);
    }
    
    private IEnumerator InvokeSceneChangeToStart()
    {
        yield return new WaitForSeconds(0.1f);
        onSceneChangeToStart.Invoke();
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

        if (currentScene == LORE_SCENE && Input.GetKeyDown(KeyCode.Return))
        {
            LoadMainScene();
        }
        
        if (currentScene == START_SCENE && Input.GetKeyDown(KeyCode.Return))
        {
            LoadLoreScene();
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

    public void LoadLoreScene()
    {
        SceneManager.LoadScene(LORE_SCENE, LoadSceneMode.Single);
        onSceneChangeToLore.Invoke();
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
        StartCoroutine(_loadGameOverScene());
    }

    private IEnumerator _loadGameOverScene()
    {
        
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(GAME_OVER_SCENE, LoadSceneMode.Single);
        onSceneChangeToGameOver.Invoke();
    }
    
    public void LoadEndScene()
    {
        SceneManager.LoadScene(END_SCENE, LoadSceneMode.Single);
        onSceneChangeToEnd.Invoke();
    }
}