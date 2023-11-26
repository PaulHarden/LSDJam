using System.Collections;
using UnityEngine;

namespace Audio
{
	public class AudioController : MonoBehaviour
	{
		public static AudioController Singleton { get; set; }
		[SerializeField] private AudioSource effectsSource;
		[SerializeField] private AudioSource musicSource;
		public float minPitch, maxPitch;
		private float _masterVolume;
		private float _musicVolume;
		private float _effectVolume;

		private void Awake()
		{
			if (Singleton != null && Singleton != this)
				Destroy(gameObject);
			else
				Singleton = this;
			DontDestroyOnLoad(this);
		}

		public void PlaySound(AudioClip clip, float vol)
		{
			effectsSource.pitch = 1f;
			effectsSource.PlayOneShot(clip);
		}

		public void PlayRandomSound(AudioClip clip, float vol)
		{
			float randomPitch = Random.Range(minPitch, maxPitch);
			effectsSource.pitch = randomPitch;
			effectsSource.PlayOneShot(clip);
		}
	
		public void StopSound() => effectsSource.Stop();

		public void PlayMusic(AudioClip clip)
		{
			musicSource.clip = clip;
			StartCoroutine(FadeIn(2f));
		}

		public void StopMusic() => StartCoroutine(FadeOut(2f));

		public IEnumerator FadeIn(float duration)
		{
			float t = 0;
			musicSource.volume = 0f;
			musicSource.Play();
			while (t < duration)
			{
				t += Time.deltaTime;
				musicSource.volume = Mathf.Lerp(0f, _masterVolume * _musicVolume, t / duration);
				yield return null;
			}
			musicSource.volume = _masterVolume * _musicVolume;
			yield return null;
		}

		public IEnumerator FadeOut(float duration)
		{
			float currentTime = 0;
			float startVol = musicSource.volume;
			while (currentTime < duration)
			{
				currentTime += Time.deltaTime;
				musicSource.volume = Mathf.Lerp(startVol, 0f, currentTime / duration);
				if (musicSource.volume <= 0)
					musicSource.Stop();
				yield return null;
			}
			yield return null;
		}
	}
}