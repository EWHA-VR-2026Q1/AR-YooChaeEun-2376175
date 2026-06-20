using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Video;

public class TriggerTimeline : MonoBehaviour
{
    public PlayableDirector director;
    public VideoPlayer videoPlayer;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            videoPlayer.Play();   // 영상 재생
            director.Play();      // 조명, 사운드, 큐브 생성
        }
    }
}