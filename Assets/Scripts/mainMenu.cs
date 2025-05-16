using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource audioSource;
    public AudioClip onClickClip;
    public void ClickAndLoad(){
        audioSource.PlayOneShot(onClickClip);
        Invoke("LoadGame", 0.3f);
    }

    void Awake()
    {
        DontDestroyOnLoad(GameObject.Find("EventSystem"));
    }


    public void LoadGame() {
        SceneManager.LoadScene("AileyScene", LoadSceneMode.Additive);

        // Delay StartGame() to ensure the scene is ready
        Invoke(nameof(StartWavesAfterDelay), 0.3f);
    }


    private void StartWavesAfterDelay()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartGame();

            // ✅ Unload the main menu scene so it disappears
            SceneManager.UnloadSceneAsync("MainMenu");
        }
        else
        {
            Debug.LogError("❌ GameManager.Instance is null! Make sure it was loaded.");
        }
    }

    public Animator titleAnimator;

    void Start()
    {
        if (!SceneManager.GetSceneByName("GameManager").isLoaded)
        {
            SceneManager.LoadScene("GameManager", LoadSceneMode.Additive);
        }
        Time.timeScale = 1; // ensure animations are unpaused
        titleAnimator.Play("TitleAnimation", 0, 0f); // replay from start
    }

}
