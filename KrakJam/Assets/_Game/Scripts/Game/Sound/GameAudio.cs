using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameAudio : MonoBehaviour
{
    [SerializeField] private Volume volume;
    [SerializeField] private float fadeTime = 2;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip level1Music;
    [SerializeField] private AudioClip level2Music;
    [SerializeField] private AudioClip level3Music;
    [SerializeField] private AudioClip bossMusic;

    [SerializeField] private float minBloom = .5f;
    [SerializeField] private float maxBloom = 2;

    public static GameAudio Instance;

    private AudioSource currentAudioSource;
    private AudioSource newAudioSource;

    private Bloom bloom;
    
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        volume.profile.TryGet(out bloom);
    }

    private void Start()
    {
        currentAudioSource = new GameObject().AddComponent<AudioSource>();
        currentAudioSource.loop = true;
        currentAudioSource.transform.parent = transform;
        
        ChangeToMainMenu();
    }

    private void Update()
    {
        if(currentAudioSource == null) return;
        
        float[] spectrum = new float[64];

        currentAudioSource.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
        
        
        bloom.intensity.value = Mathf.Lerp(minBloom, maxBloom, spectrum[0]);
    }

    public void ChangeToMainMenu()
    {
        StartCoroutine(MusicTransitionCoroutine(menuMusic));
    }

    public void ChangeToLevel1()
    {
        StartCoroutine(MusicTransitionCoroutine(level1Music));
    }

    public void ChangeToLevel2()
    {
        StartCoroutine(MusicTransitionCoroutine(level2Music));
    }

    public void ChangeToLevel3()
    {
        StartCoroutine(MusicTransitionCoroutine(level3Music));
    }

    public void ChangeToBossMusic()
    {
        StartCoroutine(MusicTransitionCoroutine(bossMusic));
    }
    
    private IEnumerator MusicTransitionCoroutine(AudioClip audioClip)
    {
        float timer = 0;

        if(newAudioSource != null)
            Destroy(newAudioSource.gameObject);
        newAudioSource = new GameObject().AddComponent<AudioSource>();
        newAudioSource.loop = true;
        newAudioSource.clip = audioClip;
        newAudioSource.transform.parent = transform;
        newAudioSource.Play();
        
        while (timer <= fadeTime)
        {
            currentAudioSource.volume = Mathf.Lerp(0, 1, 1 - timer / fadeTime);
            newAudioSource.volume = Mathf.Lerp(0, 1, timer / fadeTime);
            yield return null;
            timer += Time.deltaTime;
        }
        

        Destroy(currentAudioSource.gameObject);
        currentAudioSource = newAudioSource;
        newAudioSource = null;
    }
}
