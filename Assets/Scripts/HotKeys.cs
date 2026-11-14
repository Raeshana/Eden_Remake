using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HotKeys : MonoBehaviour
{
    [HideInInspector]
    public ShopController shopController;
    private SoundManager soundManager;

    void Awake()
    {
        soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
        shopController = GameObject.FindWithTag("ShopController").GetComponent<ShopController>();
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        // Press Esc to quit
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            QuitGame();
        }

        // Press R to reload the current scene
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            ReloadGame();
        }

        // Press I to open/ close decor shop
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {   
            ToggleDecorShop();
        }

        // Press U to open/ close upgrade shop
        if (Keyboard.current.uKey.wasPressedThisFrame)
        {   
            ToggleUpgradeShop();
        }
    }

    private void PlaySound()
    {
        soundManager.Playsfx(soundManager.click);   
    }

    public void QuitGame()
    {
        PlaySound();
        Application.Quit();
        Debug.Log("Quitting game.");
    }

    public void ReloadGame()
    {
        PlaySound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }    

    public void ToggleDecorShop()
    {
        PlaySound();
        shopController.populateDecor();
    }

    public void ToggleUpgradeShop()
    {
        PlaySound();
        shopController.populateUpgrade();
    }
}