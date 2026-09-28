using UnityEngine;
using UnityEngine.EventSystems;

// Reference https://youtu.be/BGr-7GZJNXg?si=Kt4KqKq-OAM8VkRn (drag and drop tutorial)
public class PotController : MonoBehaviour, IDropHandler 
{
    private RectTransform rectTransform;
    private HungerStat hungerStat;
    [SerializeField] DeerManager deerManager;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped");
        // Game object currently being dragged
        if (eventData.pointerDrag != null)
        {
            // Snap to position
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = rectTransform.anchoredPosition;

            // Increase player hunger by 20
            hungerStat.IncreaseCurrStat(20);

            // Destory game object being dragged
            Debug.Log(eventData.pointerDrag.transform.gameObject.name);
            Destroy(eventData.pointerDrag.transform.gameObject);
            deerManager.numDeer--;
        }   
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        hungerStat = GameObject.FindWithTag("Player").GetComponent<HungerStat>();
    }
}
