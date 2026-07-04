using UnityEngine;

public class Level5Manager : LevelManager
{
	public bool isHight = false;
	public bool isLow = false;

	public override void CheckComplete()
	{
		base.CheckComplete();
		GameManager.Instance.control = false;
		if (isHight)
		{
			Debug.Log("Hight");
			Fail();
		}
		else
		{
			Debug.Log("Low");
			Success();
		}
	}

	public override void Success()
	{
		StopAllCoroutines();
		GameManager.Instance.HappyEnd();
	}

	public void Check()
	{
		if (immediatelyCheckCoroutine != null)
		{
			StopCoroutine(immediatelyCheckCoroutine);
		}
		immediatelyCheckCoroutine = StartCoroutine(DelayInvoke(checkTime, CheckComplete));
	}

	public void CancelCheck()
	{
		if (immediatelyCheckCoroutine != null)
		{
			StopCoroutine(immediatelyCheckCoroutine);
			immediatelyCheckCoroutine = null;
		}
	}

	public void EnterHight()
	{
		isHight = true;
		Check();
	}

	public void LeaveHight()
	{
		isHight = false;
		if(!isLow)
		{
			CancelCheck();
		}
	}

	public void EnterLow()
	{
		isLow = true;
		Check();
	}

	public void LeaveLow()
	{
		isLow = false;
		if (!isHight)
		{
			CancelCheck();
		}
	}
}
