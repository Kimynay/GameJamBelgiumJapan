using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip TitleAudioClip;
    public AudioClip FreedAnimalsAudioClip;
    public AudioClip GameAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Title()
    {
        audioSource.clip = TitleAudioClip;
        audioSource.Play();
    }

    public void FreedAnimals()
    {
        audioSource.clip = FreedAnimalsAudioClip;
        audioSource.Play();
    }

    public void Game()
    {
        audioSource.clip = GameAudioClip;
        audioSource.Play();
    }
}
