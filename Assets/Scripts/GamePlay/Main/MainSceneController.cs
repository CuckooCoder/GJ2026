using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneController : MonoBehaviour
{
	public Button newgameButton;
	public Button continueButton;
	public Button creditButton;
	public Button quitButton;
	public GameObject creditPanel;

	private void Start()
	{
		newgameButton.onClick.AddListener(NewGame);
		continueButton.onClick.AddListener(Continue);
		creditButton.onClick.AddListener(Credit);
		quitButton.onClick.AddListener(Quit);
		TransitionEffect.Instance.FadeIn();
	}

	void NewGame()
	{
		GameManager.Instance.NewGame();
	}

	void Continue()
	{
		GameManager.Instance.Load();
	}

	void Credit()
	{
		creditPanel.SetActive(true);
	}

	void Quit()
	{
		GameManager.Instance.Quit();
	}
}
