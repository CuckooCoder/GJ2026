using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Manager : LevelManager
{
	public SpriteRenderer playerSpriteRender;
	public Sprite sitSprite;
	public Sprite standSprite;
	public Animator bossAnimator;
	public List<Animator> npcAnimators;
	public GameObject film;
	public bool isPlayerStandUp = false;

	protected override void Start()
	{
		base.Start();
		StartCoroutine(NpcStandUp());
	}

	public override void CheckComplete()
	{
		base.CheckComplete();
		GameManager.Instance.control = false;
		if (!isPlayerStandUp)
		{
			Fail();
		}
		else
		{
			film.SetActive(true);
			StartCoroutine(DelayInvoke(1f, Success));
		}
	}

	public void PlayerSwitchStand()
	{
		if (immediatelyCheckCoroutine != null)
		{
			StopCoroutine(immediatelyCheckCoroutine);
		}
		isPlayerStandUp = !isPlayerStandUp;
		playerSpriteRender.GetComponent<AudioSource>().Play();
		if (isPlayerStandUp)
		{
			playerSpriteRender.sprite = standSprite;
			immediatelyCheckCoroutine = StartCoroutine(DelayInvoke(checkTime, CheckComplete));
		}
		else
		{
			playerSpriteRender.sprite = sitSprite;
		}
	}

	IEnumerator NpcStandUp()
	{
		bossAnimator.enabled = true;
		yield return new WaitForSeconds(0.5f);
		foreach (var animator in npcAnimators)
		{
			StartCoroutine(DelayInvoke(Random.Range(0f, 0.5f), () => { animator.enabled = true;animator.GetComponent<AudioSource>().Play(); }));
		}
	}
}
