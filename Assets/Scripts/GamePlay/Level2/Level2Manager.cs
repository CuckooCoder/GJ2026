using System.Collections.Generic;
using UnityEngine;

public class Level2Manager : LevelManager
{
	public SpriteRenderer playerSprite;
	public List<Sprite> poseSprites;
	public int curPoseIndex = 0;
	public GameObject endPanel;
	public GameObject end1Panell;
	public GameObject end1Panel2;
	public GameObject end1Panel3;
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
			return;
		}
		else
		{
			animator.enabled = false;
			ChangePose();
		}
	}

	public void ChangePose()
	{
		curPoseIndex = (curPoseIndex + 1) % poseSprites.Count;
		playerSprite.sprite = poseSprites[curPoseIndex];
		if (immediatelyCheckCoroutine != null)
		{
			StopCoroutine(immediatelyCheckCoroutine);
		}
		immediatelyCheckCoroutine = StartCoroutine(DelayInvoke(checkTime, CheckComplete));
	}

	public override void CheckComplete()
	{
		if (!isOverlap || !isPosed)
		{
			Fail();
			return;
		}
		switch (curPoseIndex)
		{
			case 0:
				//剪刀手
				Success();
				break;
			case 1:
				//拳头
				endPanel.SetActive(true);
				end1Panell.SetActive(true);
				break;
			case 2:
				//搞怪
				endPanel.SetActive(true);
				end1Panel2.SetActive(true);
				break;
			case 3:
				//鄙视
				endPanel.SetActive(true);
				end1Panel3.SetActive(true);
				break;
		}
	}

	public override void Fail()
	{
		base.Fail();
	}
}
