using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;

public class Interactable : MonoBehaviour
{   // Ref. https://youtu.be/48p1M7McQ_0?si=JHEVSlWgCrmgfR9W
    [Tooltip("Choose what objects get hit by raycast")]
    [SerializeField] private LayerMask raycastLayers;
    private RaycastHit hit;
    // [SerializeField] Image cursor;

    // Used in player movement 'on interact' to determine if the raycast
    // intersects with an interactable 
    // (which then triggers the interact function on the interactable)
    [HideInInspector]
    public bool canInteract;

    // Do something to the UI when the player interacts/ can interact
    private TMP_Text UIText;

    // [SerializeField] private TMP_Text crosshairs;

    // to know when to change scope
    private DeerManager deerManager;

    void Awake()
    {
        // uiContainer.SetActive(false);
        canInteract = false;
        UIText = GameObject.FindWithTag("UIText").GetComponent<TMP_Text>();
        UIText.text = " ";
        deerManager = GameObject.FindWithTag("DeerManager").GetComponent<DeerManager>();

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
            // Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        
            // get the interactable interface from the hit object 
            interactable = hit.collider.GetComponent<IInteractable>();

            // communicate to player that they can interact with the hit object
            if (interactable != null)
            {
                // Debug.Log(hit.collider.name); // uncomment to see what interactable the raycast hit

                // make sure scope is white otherwise
                // if (hit.collider.tag != "Deer")
                // {
                //     deerManager.cursor.color = Color.white;
                // }

                interactable.Hover(); 
                UIText.text = interactable.InteractionText;

                // while player is looking at interactble, interact on pressing 'e'
                if (canInteract)
                {
                    interactable.Interact();
                    canInteract = false;
                }
            }  
            
            // if (interactable == null || hit.collider.tag != "Deer")
            // {
            //     deerManager.cursor.color = Color.white;
            // }
        }
        else
        {
            Debug.Log("not intersecting");
            if (interactable == null || hit.collider.tag != "Deer")
            {
                deerManager.cursor.color = Color.white;
            }
        }
    }
}
