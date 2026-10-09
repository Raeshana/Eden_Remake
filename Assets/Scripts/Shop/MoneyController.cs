using UnityEngine;
using TMPro;

public class MoneyController : MonoBehaviour
{
    [Tooltip("Amount of money the player currently has")]
    public float playerMoney;

    private TMP_Text moneyText;

    void Awake()
    {
        // get money text box
        moneyText = GetComponent<TMP_Text>();
    }

    void Start()
    {
        // Get moneyC
        // The player starts with $100
        playerMoney = 100;
    }

    /// <summary>
    /// Recalculates current playerMoney
    /// Updates the money display text
    /// </summary>
    /// <param name="cost">The amount to reduce current money by</param>
    public void updateMoney(float cost)
    {
        playerMoney += cost;
        moneyText.text = "$" + playerMoney;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="shopItem"></param>
    /// <returns></returns>
    public bool buyFromShop(float itemCost)
    {
        if ((playerMoney - itemCost) < 0) // Inadequate money, return false
        {
            return false;
        }
        else // Adequate money, return true
        {
            // Calculate remaining player money
            updateMoney(-itemCost);
            return true;
        }
    }
}
