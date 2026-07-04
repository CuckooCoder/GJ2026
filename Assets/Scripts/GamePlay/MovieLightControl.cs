using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.Rendering.DebugUI;

public class MovieLightControl : MonoBehaviour
{
	private Light2D screenLight;
	public float maxIntensity = 1.0f;
	public float intensitySpeed = 0.3f;
	public float intensitySpeed2 = 1;
	public float redSpeed = 0.18f;
	public float greenSpeed = 0.22f;
	public float blueSpeed = 0.2f;

	private void Awake()
	{
		screenLight = GetComponent<Light2D>();
	}

	void Update()
	{
		float noise = Mathf.PerlinNoise1D(Time.time * intensitySpeed) * Mathf.PerlinNoise1D(Time.time * intensitySpeed2);
		screenLight.intensity = noise * maxIntensity;

		// 三个通道独立噪声
		float r = Mathf.PerlinNoise1D(Time.time * redSpeed);
		float g = Mathf.PerlinNoise1D(Time.time * greenSpeed);
		float b = Mathf.PerlinNoise1D(Time.time * blueSpeed);

		r = Mathf.Lerp(0.8f, 1.3f, r);
		g = Mathf.Lerp(0.8f, 1.3f, g);
		b = Mathf.Lerp(0.8f, 1.3f, b);
	}
}
