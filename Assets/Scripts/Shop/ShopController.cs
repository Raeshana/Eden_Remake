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

    private MoneyController moneyController;
    private GameObject playerGO;
    private Stat hungerStat;
    private Stat satisfactionStat;

    private int pageNum;
    private bool isPaused;
    // UnityEvent shopIsOpen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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
    /// Pauses game when the player opens the shop
    /// Does not pause timers on stats
    /// </summary>
    public void toggleShop()
    {
        // Debug.Log("Shop callback called");
        isPaused = !isPaused;
        gameObject.SetActive(isPaused);
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
