using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class GameManager : SingletonMono<GameManager>
{
	public string mainSceneName = "Main";
	public string happyEndSceneName = "HE";
	public string badEndSceneName = "BE";
	public List<string> levels;
	public int curLevelIndex = 0;
	public bool control = true;

	public void LoadScene(int sceneIndex)
	{
		if (sceneIndex >= 0 && sceneIndex < levels.Count)
		{
			curLevelIndex = sceneIndex;
			Save();
			TransitionEffect.Instance.FadeOut(() => SceneManager.LoadScene(levels[sceneIndex]));
		}
		else
		{

			TransitionEffect.Instance.FadeOut(() => SceneManager.LoadScene(mainSceneName));
		}
	}

	public void ReturnMain()
	{
		TransitionEffect.Instance.FadeOut(() => SceneManager.LoadScene(mainSceneName));
	}

	public void NewGame()
	{
		LoadScene(0);
	}

	public void Load()
	{
		LoadScene(PlayerPrefs.GetInt("LevelIndex"));
	}

	public void Save()
	{
		PlayerPrefs.SetInt("LevelIndex", curLevelIndex);
	}

	public void HappyEnd()
	{
		TransitionEffect.Instance.FadeOut(() => SceneManager.LoadScene(happyEndSceneName));
	}

	public void BadEnd()
	{
		TransitionEffect.Instance.FadeOut(() => SceneManager.LoadScene(badEndSceneName));
	}

	public void Quit()
	{
		Application.Quit();
	}
}
