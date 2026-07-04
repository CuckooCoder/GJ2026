using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
	public GameObject player;

	void Update()
	{
		if (!GameManager.Instance.control)
		{
			return;
		}
		if (Input.GetMouseButtonUp(0))
		{
			transform.position = player.transform.position;
		}
	}
}
