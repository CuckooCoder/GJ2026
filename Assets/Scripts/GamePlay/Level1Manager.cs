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
	}
}
