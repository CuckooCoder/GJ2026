using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
	AudioSource audioSource;
	public List<AudioClip> audioClips;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (!audioSource.isPlaying)
		{
			int randomIndex = Random.Range(0, audioClips.Count);
			AudioClip randomClip = audioClips[randomIndex];
			audioSource.PlayOneShot(randomClip);
		}
	}
}
