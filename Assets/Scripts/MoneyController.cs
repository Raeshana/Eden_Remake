using UnityEngine;
using TMPro;

public class MoneyController : MonoBehaviour
{
    [Tooltip("Amount of money the player currently has")]
    public float playerMoney;

    [SerializeField] TMP_Text moneyText;

    void Start()
    {
        // Get moneyC
        // The player starts with $100
        playerMoney = 100;

        // Set initial money text
        // updateMoney(0);
    }

    /// <summary>
    /// Recalculates current playerMoney
    /// Updates the money display text
    /// </summary>
    /// <param name="cost">The amount to reduce current money by</param>
    public void updateMoney(float cost)
    {
        playerMoney = playerMoney - cost;
        moneyText.text = "$" + playerMoney;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="shopItem"></param>
    /// <returns></returns>
    public bool buyFromShop(ShopItem shopItem)
    {
        if ((playerMoney - shopItem.itemCost) < 0) // Inadequate money, return false
        {
            return false;
        }
        else // Adequate money, return true
        {
            // Calculate remaining player money
            updateMoney(shopItem.itemCost);
            return true;
        }
    }
}
