using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    public AudioClip backgroundMusic;

    private AudioSource audioSource;

    void PlayBackgroundMusic()
    {
        this.audioSource.clip = this.backgroundMusic;
        this.audioSource.loop = true;
        this.audioSource.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.audioSource = this.GetComponent<AudioSource>();
        this.PlayBackgroundMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
