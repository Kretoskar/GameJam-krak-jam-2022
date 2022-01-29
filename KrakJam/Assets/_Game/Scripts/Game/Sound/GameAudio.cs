using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    [SerializeField] private float fadeTime = 2;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip level1Music;

    public static GameAudio Instance;

    private AudioSource currentAudioSource;
    private AudioSource newAudioSource;
    
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
    }

    private void Start()
    {
        currentAudioSource = new GameObject().AddComponent<AudioSource>();
        currentAudioSource.loop = true;
        currentAudioSource.transform.parent = transform;
        
        ChangeToMainMenu();
    }

    public void ChangeToMainMenu()
    {
        StartCoroutine(MusicTransitionCoroutine(menuMusic));
    }

    public void ChangeToLevel1()
    {
        StartCoroutine(MusicTransitionCoroutine(level1Music));
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
