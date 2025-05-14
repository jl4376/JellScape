using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Scene Progression")]
    public string[] waveScenes; // Assign in the Inspector: e.g., ["Wave1", "Wave2", "BossWave"]
    private int currentWaveIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
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
        currentWaveIndex++;

        if (currentWaveIndex >= waveScenes.Length)
        {
            Debug.Log("✅ All waves complete.");
            // TODO: Handle win/game over later
            return;
        }

        LoadCurrentWave();
    }

    private void LoadCurrentWave()
    {
        string nextScene = waveScenes[currentWaveIndex];
        Debug.Log($"🔁 Loading wave scene: {nextScene}");
        SceneManager.LoadScene(nextScene);
    }

    public void MarkWaveComplete()
    {
        Debug.Log("✅ Wave complete signal received.");
        LoadNextWave();
    }
}
