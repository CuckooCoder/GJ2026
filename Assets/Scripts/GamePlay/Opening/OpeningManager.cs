using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class OpeningManager : MonoBehaviour
{
	public List<VideoClip> videoClips;


	public VideoPlayer videoPlayer;

	// 当前播放索引
	private int currentIndex = 0;
	// 标记：视频是否已经播放完成，等待点击
	private bool waitForClick = false;

	void Start()
	{
		// 绑定播放结束事件
		videoPlayer.loopPointReached += OnVideoFinish;

		if (videoClips.Count > 0)
		{
			PlayVideo(currentIndex);
		}
	}

	// 播放指定索引视频
	void PlayVideo(int index)
	{
		waitForClick = false;
		videoPlayer.clip = videoClips[index];
		videoPlayer.Play();
	}

	// 视频播放完毕回调
	void OnVideoFinish(VideoPlayer vp)
	{
		waitForClick = true;
	}

	void Update()
	{
		// 只有视频播完等待点击时才响应鼠标左键/屏幕点击
		if (waitForClick && Input.GetMouseButtonDown(0))
		{
			NextVideo();
		}
	}

	void NextVideo()
	{
		currentIndex++;

		// 判断是否还有下一个视频
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
