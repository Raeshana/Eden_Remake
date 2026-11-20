using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource  musicSource;
    [SerializeField] AudioSource  sfxSource;
    [SerializeField] AudioSource assistantVoice;
    
    [Header("Audio Clips")]
    public AudioClip forestBackground;
    public AudioClip sold;
    public AudioClip gunshot;
    public AudioClip ignite;
    public AudioClip pot;
    public AudioClip leather;
    public AudioClip click;
    public AudioClip eat;
    public AudioClip deerDeath;
    public AudioClip ding;

    [Header("Assistant")]
    [SerializeField] float assistantPause;
    private float lastTalked;
    public AudioClip[] deerHit;
    public AudioClip deerMiss;
    public AudioClip carrying;
    public AudioClip cooked;
    public AudioClip cooking;
    public AudioClip shoot;
    public AudioClip cook;
    public AudioClip sell;
    public AudioClip notCarrying;
    public AudioClip assistance;
    public AudioClip yesAssistance;
    public AudioClip noAssistance;
    public AudioClip carry;
    public AudioClip eaten; //You have eaten the deer
    public AudioClip released; // You have released the deer

    private void Start()
    {
        musicSource.clip = forestBackground;
        musicSource.Play();
    }

    public void Playsfx(AudioClip clip)
    {
        sfxSource.PlayOneShot (clip);
    }

    public void PlayAssistantVoice(AudioClip clip)
    {
        if(clip.name == "deerHit")
        {
            assistantVoice.Stop();
            assistantVoice.PlayOneShot (clip);
            lastTalked = Time.time;
        }

        else if (!assistantVoice.isPlaying && 
            Time.time - lastTalked > assistantPause)
        {
            assistantVoice.PlayOneShot (clip);
            lastTalked = Time.time;
        }
    }
}
