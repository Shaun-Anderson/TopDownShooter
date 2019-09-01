using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip backgroundClip;
    public AudioClip menuClip;
    public AudioSource background;

    void Start ()
    {
        //Create Background Music
        background = this.gameObject.GetComponent<AudioSource>();

        background.playOnAwake = false;
        background.clip = backgroundClip;
        background.volume = 0.1f;
        background.Play();
        Destroy(background, background.clip.length);
    }

    public void Pause()
    {
        background.Pause();
    }

    public void Play(AudioClip clip, float pitch, float volume)
    {
        AudioSource audioSource = this.gameObject.AddComponent<AudioSource>();
        
        audioSource.playOnAwake = false;
        audioSource.clip = clip;
        audioSource.pitch = pitch;
        audioSource.volume = volume;

        audioSource.Play();
        Destroy(audioSource, audioSource.clip.length);
    }
}
