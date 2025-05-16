using UnityEngine;
using UnityEngine.Playables;

public class WaveController : MonoBehaviour
{
    public PlayableDirector[] timelineDirectors;

    private int timelinesRemaining;

    private void Start()
    {
        if (timelineDirectors == null || timelineDirectors.Length == 0)
        {
            Debug.LogWarning("⚠️ No timelines assigned to WaveController.");
            return;
        }

        timelinesRemaining = timelineDirectors.Length;

        foreach (var director in timelineDirectors)
        {
            director.stopped += OnTimelineStopped;
        }

        Debug.Log($"🎬 Wave started. Waiting for {timelinesRemaining} timelines.");
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        timelinesRemaining--;
        Debug.Log($"✅ Timeline ended. Remaining: {timelinesRemaining}");

        if (timelinesRemaining <= 0)
        {
            Debug.Log("🌊 All timelines finished. Completing wave.");
            CompleteWave();
        }
    }

    public void CompleteWave()
    {
        GameManager.Instance.MarkWaveComplete();
    }
}
