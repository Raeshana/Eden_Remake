using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private float movementX; 
    private float movementY;

    [SerializeField] private float playerSpeed; 
    [SerializeField] private float lookSpeed;

    private Vector2 lookVector;
    [SerializeField] private float lookVerticalClampMin;
    [SerializeField] private float lookVerticalClampMax;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Apply player movement
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        playerRigidbody.linearVelocity = movement * playerSpeed;

        // Apply player rotation
        transform.Rotate(Vector3.up * lookVector.x * lookSpeed);
        // transform.Rotate(Vector3.right * lookVector.y * lookSpeed);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x; 
        movementY = movementVector.y;
    }

    void OnLook(InputValue lookValue)
    {
        lookVector = lookValue.Get<Vector2>(); 
        // Mathf.Clamp(lookVector.y, lookVerticalClampMin, lookVerticalClampMax); 
    }
}
