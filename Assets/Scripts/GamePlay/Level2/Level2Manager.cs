using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class Level2Manager : LevelManager
{
	public SpriteRenderer playerSprite;
	public List<Sprite> poseSprites;
	public int curPoseIndex = 0;
	public Image shootImage;
	public float shootTime = 0.3f;
	public AudioClip shootAudioClip;
	Animator animator;
	bool isPosed = false;
	bool isOverlap = false;

	private void Awake()
	{
		animator = playerSprite.GetComponent<Animator>();
	}

	public void SetOverlap(bool overlap)
	{
		isOverlap = overlap;
	}

	public void OnClickPlayer()
	{
		if (!isPosed)
		{
			animator.enabled = true;
			isPosed = true;
			ChangePose(curPoseIndex);
			return;
		}
		else
		{
			animator.enabled = false;
			ChangePose(curPoseIndex + 1);
		}
	}

	public void ChangePose(int index)
	{
		curPoseIndex = index % poseSprites.Count;
		playerSprite.sprite = poseSprites[curPoseIndex];
		if (immediatelyCheckCoroutine != null)
		{
			StopCoroutine(immediatelyCheckCoroutine);
		}
		immediatelyCheckCoroutine = StartCoroutine(DelayInvoke(checkTime, CheckComplete));
	}

	public override void CheckComplete()
	{
		GameManager.Instance.control = false;
		if (!isOverlap || !isPosed)
		{
			StartCoroutine(Shooting(Fail));
			return;
		}
		switch (curPoseIndex)
		{
			case 0:
				//剪刀手
				StartCoroutine(Shooting(Success));
				break;
			case 1:
				//拳头
				StartCoroutine(Shooting(() => {
					GameManager.Instance.SpecialEnd(specialEndSceneNames[0]);
				}));
				break;
			case 2:
				//搞怪
				StartCoroutine(Shooting(() => {
					GameManager.Instance.SpecialEnd(specialEndSceneNames[1]);
				}));
				break;
			case 3:
				//鄙视
				StartCoroutine(Shooting(() => {
					GameManager.Instance.SpecialEnd(specialEndSceneNames[2]);
				}));
				break;
		}
	}

	public void Shoot()
	{
		StartCoroutine(Shooting(Success));
	}

	IEnumerator Shooting(Action onEnd = null)
	{
		audioSource.PlayOneShot(shootAudioClip);
		float t = 0;
		while (t < shootTime)
		{
			t += Time.deltaTime;
			float value = EasingLerps.EasingLerp(EasingLerps.EasingInOutType.EaseOut, EasingLerps.EasingLerpsType.Sine, t / shootTime, 1, 0);
			Color color = shootImage.color;
			color.a = value;
			shootImage.color = color;
			yield return null;
		}
		onEnd?.Invoke();
	}
}
