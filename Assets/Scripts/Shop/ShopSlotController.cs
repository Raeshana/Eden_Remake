using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopSlotController : MonoBehaviour
{
    private GameObject shopControllerGO;
    private MoneyController moneyController;

    [Header("Reference the shop slot prefab children here")]
    [SerializeField] TMP_Text itemName;
    [SerializeField] Image itemImage;
    [SerializeField] TMP_Text itemCostSlot;
    [SerializeField] GameObject toolTip;
    [SerializeField] Button itemButton;

    private GameObject playerGO;
    private Stat hungerStat;
    private Stat satisfactionStat;

    [Header("Shopitem prefab used for debugging")]
    [SerializeField] ShopItem shopItemPopulated;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find the money controller
        shopControllerGO = GameObject.FindWithTag("ShopController");
        moneyController = shopControllerGO.GetComponent<MoneyController>();

        // Find the player and their stats
        playerGO = GameObject.FindWithTag("Player");
        hungerStat = playerGO.GetComponent<HungerStat>();
        satisfactionStat = playerGO.GetComponent<SatisfactionStat>();
    }

    /// <summary>
    /// Checks if the player has adequate money to buy shop item
    /// If they do, subtracts the shop item cost from their money
    /// If they don't, displays error message
    /// </summary>
    public void purchaseFromShopSlot()
    {
        if (moneyController.buyFromShop(shopItemPopulated.itemCost))
        {
            // If the item is an upgrade, decrease hunger rate
            // Otherwise, add satisfaction
            if (shopItemPopulated.isUpgrade)
            {
                hungerStat.DecreaseStatRate(shopItemPopulated.saturationModifier);                    
            }
            else 
            {
                satisfactionStat.IncreaseCurrStat(shopItemPopulated.satisfactionModifier);
            }
            // Disable button
            itemButton.interactable = false; 
        }
        else
        {
            Debug.Log("You have inadequate money to purchase " + shopItemPopulated.name);
        }
    }

    /// <summary>
    /// Called by shop controller
    /// Fills in shop slot with info stored in the item SO
    /// </summary>
    public void populateShopItem(ShopItem shopItem)
    {
        itemName.text = shopItem.itemName;
        itemImage = shopItem.itemImage;
        itemCostSlot.text = "$" + shopItem.itemCost;
        toolTip.GetComponentInChildren<TMP_Text>().text = "" + shopItem.toolTip;
        toggleToolTip(false);

        shopItemPopulated = shopItem;
    }

    public void toggleToolTip(bool mode)
    {
        toolTip.gameObject.SetActive(mode);
    }
}
