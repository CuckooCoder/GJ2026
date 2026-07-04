using System;
using UnityEngine;

public class OverlapTrigger : MonoBehaviour
{
	public Collider2D targetTrigger;
	public bool isOverlapping;
	public Action onEnter;
	public Action onExit;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision == targetTrigger)
		{
			isOverlapping = true;
			onEnter?.Invoke();
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision == targetTrigger)
		{
			isOverlapping = false;
			onExit?.Invoke();
		}
	}
}
