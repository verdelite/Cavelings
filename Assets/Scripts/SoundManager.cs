using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameManager gameManager;

    public AudioSource soundSource;

    public AudioSource soundSource2;

    public AudioClip[] soundList;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        if(gameManager) gameManager.soundManager = this;

        //audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound)
    {
        soundSource.clip = sound;
        soundSource.Play();
    }

    public void PlaySoundByName(string name)
    {
        soundSource.clip = GetSoundByName(name);
        if(soundSource.clip) soundSource.Play();
    }

    public void PlaySoundByName2(string name)
    {
        soundSource2.clip = GetSoundByName(name);
        if(soundSource2.clip) soundSource2.Play();
    }

    public void PlaySoundOnce(string name)
    {
        soundSource2.clip = GetSoundByName(name);
        if(soundSource2.clip) soundSource2.PlayOneShot(soundSource2.clip);
    }

    AudioClip GetSoundByName(string name)
    {
        foreach(AudioClip sound in soundList)
        {
            if(sound.name == name) return sound;
        }

        print(name + " not found in soundList!");
        return null;
    }

    public void StopSound()
    {
        soundSource.Stop();
    }

    public void StopSound2()
    {
        soundSource2.Stop();
    }

    public void PauseAllSound()
    {
        //soundSource.Pause();
        soundSource2.Pause();
    }

    public void UnpauseAllSound()
    {
        soundSource.UnPause();
        soundSource2.UnPause();
    }
}
