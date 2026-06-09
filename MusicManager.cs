using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    [Header("Music Clips")]
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameMusic;

    [Header("Settings")]
    [SerializeField] private float fadeDuration = 1.5f;

    private float volume = 0.5f;
    private Coroutine currentRoutine;

    private AudioClip currentClip;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (!audioSource)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.loop = true;

        volume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        audioSource.volume = volume;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        PlayMenuMusic();
    }

    void OnDestroy()
    {
        if (Instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }

 
    public void PlayMenuMusic()
    {
        PlayMusic(menuMusic);
    }

    public void PlayGameMusic()
    {
        PlayMusic(gameMusic);
    }

    public void SetMusicVolume(float newVolume)
    {
        volume = newVolume;
        audioSource.volume = newVolume;

        PlayerPrefs.SetFloat("MusicVolume", newVolume);
        PlayerPrefs.Save();
    }

    public float GetMusicVolume() => volume;

 
    private void PlayMusic(AudioClip newClip)
    {
        if (newClip == null)
            return;

        if (currentClip == newClip && audioSource.isPlaying)
            return;

        currentClip = newClip;

        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
            currentRoutine = null;
        }

        currentRoutine = StartCoroutine(CrossfadeTo(newClip));
    }

    private IEnumerator CrossfadeTo(AudioClip newClip)
    {
        float startVolume = audioSource.volume;

     
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();

        t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, volume, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = volume;
        currentRoutine = null;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Ajusta esto a tus nombres reales de escena
        if (scene.name.ToLower().Contains("menu"))
        {
            PlayMenuMusic();
        }
        else
        {
            PlayGameMusic();
        }
    }
}