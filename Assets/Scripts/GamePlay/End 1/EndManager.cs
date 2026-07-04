using UnityEngine;

public class EndManager : MonoBehaviour
{
	private void Start()
	{
		TransitionEffect.Instance.FadeIn();
	}

	public void ReturnMain()
	{
		GameManager.Instance.ReturnMain();
	}

	public void ReturnLastLevel()
	{
		GameManager.Instance.LoadScene(GameManager.Instance.curLevelIndex);
	}
}
