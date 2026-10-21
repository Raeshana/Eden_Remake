using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource  musicSource;
    [SerializeField] AudioSource  sfxSource;
    
    [Header("Audio Clips")]
    public AudioClip forestBackground;
    public AudioClip sold;
    public AudioClip gunshot;
    public AudioClip ignite;
    public AudioClip pot;
    public AudioClip leather;
    public AudioClip click;
    public AudioClip eat;

    private void Start()
    {
        musicSource.clip = forestBackground;
        musicSource.Play();
    }

    public void Playsfx(AudioClip clip)
    {
        sfxSource.PlayOneShot (clip);
    }
}
