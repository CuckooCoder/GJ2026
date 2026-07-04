using System;
using UnityEngine;

public class ClickSprite : MonoBehaviour
{
	private float pressTime;
	public float clickDurationThreshold = 0.2f;
	public Action onClick;

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			var hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
			if (hit.collider != null && hit.collider.transform == transform)
			{
				pressTime = Time.time;
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			float holdTime = Time.time - pressTime;
			if (holdTime < clickDurationThreshold)
			{
				onClick?.Invoke();
			}
		}
	}
}
