using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Clips")]
    public AudioClip menuMusic;
    public AudioClip easyMusic;
    public AudioClip mediumMusic;
    public AudioClip hardMusic;
    public AudioClip resultsMusic;
    public AudioClip secretMusic;
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

    public void PlayMenuMusic()
    {
        musicSource.clip = menuMusic;
        musicSource.loop = true;
        musicSource.Play();
    }
    public void PlaySecretMusic()
    {
        musicSource.clip = secretMusic;
        musicSource.Play();
    }

    public void PlayResultsMusic()
    {
        musicSource.clip = resultsMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayEasyMusic()
    {
        if (musicSource.clip == easyMusic &&
        musicSource.isPlaying)
        {
            return;
        }
        musicSource.clip = easyMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayMediumMusic()
    {
        if (musicSource.clip == mediumMusic &&
        musicSource.isPlaying)
        {
            return;
        }
        musicSource.clip = mediumMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayHardMusic()
    {
        if (musicSource.clip == hardMusic &&
        musicSource.isPlaying)
        {
            return;
        }
        musicSource.clip = hardMusic;
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