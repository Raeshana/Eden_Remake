using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ShopController : MonoBehaviour
{
    [Header("Reference the shop item slots here")]
    [SerializeField] GameObject[] shopItemSlots;
    [SerializeField] int numSlots;

    [Tooltip("Insert scriptable objects for all shop items here")]
    [SerializeField] ShopItem[] shopItems;

    [SerializeField] GameObject crossHairs;

    private MoneyController moneyController;
    private GameObject playerGO;
    private Stat hungerStat;
    private Stat satisfactionStat;

    private int pageNum;
    private bool isPaused;
    // UnityEvent shopIsOpen;

    void Awake()
    {
        // Find the money controller
        moneyController = GetComponent<MoneyController>();

        // Find the player and their stats
        playerGO = GameObject.FindWithTag("Player");
        hungerStat = playerGO.GetComponent<HungerStat>();
        satisfactionStat = playerGO.GetComponent<SatisfactionStat>();

        // Load first shop page
        pageNum = 0;
        loadShopPage(pageNum);

        // Hide shop when game starts
        isPaused = true;
        toggleShop();
        // if (shopIsOpen == null)
        //     shopIsOpen = new UnityEvent();

        // shopIsOpen.AddListener(toggleShop);
    }

    /// <summary>
    /// Pauses game when the player opens the shop (!)
    /// Does not pause timers on stats
    /// Toggles mouse cursor as well
    /// </summary>
    public void toggleShop()
    {
        // Debug.Log("Shop callback called");
        isPaused = !isPaused;
        gameObject.SetActive(isPaused);
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
        crossHairs.SetActive(!isPaused);
        if (isPaused == true) Debug.Log("Paused");
        else Debug.Log("Unpaused");
    }

    public void loadShopPage(int pageNum)
    {
        for(int i = 0; i < numSlots; i++)
        {
            int numShopItem = i + pageNum*numSlots;
            if (numShopItem < shopItems.Length)
            {
                ShopSlotController shopSlotController = shopItemSlots[i].GetComponent<ShopSlotController>();
                shopSlotController.populateShopItem(shopItems[numShopItem]);   
            }
        }
    }
}
