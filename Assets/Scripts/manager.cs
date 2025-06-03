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
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
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
            return;
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
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name == "DontDestroyOnLoad" || scene.name == keepSceneName)
                continue;

            GameObject[] roots = scene.GetRootGameObjects();
            foreach (GameObject go in roots)
            {
                if (go == this.gameObject)
                    continue;
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

}
