using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicPlayer : MonoBehaviour
{
    
    [SerializeField]
    public AudioSource music1;
    
    private AudioSource activeMusic;

    public AudioSource ActiveMusic
    {
        get => activeMusic;
        set => activeMusic = value;
    }


    // Start is called before the first frame update
    void Start()
    {
        activeMusic = music1;
    }

    public void ChangeMusic(AudioSource a)
    {
        activeMusic.Stop ();
        activeMusic = a;
        activeMusic.loop = true;
        activeMusic.volume = 0.2f;
        activeMusic.Play();
    }
}
