using UnityEngine;

public class Cup : MonoBehaviour
{
	AudioSource audioSource;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (!audioSource.isPlaying)
		{
			audioSource.Play();
		}
	}
}
