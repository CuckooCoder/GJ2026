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
	public List<AudioClip> levelBgms;
	public AudioClip mainBgm;
	public int curLevelIndex = 0;
	public bool control = true;
	public AudioSource audioSource;
	public GameObject pausePanel;

	protected override void Init()
	{
		audioSource = GetComponent<AudioSource>();
	}

	private void Start()
	{
		PlayBgm(mainBgm);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SetPause(Time.timeScale == 1);
		}
	}

	public void PlayBgm(AudioClip clip)
	{
		audioSource.clip = clip;
		audioSource.Play();
	}

	public void LoadScene(int sceneIndex)
	{
		if (sceneIndex >= 0 && sceneIndex < levels.Count)
		{
			curLevelIndex = sceneIndex;
			Save();
			TransitionEffect.Instance.FadeOut(() => { SceneManager.LoadScene(levels[sceneIndex]); PlayBgm(levelBgms[sceneIndex]); });
		}
		else
		{

			TransitionEffect.Instance.FadeOut(() => { SceneManager.LoadScene(mainSceneName); PlayBgm(mainBgm); });
		}
	}

	public void ReturnMain()
	{
		TransitionEffect.Instance.FadeOut(() => { SceneManager.LoadScene(mainSceneName); PlayBgm(mainBgm); });
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
		TransitionEffect.Instance.FadeOut(() => { SceneManager.LoadScene(happyEndSceneName); audioSource.Stop(); });
	}

	public void BadEnd()
	{
		TransitionEffect.Instance.FadeOut(() => { SceneManager.LoadScene(badEndSceneName); audioSource.Stop(); });
	}

	public void SetPause(bool pause)
	{
		Time.timeScale = pause ? 0 : 1;
		pausePanel.SetActive(pause);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
