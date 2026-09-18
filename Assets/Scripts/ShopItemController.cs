using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopItemController : MonoBehaviour
{
    [Tooltip("To be dynamically populated in ShopUI script")]
    public ShopItem shopItem;

    private GameObject moneyControllerGO;
    private MoneyController moneyController;

    [Header("Reference the shop item prefab children here")]
    [SerializeField] TMP_Text itemName;
    [SerializeField] Image itemImage;
    [SerializeField] TMP_Text itemCost;
    [SerializeField] GameObject toolTip;

    private GameObject playerGO;
    private Stat hungerStat;
    private Stat satisfactionStat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find the money controller
        moneyControllerGO = GameObject.FindWithTag("MoneyController");
        moneyController = moneyControllerGO.GetComponent<MoneyController>();

        // Find the player and their stats
        playerGO = GameObject.FindWithTag("Player");
        hungerStat = playerGO.GetComponent<HungerStat>();
        hungerStat = playerGO.GetComponent<SatisfactionStat>();
    }

    /// <summary>
    /// Checks if the player has adequate money to buy shop item
    /// If they do, subtracts the shop item cost from their money
    /// If they don't, displays error message
    /// </summary>
    public void purchaseItem()
    {
        if ((moneyController.playerMoney - shopItem.itemCost) < 0) // Inadequate money, error message
        {
            Debug.Log("You have inadequate money");
        }
        else // Adequate money, complete transaction
        {
            if (shopItem.isUpgrade)
            {
                // hungerStat.IncreaseMaxHunger()
            }
            moneyController.updateMoney(shopItem.itemCost); 
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
