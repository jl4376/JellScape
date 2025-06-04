using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject pauseMenu;
    public GameObject confirmMenu;

    [Header("Audio")]
    public AudioClip onClickClip;
    public AudioSource audioSource;

    [Header("Main Menu Scene Name")]
    public string mainMenuSceneName = "MainMenu"; // Assign in Inspector

    private bool isPaused = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isPaused)
                Pause();
            else
                Resume();
        }
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;

        if (pauseMenu != null)
            pauseMenu.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;

        if (pauseMenu != null)
            pauseMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;

        // Optional: Only if you define this method in GameManager
        // GameManager.Instance?.ResumeGame();

        foreach (var rb in Object.FindObjectsByType<Rigidbody2D>(FindObjectsSortMode.None))
        {
            rb.linearVelocity = Vector2.zero;
        }
    }


    public void ClickAndLoadMainMenu()
    {
        PlayClickSound();
        Time.timeScale = 1f;
        Invoke(nameof(LoadMainMenu), 0.3f);
    }

    private void LoadMainMenu()
    {
        Time.timeScale = 1f;

        if (GameManager.Instance != null)
        {
            GameManager.playerCount = 0;
            Destroy(GameManager.Instance.gameObject);
            GameManager.Instance = null;
        }

        // ✅ Destroy this PauseMenu GameObject (since it’s DontDestroyOnLoad)
        Destroy(gameObject);

        // ✅ Optionally destroy persistent EventSystem if you have one
        var eventSystem = GameObject.Find("EventSystem");
        if (eventSystem != null && eventSystem.scene.name == "DontDestroyOnLoad")
        {
            Destroy(eventSystem);
        }

        SceneManager.LoadScene(mainMenuSceneName);
    }


    public void GoToConfirmMenu()
    {
        if (pauseMenu != null) pauseMenu.SetActive(false);
        if (confirmMenu != null) confirmMenu.SetActive(true);
    }

    public void BackToPauseMenu()
    {
        if (confirmMenu != null) confirmMenu.SetActive(false);
        if (pauseMenu != null) pauseMenu.SetActive(true);
    }

    public void PlayClickSound()
    {
        if (audioSource != null && onClickClip != null)
            audioSource.PlayOneShot(onClickClip);
    }
}
