using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopItemController : MonoBehaviour
{
    [Tooltip("To be dynamically populated in ShopUI script")]
    public ShopItem shopItem;

    private GameObject shopControllerGO;
    private MoneyController moneyController;

    [Header("Reference the shop item prefab children here")]
    [SerializeField] TMP_Text itemName;
    [SerializeField] Image itemImage;
    [SerializeField] TMP_Text itemCost;
    [SerializeField] GameObject toolTip;
    [SerializeField] Button itemButton;

    private GameObject playerGO;
    private Stat hungerStat;
    private Stat satisfactionStat;

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
    public void purchaseShopItem()
    {
        if (moneyController.buyFromShop(shopItem))
        {
            // If the item is an upgrade, decrease hunger rate
            // Otherwise, add satisfaction
            if (shopItem.isUpgrade)
            {
                hungerStat.DecreaseStatRate(shopItem.saturationModifier);                    
            }
            else 
            {
                satisfactionStat.IncreaseCurrStat(shopItem.satisfactionModifier);
            }
            // Disable button
            itemButton.interactable = false; 
        }
        else
        {
            Debug.Log("You have inadequate money to purchase " + shopItem.name);
        }
    }

    /// <summary>
    /// Fills in shop item UI with info stored in the SO
    /// </summary>
    public void populateShopItem()
    {
        itemName.text = shopItem.itemName;
        itemImage = shopItem.itemImage;
        itemCost.text = "$" + shopItem.itemCost;
        toolTip.GetComponentInChildren<TMP_Text>().text = "" + shopItem.toolTip;
        toggleToolTip(false);
    }

    public void toggleToolTip(bool mode)
    {
        toolTip.gameObject.SetActive(mode);
    }
}
