using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicPlayer : MonoBehaviour
{
    
    [SerializeField]
    public AudioSource music1;
    
    private AudioSource activeMusic;
    
    [SerializeField]
    public AudioSource passThroughSound;
    
    [SerializeField]
    public AudioSource collisionSound;
    
    [SerializeField]
    public AudioSource landingSound;
    

    public AudioSource ActiveMusic
    {
        get => activeMusic;
        set => activeMusic = value;
    }


    // Start is called before the first frame update
    void Start()
    {
        activeMusic = music1;
        ChangeMusic(activeMusic);

        foreach (AbstractPlateform plateform in FindObjectsOfType<AbstractPlateform>())
        {
            plateform.CollisionSound = collisionSound;
            plateform.LandingSound = landingSound;
            plateform.PassThroughSound = passThroughSound;
        }
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
