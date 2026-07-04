using System;
using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public float totalTime = 10;
	public float checkTime = 3;
	protected Coroutine immediatelyCheckCoroutine;


	private void Start()
	{
		TransitionEffect.Instance.FadeIn();
		StartCoroutine(DelayInvoke(totalTime, CheckComplete));
	}

	public void Reload()
	{
		GameManager.Instance.LoadScene(GameManager.Instance.curLevelIndex);
	}

	public void LoadNextLevel()
	{
		GameManager.Instance.LoadScene(GameManager.Instance.curLevelIndex + 1);
	}

	public virtual void CheckComplete()
	{
		StopAllCoroutines();
	}

	public virtual void Success()
	{
		StopAllCoroutines();
		LoadNextLevel();
	}

	public virtual void Fail()
	{
		StopAllCoroutines();
	}

	public virtual IEnumerator DelayInvoke(float delay, Action action)
	{
		yield return new WaitForSeconds(delay);
		action?.Invoke();
	}
}
