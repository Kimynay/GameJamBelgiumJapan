using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSE : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip UpAudioClip;
    public AudioClip DownAudioClip;
    public AudioClip ClearClip;
    public AudioClip GameOverClip;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Up()
    {
        audioSource.PlayOneShot(UpAudioClip);
    }

    public void Down()
    {
        audioSource.PlayOneShot(DownAudioClip);
    }

    public void Clear()
    {
        audioSource.PlayOneShot(ClearClip);
    }

    public void GameOver()
    {
        audioSource.PlayOneShot(GameOverClip);
    }
}
