using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _stepsSource;

    [Header("Music")]
    [SerializeField] private AudioClip _backgroundMusic;
    [SerializeField] private AudioClip _stepsMusic;

    [SerializeField] private AudioClip _buttonClick;
    [SerializeField] private AudioClip _ring;

    [SerializeField] private AudioClip _lootPickup;
    [SerializeField] private AudioClip _open;

    private bool _musicStarted = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (_musicSource)
            {
                _musicSource.loop = true;
                if (_backgroundMusic)
                {
                    _musicSource.clip = _backgroundMusic;
                    _musicSource.Play();
                    _musicStarted = true;
                }
            }

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetAllSounds();

        if (!_musicStarted)
        {
            PlayBackgroundMusic();
            _musicStarted = true;
        }
        else
        {
            if (_musicSource && !_musicSource.isPlaying)
            {
                _musicSource.Play();
            }
        }
    }

    public void PlayBackgroundMusic()
    {
        if (_backgroundMusic && _musicSource)
        {
            if (_musicSource.clip != _backgroundMusic)
                _musicSource.clip = _backgroundMusic;

            if (!_musicSource.isPlaying)
            {
                _musicSource.loop = true;
                _musicSource.Play();
            }
        }
    }

    public void StopBackgroundMusic()
    {
        if (_musicSource && _musicSource.isPlaying)
        {
            _musicSource.Stop();
        }
    }

    public void PlayStepsMusic()
    {
        if (_stepsMusic && _stepsSource)
        {
            if (_stepsSource.clip != _stepsMusic)
                _stepsSource.clip = _stepsMusic;

            if (!_stepsSource.isPlaying)
            {
                _stepsSource.loop = true;
                _stepsSource.Play();
            }
        }
    }

    public void StopStepsMusic()
    {
        if (_stepsSource && _stepsSource.isPlaying)
        {
            _stepsSource.Stop();
        }
    }

    public void ResetAllSounds()
    {
        if (_sfxSource)
            _sfxSource.Stop();
    }

    public void PlaySfx(AudioClip clip)
    {
        if (clip && _sfxSource)
        {
            _sfxSource.PlayOneShot(clip);
        }
    }

    public void StopAllSfx()
    {
        if (_sfxSource)
        {
            _sfxSource.Stop();
        }
    }

    public void PlayButtonClick() => PlaySfx(_buttonClick);
    public void PlayRing() => PlaySfx(_ring);

    public void PlayLootPickupSound() => PlaySfx(_lootPickup);
    public void PlayOpenSound() => PlaySfx(_open);
}
