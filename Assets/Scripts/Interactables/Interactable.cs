using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using TMPro;

public class Interactable : MonoBehaviour
{   // Ref. https://youtu.be/48p1M7McQ_0?si=JHEVSlWgCrmgfR9W
    [Tooltip("Choose what objects get hit by raycast")]
    [SerializeField] private LayerMask raycastLayers;
    private RaycastHit hit;

    // Is set in player movement using on interact input system action
    public bool canInteract;

    [SerializeField] TMP_Text UIText;

    // [SerializeField] private TMP_Text crosshairs;

    void Awake()
    {
        // uiContainer.SetActive(false);
        canInteract = false;
        UIText = GameObject.FindWithTag("UIText").GetComponent<TMP_Text>();
        UIText.text = " ";
    }

    // Update is called once per frame
    void Update()
    {
        // reset to null on every frame 
            IInteractable interactable = null;

        // Does the ray intersect the interactables layer?
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100f, raycastLayers))
        {
            // draw to debug
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        
            // get the interactable interface from the hit object 
            interactable = hit.collider.GetComponent<IInteractable>();

            // communicate to player that they can interact with the hit object
            if (interactable != null)
            {
                interactable.Hover(); 
                UIText.text = interactable.InteractionText;

                // while player is looking at interactble, interact on pressing 'e'
                if (canInteract)
                {
                    interactable.Interact();
                    canInteract = false;
                }
            }   
        }
        else
        {
            UIText.text = " ";
        }
    }
}
