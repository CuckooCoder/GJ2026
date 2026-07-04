using UnityEngine;
using UnityEngine.Events;

public class ClickSprite : MonoBehaviour
{
	private float pressTime;
	public float clickDurationThreshold = 0.2f;
	public UnityEvent onClick;

	void Update()
	{
		//if (!GameManager.Instance.control)
		//{
		//	return;
		//}
		if (Input.GetMouseButtonDown(0))
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			var hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity);
			foreach (var hit in hits)
			{
				if (hit.collider.transform == transform)
				{
					pressTime = Time.time;
				}
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			var hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity);
			foreach (var hit in hits)
			{
				if (hit.collider.transform == transform)
				{
					float holdTime = Time.time - pressTime;
					if (holdTime < clickDurationThreshold)
					{
						onClick?.Invoke();
					}
				}
			}
		}
	}
}
