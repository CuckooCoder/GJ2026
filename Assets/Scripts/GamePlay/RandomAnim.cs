using System;
using System.Collections;
using UnityEngine;

public class RandomAnim : MonoBehaviour
{
	public float maxTime = 0.5f;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		Animator animator = GetComponent<Animator>();
		StartCoroutine(DelayInvoke(UnityEngine.Random.Range(0, maxTime), () => { animator.enabled = true; }));
	}

	protected virtual IEnumerator DelayInvoke(float delay, Action action)
	{
		yield return new WaitForSeconds(delay);
		action?.Invoke();
	}
}
