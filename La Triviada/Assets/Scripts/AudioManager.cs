using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Clips")]
    public AudioClip menuMusic;
    public AudioClip correctSound;
    public AudioClip wrongSound;
    public AudioClip tensionSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMenuMusic();
    }

    public void PlayMenuMusic()
    {
        musicSource.clip = menuMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayCorrect()
    {
        sfxSource.PlayOneShot(correctSound);
    }

    public void PlayWrong()
    {
        sfxSource.PlayOneShot(wrongSound);
    }

    public void PlayTension()
    {
        sfxSource.PlayOneShot(tensionSound);
    }

    public void PauseMenuMusic()
    {
        musicSource.Pause();
    }

    public void ResumeMenuMusic()
    {
        musicSource.UnPause();
    }
}