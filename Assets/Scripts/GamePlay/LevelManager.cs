using System;
using System.Collections;
using UnityEditor.Build.Content;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public float totalTime = 10;
	public float checkTime = 3;

	private void Start()
	{
		StartCoroutine(DelayInvoke(totalTime, CheckComplete));
	}

	protected void Reload()
	{
		GameManager.Instance.LoadScene(GameManager.Instance.curLevelIndex);
	}

	protected void LoadNextLevel()
	{
		GameManager.Instance.LoadScene(GameManager.Instance.curLevelIndex + 1);
	}

	protected virtual void CheckComplete()
	{

	}

	protected virtual IEnumerator DelayInvoke(float delay, Action action)
	{
		yield return new WaitForSeconds(delay);
		action?.Invoke();
	}
}
