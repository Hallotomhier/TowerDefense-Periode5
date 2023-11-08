using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public Sound[] musicSound, sfx;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;    
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        PlaySound("Theme");
    }
    private void OnDisable()
    {
        PlaySound(null);
    }
    public void PlaySound(string name)
    {
        Sound sounds = Array.Find(musicSound, x => x.name == name);
        if(sounds == null)
        {
            Debug.Log("Not Found");

        }
        else
        {
            musicSource.clip = sounds.clip;
            musicSource.Play();
        }
        
        
    }
    public void PlaySfx(string name)
    {
        Sound s = Array.Find(sfx, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Not Found");

        }
        else
        {
            sfxSource.clip = s.clip;
            sfxSource.Play();
        }
    }
}

