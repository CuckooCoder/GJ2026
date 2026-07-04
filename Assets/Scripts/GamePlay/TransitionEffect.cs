using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class TransitionEffect : SingletonMono<TransitionEffect>
{
	public float maxRadius = 2.5f;
	public float fadeDuration = 1f;
	public Image image;

	private void Start()
	{
		FadeIn();
	}

	public void FadeIn()
	{
		StartCoroutine(Fade(0f, maxRadius, fadeDuration));
	}

	public void FadeOut()
	{
		StartCoroutine(Fade(maxRadius, 0f, fadeDuration));
	}

	IEnumerator Fade(float start, float end, float time)
	{
		float t = 0;
		while (t < time)
		{
			t += Time.deltaTime;
			float value = EasingLerps.EasingLerp(EasingLerps.EasingInOutType.EaseIn, EasingLerps.EasingLerpsType.Sine, t / time, start, end);
			image.material.SetFloat("_Radius", value);
			yield return null;
		}
	}
}
