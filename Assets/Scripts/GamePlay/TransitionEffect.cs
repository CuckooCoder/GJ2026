using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Utils;
using static UnityEngine.Rendering.DebugUI;

public class TransitionEffect : SingletonMono<TransitionEffect>
{
	public float maxRadius = 2.5f;
	public float fadeDuration = 1f;
	public Image image;
	AudioSource audioSource;

	protected override void Init()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public void FadeIn(bool immediately = false)
	{
		if (immediately)
		{
			image.material.SetFloat("_Radius", maxRadius);
		}
		else
		{
			audioSource.Play();
			StartCoroutine(Fade(0f, maxRadius, fadeDuration));
		}
	}

	public void FadeOut(Action onEnd = null, bool immediately = false)
	{
		if (immediately)
		{
			image.material.SetFloat("_Radius", 0);
		}
		else
		{
			audioSource.Play();
			StartCoroutine(Fade(maxRadius, 0f, fadeDuration, onEnd));
		}
	}

	IEnumerator Fade(float start, float end, float time, Action onEnd = null)
	{
		float t = 0;
		while (t < time)
		{
			t += Time.deltaTime;
			float value = Mathf.Lerp(start, end, Mathf.Clamp01(t / time));
			image.material.SetFloat("_Radius", value);
			yield return null;
		}
		image.material.SetFloat("_Radius", end);
		onEnd?.Invoke();
	}
}
