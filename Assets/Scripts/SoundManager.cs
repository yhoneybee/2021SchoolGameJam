using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public enum SoundType
{
    BGM,
    EFFECT,
    BUTTON,
    END,
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; } = null;

    public AudioSource[] audioSources = new AudioSource[(int)SoundType.END];

    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    private float bgm_volume = 0.5f;
    public float BgmVolume
    {
        get { return bgm_volume * TotalVolume; }
        set { bgm_volume = value; audioSources[((int)SoundType.BGM)].volume = BgmVolume; }
    }
    private float sfx_volume = 0.3f;
    public float SfxVolume
    {
        get { return sfx_volume * TotalVolume; }
        set { sfx_volume = value; audioSources[((int)SoundType.EFFECT)].volume = SfxVolume; }
    }
    private float btn_volume = 1;
    public float BtnVolume
    {
        get { return btn_volume * TotalVolume; }
        set { btn_volume = value; audioSources[((int)SoundType.BUTTON)].volume = BtnVolume; }
    }

    private float total_volume;
    public float TotalVolume
    {
        get { return total_volume; }
        set
        {
            total_volume = value;

            audioSources[((int)SoundType.BGM)].volume = BgmVolume;
            audioSources[((int)SoundType.EFFECT)].volume = SfxVolume;
            audioSources[((int)SoundType.BUTTON)].volume = BtnVolume;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            GameObject sound = GameObject.Find("SoundManager");

            if (sound)
            {
                name = "SoundManager";
                DontDestroyOnLoad(gameObject);

                string[] soundNames = Enum.GetNames(typeof(SoundType));

                for (int i = 0; i < soundNames.Length - 1; i++)
                {
                    GameObject go = new GameObject { name = soundNames[i] };
                    audioSources[i] = go.AddComponent<AudioSource>();
                    go.transform.SetParent(sound.transform);
                }

                audioSources[(int)SoundType.BGM].loop = true;
            }
        }
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        TotalVolume = 1;
        Play("Bgm", SoundType.BGM);
    }
    public void SwitchMute()
    {
        foreach (var item in audioSources)
            item.mute = !item.mute;
    }
    public void StopAllSound()
    {
        foreach (var item in audioSources)
        {
            item.clip = null;
            item.Stop();
        }

        audioClips.Clear();
    }
    public void Play(AudioClip audioClip, SoundType soundType = SoundType.EFFECT)
    {
        if (!audioClip)
            return;

        AudioSource audioSource;

        audioSource = audioSources[(int)soundType];

        if (soundType == SoundType.BGM)
        {
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
            audioSource.PlayOneShot(audioClip);
    }

    public void Play(string path, SoundType soundType = SoundType.EFFECT) => Play(GetOrAddAudioClip(path, soundType), soundType);

    AudioClip GetOrAddAudioClip(string path, SoundType soundType)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        AudioClip audioClip = null;

        if (soundType == SoundType.BGM)
        {
            audioClip = Resources.Load<AudioClip>(path);
        }
        else
        {
            if (audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Resources.Load<AudioClip>(path);
                audioClips.Add(path, audioClip);
            }
        }

        if (!audioClip)
            Debug.LogWarning($"AudioClip Missing!, path info : {path}");

        return audioClip;
    }
}
