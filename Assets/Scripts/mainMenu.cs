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

    public void LoadGame() {
        SceneManager.LoadScene("Game");
    }
    public Animator titleAnimator;

    void Start()
    {
        Time.timeScale = 1; // ensure animations are unpaused
        titleAnimator.Play("TitleAnimation", 0, 0f); // replay from start
    }

}
