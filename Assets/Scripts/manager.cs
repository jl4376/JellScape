using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static int playerCount = 0;

    [Header("Scene Progression")]
    public string[] waveScenes;
    private int currentWaveIndex = 0;

    [Header("Persistence")]
    [Tooltip("Name of the scene whose root objects should never be destroyed (e.g., \"PlayerScene\").")]
    public string sceneToKeep;

    public GameObject gameOverUI;
    public GameObject winUI;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Debug.LogWarning("🟡 Duplicate GameManager detected. Destroying this one.");
            Destroy(gameObject);
        }
    }


    public void StartGame()
    {
        currentWaveIndex = 0;
        LoadCurrentWave();
    }

    public void LoadNextWave()
    {
        DestroyAllExcept(sceneToKeep);
        currentWaveIndex++;
        if (currentWaveIndex >= waveScenes.Length)
        {
            HandleGameWin();
            return;
        }
        LoadCurrentWave();
    }

    private void LoadCurrentWave()
    {
        if (waveScenes == null || waveScenes.Length == 0)
            return;
        if (currentWaveIndex < 0 || currentWaveIndex >= waveScenes.Length)
            return;
        string nextSceneName = waveScenes[currentWaveIndex];
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Additive);
    }

    private void DestroyAllExcept(string keepSceneName)
    {
        Scene gameManagerScene = Instance.gameObject.scene;

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);

            if (scene == gameManagerScene || scene.name == keepSceneName)
                continue;

            GameObject[] roots = scene.GetRootGameObjects();
            foreach (GameObject go in roots)
            {
                Destroy(go);
            }

            SceneManager.UnloadSceneAsync(scene);
        }
    }


    public void MarkWaveComplete()
    {
        LoadNextWave();
    }

    public static void RegisterPlayer()
    {
        playerCount++;
    }

    public static void HandlePlayerDeath()
    {
        playerCount--;
        if (playerCount <= 0)
        {
            Time.timeScale = 0f;
            if (Instance != null && Instance.gameOverUI != null)
            {
                // ✅ Unlock and show cursor for UI interaction
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                Instance.gameOverUI.SetActive(true); // Show Game Over UI
            }
        }
    }

    private void HandleGameWin()
    {
        Time.timeScale = 0f;

        // Show win screen
        if (winUI != null)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            winUI.SetActive(true);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Rebind UI references only if they’re missing
        if (gameOverUI == null)
            gameOverUI = GameObject.Find("GameOverMenu");

        if (winUI == null)
            winUI = GameObject.Find("WinMenu");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


}