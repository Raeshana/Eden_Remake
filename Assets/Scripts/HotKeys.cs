using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HotKeys : MonoBehaviour
{
    private ShopController shopController;

    void Awake()
    {
        shopController = GameObject.FindWithTag("ShopController").GetComponent<ShopController>();
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        // Press Esc to quit
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
            Debug.Log("Quitting game.");
        }

        // Press R to reload the current scene
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Press S to open/ close the shop
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            shopController.toggleShop();
        }
    }
}