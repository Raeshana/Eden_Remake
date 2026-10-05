using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class DetectDeer0 : MonoBehaviour
{
    // Ref. https://youtu.be/48p1M7McQ_0?si=JHEVSlWgCrmgfR9W
    [Tooltip("Choose what objects get hit by raycast")]
    [SerializeField] private LayerMask raycastLayers;
    private RaycastHit hit;
    
    [Header("Shop Variables")]
    private GameObject shopController;
    private MoneyController moneyController;

    // [Tooltip("For deer anim")]
    // private RectTransform rectTransform;
    // private CanvasGroup canvasGroup;
    // private Canvas canvas;
    // private Vector2 initialPos;
    // private DeerManager deerManager;

    private void Awake()
    {
        // Find the money controller
        shopController = GameObject.FindWithTag("ShopController");
        moneyController = shopController.GetComponent<MoneyController>();

        // // Get rect transform for drag anim
        // rectTransform = GetComponent<RectTransform>(); // change as drag
        // initialPos = rectTransform.anchoredPosition; // initial

        // // Get canvas group for drop anim
        // canvasGroup = GetComponent<CanvasGroup>();

        // // Get canvas
        // canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();

        // // Deer Manager
        // deerManager = GameObject.FindWithTag("DeerManager").GetComponent<DeerManager>();
    }

    // See Order of Execution for Event Functions for information on FixedUpdate() and Update() related to physics queries
    void FixedUpdate()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("Left Clicky");
            // Does the ray intersect any objects excluding the player layer
            Physics.Raycast(transform.position, transform.forward, out hit, 100f, raycastLayers);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if (hit.rigidbody != null && hit.transform.tag == "Deer")
            {
                Debug.Log("Did Hit");
                ShootDeer();
            }
        }
        // else
        // {
        //     Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        //     Debug.Log("Did not Hit");
        // }

    }

    public void ShootDeer()
    {
        moneyController.updateMoney(-50);
        // deerManager.decreaseNumDeer();
        Destroy(hit.transform.gameObject);
    }
}