using UnityEngine;

public class FollowMouse : MonoBehaviour
{
	public float speed = 5f;
	public BoxCollider2D region;
	Rigidbody2D rig;

	private void Awake()
	{
		rig = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		Bounds bound = region.bounds;
		//Vector2 clampedTarget;
		//clampedTarget.x = Mathf.Clamp(mouseWorld.x, bound.min.x, bound.max.x);
		//clampedTarget.y = Mathf.Clamp(mouseWorld.y, bound.min.y, bound.max.y);

		Vector2 delta = mouseWorld - rig.position;
		Vector2 moveDir = delta.normalized;
		float stopThreshold = speed * Time.deltaTime;
		if (delta.magnitude < stopThreshold || !bound.Contains(rig.position + moveDir * speed * Time.deltaTime))
		{
			rig.linearVelocity = Vector2.zero;
			return;
		}
		rig.linearVelocity = moveDir * speed;
	}
}