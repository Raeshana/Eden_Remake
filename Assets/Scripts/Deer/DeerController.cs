using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class DeerController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler{

    [Header("Shop Variables")]
    private GameObject shopController;
    private MoneyController moneyController;

    [Tooltip("For deer anim")]
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private Vector2 initialPos;
    private DeerManager deerManager;

    // Reference https://youtu.be/BGr-7GZJNXg?si=Kt4KqKq-OAM8VkRn (drag and drop tutorial)
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Left Clicky");
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Right Clicky");
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("Let go of right clicky");
            ShootDeer();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Start drag");
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0.6f;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Dragging");
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Done drag");
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1.0f;
            rectTransform.anchoredPosition = initialPos;
        }        
    }

    private void Awake()
    {
        // Find the money controller
        shopController = GameObject.FindWithTag("ShopController");
        moneyController = shopController.GetComponent<MoneyController>();

        // Get rect transform for drag anim
        rectTransform = GetComponent<RectTransform>(); // change as drag
        initialPos = rectTransform.anchoredPosition; // initial

        // Get canvas group for drop anim
        canvasGroup = GetComponent<CanvasGroup>();

        // Get canvas
        canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();

        // Deer Manager
        deerManager = GameObject.FindWithTag("DeerManager").GetComponent<DeerManager>();
    }

    public void ShootDeer()
    {
        moneyController.updateMoney(-50);
        deerManager.decreaseNumDeer();
        Destroy(gameObject);
    }
}
