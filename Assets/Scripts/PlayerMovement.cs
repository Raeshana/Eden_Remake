using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    private ShopController shopController;
    UnityEvent isShopOpen;

    private float sensitivity = 0.5f;
    private float yaw = 0f;
    private float pitch = 0f;
    private Vector2 lookVector;

    void Awake()
    {
        // Get reference to shop in scene
        shopController = GameObject.FindWithTag("ShopController").GetComponent<ShopController>();
        
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
        // Mathf.Clamp(lookVector.y, lookVerticalClampMin, lookVerticalClampMax); 
    }
}
