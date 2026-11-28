using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private SoundManager soundManager;

    void Awake()
    {
        // shopController = GameObject.FindWithTag("ShopController").GetComponent<ShopController>();
        soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();

        if (!soundManager) Debug.Log("Can't find sound manager");
    }

    void Update()
    {
        // Press Esc to quit
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            QuitButton();
        }

        // Press Space to start
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            PlayButton();
        }
    }

    public void QuitButton()
    {
        PlaySound();
        Debug.Log("Quitting game.");
        Application.Quit();
    }

    public void PlayButton()
    {
        PlaySound();
        SceneManager.LoadScene(2);
    }

    public void CreditsButton()
    {
        PlaySound();
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {
        PlaySound();
        SceneManager.LoadScene(0);
    }

    private void PlaySound()
    {
        soundManager.Playsfx(soundManager.click);   
    }

    // private void OnDisable()
    // {
    //     Debug.Log($"{gameObject.name} -> {GetType().Name} was disabled!", this);
    // }
}