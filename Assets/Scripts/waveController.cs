using UnityEngine;
using System.Collections;

public class WaveController : MonoBehaviour
{
    public void CompleteWave()
    {
        GameManager.Instance.MarkWaveComplete();
    }

    // Example trigger — replace with animation/timeline trigger as needed
    private IEnumerator Start()
    {
        Debug.Log("🌊 Wave started.");
        yield return new WaitForSeconds(7f); // Placeholder: wave duration
        CompleteWave();
    }
}
