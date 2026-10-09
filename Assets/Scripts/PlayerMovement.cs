using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    private ShopController shopController;
    UnityEvent isShopOpen;
    public bool isDraggingDeer;
    [HideInInspector]
    public GameObject deerGO;

    private float sensitivity = 0.5f;
    private float yaw = 0f;
    private float pitch = 0f;
    private Vector2 lookVector;

    void Awake()
    {
        // Get reference to shop in scene
        shopController = GameObject.FindWithTag("ShopController").GetComponent<ShopController>();
        
        // Check for if player is dragging deer or not
        // To check eat/ sell interactions
        isDraggingDeer = false;

        // // Listen for shop opened event
        // if (isShopOpen == null)
        //     isShopOpen = new UnityEvent();

        // isShopOpen.AddListener(shopController.toggleShop);
    }

    /// <summary>
    /// Rotates camera around player using pitch and yaw
    /// Ref: https://www.geeksforgeeks.org/c-sharp/camera-control-in-unity/
    /// </summary>
    void LateUpdate()
    {
        yaw += lookVector.x * sensitivity;
        pitch -= lookVector.y * sensitivity;
        pitch = Mathf.Clamp(pitch, -30f, 60f);
        
        transform.rotation = Quaternion.Euler(pitch, yaw, 0);
    }

    void Start()
    {
        //Set Cursor to not be visible
        Cursor.visible = false;
    }

    void OnLook(InputValue lookValue)
    {
        lookVector = lookValue.Get<Vector2>();
    }

    void OnInteract(InputValue interactValue)
    {
        if (interactValue.isPressed) 
        {
            // Debug.Log("Interacting");
            GetComponentInChildren<Interactable>().canInteract = true;
        }
    }
}
