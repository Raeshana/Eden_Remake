using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HotKeys : MonoBehaviour
{
    [SerializeField] ShopController shopController;
    private SoundManager soundManager;

    void Awake()
    {
        soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
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

        // Press S to open/ close decor shop
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {   
            ToggleDecorShop();
        }

        // Press I to open/ close upgrade shop
        if (Keyboard.current.iKey.wasPressedThisFrame)
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