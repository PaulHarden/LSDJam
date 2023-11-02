using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
	public static AudioController Singleton { get; set; }
	[SerializeField] private AudioSource _effectsSource;
	[SerializeField] private AudioSource _musicSource;
	[field: SerializeField, Tooltip("The lowest pitch variation a single sound can be played with.")]
	public float MinPitch { get; private set; }
	[field: SerializeField, Tooltip("The highest pitch variation a single sound can be played with.")]
	public float MaxPitch { get; private set; }
	private AudioClip _loadedClip;
	public AudioClip levelMusic;
	private float _masterVolume;
	private float _musicVolume;
	private float _effectVolume;

	private void Awake()
	{
		if (Singleton != null && Singleton != this)
		{
			if (levelMusic != Singleton.levelMusic)
			{
				AdjustVolume();
				Singleton.levelMusic = levelMusic;
				Singleton.PlayLevelMusic();
			}
			Destroy(gameObject);
		}
		else
		{
			AdjustVolume();
			Singleton = this;
		}
		DontDestroyOnLoad(this);
	}

	/*private void OnEnable()
	{
		SettingsUI.OnVolumeChange += AdjustVolume;
		PauseController.OnVolumeChange += AdjustVolume;
	}*/

	/*private void OnDisable()
	{
		SettingsUI.OnVolumeChange -= AdjustVolume;
		PauseController.OnVolumeChange -= AdjustVolume;
	}*/

	private void AdjustVolume()
	{
		_masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
		_musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
		_effectVolume = PlayerPrefs.GetFloat("EffectVolume", 0.5f);
		_musicSource.volume = _masterVolume * _musicVolume;
	}

	public void Start() => PlayLevelMusic(); // TODO: remove later.

	public void PlayLevelMusic() => PlayMusic(levelMusic);

	public void PlaySound(AudioClip clip, float vol)
	{
		_effectsSource.volume = vol * _masterVolume * _effectVolume;
		_effectsSource.pitch = 1f;
		_effectsSource.PlayOneShot(clip);
	}

	/*public IEnumerator LoadAndPlaySound(AssetReferenceAudioClip unloadedClip, float vol)
	{
		_effectsSource.volume = vol * _masterVolume * _effectVolume;
		_effectsSource.pitch = 1f;
		string key = unloadedClip.AssetGUID;

		AsyncOperationHandle<AudioClip> loadOperation = Addressables.LoadAssetAsync<AudioClip>(key);
		yield return loadOperation;

		if (loadOperation.Status == AsyncOperationStatus.Succeeded)
		{
			AudioClip clip = loadOperation.Result;
			_effectsSource.PlayOneShot(clip);
			// Wait for the sound to finish playing
			yield return new WaitForSeconds(clip.length);
			AddressableManager.UnloadAsset(clip);
		}
	}*/

	public void StopSound() => _effectsSource.Stop();

	/*private void UnloadSound()
	{
		if (_loadedClip)
			AddressableManager.UnloadAsset(_loadedClip);
		_loadedClip = null;
	}*/

	public void PlayRandomSound(AudioClip clip, float vol)
	{
		float randomPitch = Random.Range(MinPitch, MaxPitch);
		_effectsSource.pitch = randomPitch;
		_effectsSource.volume = vol * _masterVolume * _effectVolume;
		_effectsSource.PlayOneShot(clip);
	}

	/*public async void LoadAndPlayMusic(AssetReferenceAudioClip unloadedClip)
	{
		UnloadSound();
		string key = unloadedClip.AssetGUID;
		_loadedClip = await AddressableManager.LoadAsset<AudioClip>(key);
		_musicSource.clip = _loadedClip;
		StartCoroutine(FadeIn(2f));
	}*/

	public void PlayMusic(AudioClip clip)
	{
		//UnloadSound();
		_musicSource.clip = clip;
		StartCoroutine(FadeIn(2f));
	}

	public void StopMusic() => StartCoroutine(FadeOut(2f));

	public IEnumerator FadeIn(float duration)
	{
		float t = 0;
		_musicSource.volume = 0f;
		_musicSource.Play();
		while (t < duration)
		{
			t += Time.deltaTime;
			_musicSource.volume = Mathf.Lerp(0f, _masterVolume * _musicVolume, t / duration);
			yield return null;
		}
		_musicSource.volume = _masterVolume * _musicVolume;
		yield return null;
	}

	public IEnumerator FadeOut(float duration)
	{
		float currentTime = 0;
		float startVol = _musicSource.volume;
		while (currentTime < duration)
		{
			currentTime += Time.deltaTime;
			_musicSource.volume = Mathf.Lerp(startVol, 0f, currentTime / duration);
			if (_musicSource.volume <= 0)
				_musicSource.Stop();
			yield return null;
		}
		//UnloadSound();
		yield return null;
	}
}
