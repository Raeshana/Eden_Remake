using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections.Generic;

public class ShopController : MonoBehaviour
{
    [Header("Reference the shop item slots here")]
    [SerializeField] GameObject[] shopItemSlots;
    [SerializeField] int numSlots;

    [Tooltip("Insert scriptable objects for all shop items here")]
    [SerializeField] ShopItem[] shopItems;
    private List<ShopItem> decorItems = new List<ShopItem>();
    private List<ShopItem> upgradeItems = new List<ShopItem>();

    [Tooltip("Reference shop text here (changes depending on type of shop)")]
    [SerializeField] TMP_Text shopText;

    [Tooltip("Reference crosshairs here (hidden when shop open)")]
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

        // Separate decor and upgrade items
        // int decorNum = 0;
        // int upgradeNum = 0;
        for (int i = 0; i < numSlots; i++)
        {
            Debug.Log("entering list population loop");
            if (shopItems[i].isUpgrade)
            { // add to the upgrades array
                Debug.Log("shopitem: " + shopItems[i]);
                upgradeItems.Add(shopItems[i]);
                Debug.Log("Successfully added " + shopItems[i] + " to upgrades");
            }
            else
            { // add to the decor array
                decorItems.Add(shopItems[i]);
                Debug.Log("Successfully added " + shopItems[i] + " to decor");
            }
            Debug.Log("exiting list population loop");
        }

        // Load first shop page
        pageNum = 0;
        populateDecor();

        // Hide shop when game starts
        isPaused = true;
        toggleShop();
        // if (shopIsOpen == null)
        //     shopIsOpen = new UnityEvent();

        // shopIsOpen.AddListener(toggleShop);
    }

    public void populateDecor()
    {
        populateShop(decorItems);
        shopText.text = "Decor Shop";
    }

    public void populateUpgrade()
    {
        populateShop(upgradeItems);
        shopText.text = "Upgrade Shop";
    }


    /// <summary>
    /// Called when buttons/ hotkeys are pressed
    /// </summary>
    private void populateShop(List<ShopItem> list)
    {
        toggleShop();
        loadShopPage(list);
    }

    /// <summary>
    /// Is the shop visible or not?
    /// Pauses game when the player opens the shop (!)
    /// Does not pause timers on stats
    /// Toggles mouse cursor as well
    /// </summary>
    private void toggleShop()
    {
        // Debug.Log("Shop callback called");
        isPaused = !isPaused;
        gameObject.SetActive(isPaused);
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
        crossHairs.SetActive(!isPaused);
        // if (isPaused == true) Debug.Log("Paused");
        // else Debug.Log("Unpaused");
    }

    /// <summary>
    /// Load shop page, depending on type of shop
    /// </summary>
    /// <param name="pageNum"></param>
    private void loadShopPage(List<ShopItem> list)
    {
        if(list == null) Debug.Log("LIST NULL IN LOAD PAGE");

        for(int i = 0; i < numSlots; i++)
        {
            int numShopItem = i + pageNum*numSlots;
            if (numShopItem < list.Count)
            {
                ShopSlotController shopSlotController = shopItemSlots[i].GetComponent<ShopSlotController>();
                shopSlotController.populateShopItem(list[numShopItem]);   
            }
        }
    }
}
