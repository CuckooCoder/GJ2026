using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class GameManager : SingletonMono<GameManager>
{
	public string mainSceneName = "Main";
	public List<string> levels;
	int curLevelIndex = 0;

	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	public void LoadScene(int sceneIndex)
	{
		curLevelIndex = sceneIndex;
		Save();
		SceneManager.LoadScene(levels[sceneIndex]);
	}

	public void NewGame()
	{
		SceneManager.LoadScene(0);
	}

	public void Load()
	{
		LoadScene(PlayerPrefs.GetInt("LevelIndex"));
	}

	public void Save()
	{
		PlayerPrefs.SetInt("LevelIndex", curLevelIndex);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
