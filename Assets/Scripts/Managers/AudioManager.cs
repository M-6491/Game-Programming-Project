using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("SFX")]
    public AudioClip jumpSFX;
    public AudioClip damageSFX;
    public AudioClip collectSFX;
    public AudioClip levelCompleteSFX;
    public AudioClip gameOverSFX;

    [Header("Music")]
    public AudioClip gameplayMusic;

    private AudioSource sfxSource;
    private AudioSource musicSource;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // Create two audio sources — one for SFX, one for music
        sfxSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;
        musicSource.volume = 0.4f;

        PlayMusic();
    }

    void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public void PlayJump()
    {
        if (jumpSFX != null)
            sfxSource.PlayOneShot(jumpSFX);
    }

    public void PlayDamage()
    {
        if (damageSFX != null)
            sfxSource.PlayOneShot(damageSFX);
    }

    public void PlayCollect()
    {
        if (collectSFX != null)
            sfxSource.PlayOneShot(collectSFX);
    }

    public void PlayLevelComplete()
    {
        musicSource.Stop();
        if (levelCompleteSFX != null)
            sfxSource.PlayOneShot(levelCompleteSFX);
    }

    public void PlayGameOver()
    {
        musicSource.Stop();
        if (gameOverSFX != null)
            sfxSource.PlayOneShot(gameOverSFX);
    }

    public void PlayMusic()
    {
        if (gameplayMusic != null)
        {
            musicSource.clip = gameplayMusic;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}