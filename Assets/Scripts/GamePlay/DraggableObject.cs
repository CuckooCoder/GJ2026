using UnityEngine;
using UnityEngine.InputSystem;

public class Draggable : MonoBehaviour
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
				// 计算鼠标与物体中心点偏移，避免拖拽时物体瞬移到鼠标中心
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

	// 外部获取拖拽状态（方便你判断两个Box2D是否重叠）
	public bool IsDragging() => isDragging;
}
