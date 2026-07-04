using UnityEngine;

public class FollowMouse : MonoBehaviour
{

	void Update()
	{
		transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}
}
