using UnityEngine;

public class Level4Manager : LevelManager
{
	public override void CheckComplete()
	{
		base.CheckComplete();
		GameManager.Instance.control = false;
	}
}
