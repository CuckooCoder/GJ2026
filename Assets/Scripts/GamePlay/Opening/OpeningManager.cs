using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class OpeningManager : MonoBehaviour
{
    public List<VideoClip> videoClips;
    public List<string> lines;
    public TMP_Text textUI;

    public VideoPlayer videoPlayer;

    private int currentIndex = 0;

    void Start()
    {
        TransitionEffect.Instance.FadeIn();

        videoPlayer.loopPointReached += OnVideoFinish;

        if (videoClips.Count > 0)
        {
            PlayVideo(currentIndex);
        }
    }

    void PlayVideo(int index)
    {
        videoPlayer.clip = videoClips[index];
        videoPlayer.Play();

        if (index < lines.Count)
            textUI.text = lines[index];
    }

    void OnVideoFinish(VideoPlayer vp)
    {
        currentIndex++;

        if (currentIndex < videoClips.Count)
        {
            PlayVideo(currentIndex);
        }
        else
        {
            Debug.Log("所有视频播放完毕");
            GameManager.Instance.LoadScene(GameManager.Instance.curLevelIndex + 1);
        }
    }
}