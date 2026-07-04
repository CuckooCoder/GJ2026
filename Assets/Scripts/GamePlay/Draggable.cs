using System;
using UnityEngine;

public class Draggable : MonoBehaviour
{
	private bool isDragging = false;
	private Vector2 offset;
	public bool lockX;
	public bool lockY;

	void Update()
	{
		// 鼠标按下开始拖拽
		if (Input.GetMouseButtonDown(0))
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			var hits = Physics2D.RaycastAll(ray.origin, ray.direction, Mathf.Infinity);
			foreach (var hit in hits)
			{
				if (hit.collider.transform == transform)
				{
					isDragging = true;
					offset = (Vector2)transform.position - hit.point;
					if (lockX)
					{
						offset.x = 0;
					}
					if (lockY)
					{
						offset.y = 0;
					}
				}
			}
		}

		// 拖拽中实时更新位置
		if (isDragging && Input.GetMouseButton(0))
		{
			Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			transform.position = mouseWorldPos + offset;
		}

		// 鼠标松开结束拖拽
		if (Input.GetMouseButtonUp(0))
		{
			isDragging = false;
		}
	}
}
