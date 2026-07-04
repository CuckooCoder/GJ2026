using System;
using UnityEngine;

public class DraggableSprite : MonoBehaviour
{
	private bool isDragging = false;
	private Vector2 offset;

	void Update()
	{
		// 鼠标按下开始拖拽
		if (Input.GetMouseButtonDown(0))
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			var hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

			if (hit.collider != null && hit.collider.transform == transform)
			{
				isDragging = true;
				offset = (Vector2)transform.position - hit.point;
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
