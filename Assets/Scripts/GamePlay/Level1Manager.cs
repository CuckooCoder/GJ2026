using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : LevelManager
{
	public SpriteRenderer playerSprite;
	public List<Sprite> emoSprites;
	public int curEmoIndex = 0;
	Coroutine immediatelyCheckCoroutine;

	public void ChangeEmo()
	{
		curEmoIndex = (curEmoIndex + 1) % emoSprites.Count;
		playerSprite.sprite = emoSprites[curEmoIndex];
		if (immediatelyCheckCoroutine != null)
		{
			StopCoroutine(immediatelyCheckCoroutine);
		}
		immediatelyCheckCoroutine = StartCoroutine(DelayInvoke(checkTime, CheckComplete));
	}

	protected override void CheckComplete()
	{
		base.CheckComplete();
		switch (curEmoIndex)
		{
			case 0:
				//无表情
				Debug.Log("无表情");
				break;
			case 1:
				//笑
				Debug.Log("笑");
				break;
			case 2:
				//哭
				Debug.Log("哭");
				break;
		}
	}

	protected override void Fail()
	{
		base.Fail();
	}
}
