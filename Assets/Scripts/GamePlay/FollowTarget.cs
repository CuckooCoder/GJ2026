using UnityEngine;

public class FollowTarget : MonoBehaviour
{
	public Transform target;
	public float speed = 2f;
	Rigidbody2D rig;

	private void Awake()
	{
		rig = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		Vector2 targetPos = target.position;

		Vector2 delta = targetPos - rig.position;
		Vector2 moveDir = delta.normalized;
		float stopThreshold = speed * Time.fixedDeltaTime * 2;
		if (delta.magnitude < stopThreshold)
		{
			rig.linearVelocity = Vector2.zero;
			return;
		}
		rig.linearVelocity = moveDir * speed;
	}
}
