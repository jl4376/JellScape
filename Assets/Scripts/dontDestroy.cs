using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class dontDestroy : MonoBehaviour
{  
    private static dontDestroy instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.Log("🗑 Duplicate WinMenu found. Destroying it.");
            Destroy(gameObject);
            return;
        }

        instance = this;
        gameObject.SetActive(false); // Hide on load
        DontDestroyOnLoad(gameObject); // Persist across scenes
    }
    // void Awake()
    // {
    //     gameObject.SetActive(false);
    //     DontDestroyOnLoad(gameObject);
    // }

    public void HideMenu()
    {
        gameObject.SetActive(false);
    }
}
