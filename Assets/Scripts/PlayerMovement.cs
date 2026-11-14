using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // private ShopController shopController;
    // UnityEvent isShopOpen;
    [HideInInspector]
    public bool isDraggingDeer;
    [HideInInspector]
    public GameObject deerGO;

    [SerializeField] float sensitivity;
    private float yaw = 0f;
    private float pitch = 0f;
    private Vector2 lookVector;

    private Vector2 moveDir;
    [SerializeField] float moveSpeed;
    private Rigidbody rigidBody;

    void Awake()
    {
        // Get reference to shop in scene
        // shopController = GameObject.FindWithTag("ShopController").GetComponent<ShopController>();
        
        // Check for if player is dragging deer or not
        // To check eat/ sell interactions
        isDraggingDeer = false;
        rigidBody = GetComponent<Rigidbody>();

        // // Listen for shop opened event
        // if (isShopOpen == null)
        //     isShopOpen = new UnityEvent();

        // isShopOpen.AddListener(shopController.toggleShop);
    }  

    // update every fixed duration
    void FixedUpdate()
    {   // apply rotated moveDir to velocity

        // rigidBody.transform.position == rigidBody.transform.forward 
        Debug.Log("move dir: " + moveDir + "\n");

        // combine inputs into a move
        Vector3 moveVec = moveDir.x * rigidBody.transform.right + 
                        moveDir.y * rigidBody.transform.forward;
        
        // make sure player does not move vertically
        moveVec.y = 0f;

        // normalize for dialonal movement
        Vector3.Normalize(moveVec);

        // multiply by speed and set linear velocity
        rigidBody.linearVelocity = moveVec * moveSpeed;
        Debug.Log("vel vec: " + rigidBody.linearVelocity + "\n");
        
        
        // (moveDir * moveSpeed) + rigidBody.transform.forward;
        // (moveDir * moveSpeed) + rigidBody.transform.forward;
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

    // Ref https://docs.unity.com/en-us/engine/6000.0/script-reference/unityengine/charactercontroller/move
    // Ref https://youtu.be/ONlMEZs9Rgw?si=2MnTXW74qjHer4EH
   void OnMove(InputValue input)
    {
        moveDir = input.Get<Vector2>();
    }
}
