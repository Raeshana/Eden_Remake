using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HotKeys : MonoBehaviour
{
    private ShopController shopController;
    private SoundManager soundManager;

    void Awake()
    {
        shopController = GameObject.FindWithTag("ShopController").GetComponent<ShopController>();
        soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        // Press Esc to quit
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            PlaySound();
            Application.Quit();
            Debug.Log("Quitting game.");
        }

        // Press R to reload the current scene
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            PlaySound();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Press S to open/ close decor shop
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {   
            PlaySound();
            shopController.populateDecor();
        }

        // Press I to open/ close upgrade shop
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {   
            PlaySound();
            shopController.populateUpgrade();
        }
    }

    private void PlaySound()
    {
        soundManager.Playsfx(soundManager.click);   
    }
}