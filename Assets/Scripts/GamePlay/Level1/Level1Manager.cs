using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : LevelManager
{
	public SpriteRenderer playerSprite;
	public List<Sprite> emoSprites;
	public int curEmoIndex = 0;

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

	public override void CheckComplete()
	{
		base.CheckComplete();
		GameManager.Instance.control = false;
		switch (curEmoIndex)
		{
			case 0:
				//无表情
				Fail();
				break;
			case 1:
				//笑
				StopAllCoroutines();
				GameManager.Instance.SpecialEnd(specialEndSceneNames[0]);
				break;
			case 2:
				//哭
				Success();
				break;
		}
	}
}
