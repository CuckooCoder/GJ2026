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
	public AudioClip HEBgm;
	public AudioClip BEBgm;
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

	public void PlayBgm(AudioClip clip, bool loop = true)
	{
		if (clip != null)
		{
			audioSource.clip = clip;
			audioSource.loop = loop;
			audioSource.Play();
		}
		else
		{
			audioSource.Stop();
		}
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
		TransitionEffect.Instance.FadeOut(() => { SceneManager.LoadScene(happyEndSceneName); PlayBgm(HEBgm, false); });
	}

	public void BadEnd()
	{
		TransitionEffect.Instance.FadeOut(() => { SceneManager.LoadScene(badEndSceneName); PlayBgm(BEBgm, false); });
	}

	public void SpecialEnd(string sceneName)
	{
		TransitionEffect.Instance.FadeOut(() => { SceneManager.LoadScene(sceneName); PlayBgm(HEBgm, false); });
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
