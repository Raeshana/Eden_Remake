using UnityEngine;
using System.Collections;

public class DeerController : MonoBehaviour
{
    private GameObject shopController;
    private MoneyController moneyController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find the money controller
        shopController = GameObject.FindWithTag("ShopController");
        moneyController = shopController.GetComponent<MoneyController>();
    }

    public void ShootDeer()
    {
        moneyController.updateMoney(-50);
        // Destroy(this);
    }
}
