using UnityEngine.Audio;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Transform SfxSourcePool;

    public AudioMixer Mixer;

    public SoundBank SoundBank;

    private List<AudioSource> sourcePool;

    public AudioSource MusicSource;


    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        sourcePool = SfxSourcePool.GetComponentsInChildren<AudioSource>().ToList<AudioSource>();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            SetMasterVolume(PlayerPrefs.GetFloat("MasterVolume"));
        }
        else
        {
            SetMasterVolume(1);
        }

        if (PlayerPrefs.HasKey("SfxVolume"))
        {
            SetSfxVolume(PlayerPrefs.GetFloat("SfxVolume"));

        }
        else
        {
            SetSfxVolume(1);
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));

        }
        else
        {
            SetMusicVolume(1);
        }
    }

    public void PlaySound(Sound sound)
    {
        if (sound == null)
        {
            return;
        }

        //Debug.Log("Play sound" + sound.clip.name);

        bool clipPlayed = false;
        foreach (AudioSource source in sourcePool)
        {
            if (!source.isPlaying)
            {
                source.volume = sound.volume;
                source.outputAudioMixerGroup = sound.mixerGroup;
                source.clip = sound.clip;
                source.Play();
                break;
            }
        }
        if (!clipPlayed)
        {
            AudioSource source = sourcePool[UnityEngine.Random.Range(0, sourcePool.Count - 1)];
            source.volume = sound.volume;
            source.outputAudioMixerGroup = sound.mixerGroup;
            source.clip = sound.clip;
            source.Play();
        }
    }

    public void PlayMusic(Sound sound)
    {
        if (sound == null)
        {
            return;
        }

        MusicSource.Stop();
        MusicSource.clip = sound.clip;
        MusicSource.volume = sound.volume;
        MusicSource.outputAudioMixerGroup= sound.mixerGroup;


        MusicSource.Play();
    }

    public void PauseMusicSource()
    {
        MusicSource.Pause();
    }
    public void ResumeMusicSource()
    {
        MusicSource.Play();
    }
    public void SetMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat("MasterVolume", volume);
        Mixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public float GetMasterVolume()
    {
        float volume = 0;
        Mixer.GetFloat("MasterVolume", out volume);
        volume = Mathf.Pow(10, volume / 20f);
       
        return volume;
    }

    public void SetSfxVolume(float volume)
    {
        PlayerPrefs.SetFloat("SfxVolume", volume);
        Mixer.SetFloat("SfxVolume", Mathf.Log10(volume) * 20);
    }

    public float GetSfxVolume()
    {
        float volume = 0;
        Mixer.GetFloat("SfxVolume", out volume);
        volume = Mathf.Pow(10, volume / 20f);
        
        return volume;
    }

    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
        Mixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }

    public float GetMusicVolume()
    {
        float volume = 0;
        Mixer.GetFloat("MusicVolume", out volume);
        volume = Mathf.Pow(10, volume / 20f);

        return volume;
    }
}