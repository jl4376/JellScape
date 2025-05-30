using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Scene Progression")]
    public string[] waveScenes; // Assign in Inspector: e.g., ["Wave1", "Wave2"]
    private int currentWaveIndex = 0;

    private void Awake()
    {
        Debug.Log("🟢 GameManager Awake");

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist through scene loads
            Debug.Log("✅ GameManager set as Singleton");
        }
        else
        {
            Debug.LogWarning("⚠️ Duplicate GameManager detected — destroying this one.");
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        Debug.Log("🎮 StartGame() called");
        currentWaveIndex = 0;
        LoadCurrentWave();
    }

    public void LoadNextWave()
    {
        currentWaveIndex++;
        Debug.Log($"➡️ LoadNextWave() called — advancing to index {currentWaveIndex}");

        if (currentWaveIndex >= waveScenes.Length)
        {
            Debug.Log("🏁 All waves complete! No more scenes to load.");
            // TODO: Show win screen or return to menu
            // Pop up a canvas here...
            return;
        }

        LoadCurrentWave();
    }

    private void LoadCurrentWave()
    {
        if (waveScenes == null || waveScenes.Length == 0)
        {
            Debug.LogError("❌ waveScenes array is empty! Check the Inspector.");
            return;
        }

        if (currentWaveIndex < 0 || currentWaveIndex >= waveScenes.Length)
        {
            Debug.LogError($"❌ Invalid wave index: {currentWaveIndex}");
            return;
        }

        string nextScene = waveScenes[currentWaveIndex];
        Debug.Log($"🔁 Loading wave scene: {nextScene} (Index: {currentWaveIndex})");
        SceneManager.LoadScene(nextScene, LoadSceneMode.Additive);
    }

    public void MarkWaveComplete()
    {
        Debug.Log("✅ MarkWaveComplete() called — wave ended.");
        LoadNextWave();
    }
}
