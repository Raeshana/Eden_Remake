using UnityEngine;
using TMPro;

public class Purchase : MonoBehaviour
{
    [SerializeField] MoneyController moneyController;
    [SerializeField] TMP_Text moneyText;

    public void buyItem()
    {
        if (moneyController.playerMoney == 0)
        {
            Debug.Log("You have no money");
        }
        else
        {
            moneyController.playerMoney -= 10; 
            moneyText.text = "$" + moneyController.playerMoney;  
        }
    }
}
