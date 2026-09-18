using UnityEngine;
using TMPro;

public class MoneyController : MonoBehaviour
{
    [Tooltip("Amount of money the player currently has")]
    public float playerMoney;

    private TMP_Text moneyText;

    void Start()
    {
        // The player starts with $100
        playerMoney = 100;

        // Get reference to money text
        moneyText = GetComponent<TMP_Text>();

        // Set initial money text
        updateMoney(0);
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
}
