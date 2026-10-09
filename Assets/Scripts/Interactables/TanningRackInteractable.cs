using UnityEngine;
using System.Collections;

public class TanningRackInteractable : MonoBehaviour, IInteractable
{
    private PlayerMovement playerMovement;
    private HungerStat hungerStat;
    private MoneyController moneyController;

    void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        hungerStat = player.GetComponent<HungerStat>();
        playerMovement = player.GetComponent<PlayerMovement>();
        moneyController = GameObject.FindWithTag("MoneyController").GetComponent<MoneyController>();
    }

    // interactable
    public void Hover()
    {
        if (playerMovement.isDraggingDeer)
        {
            Debug.Log("E to sell deer");
        }
    }

    // interactable
    public void Interact()
    {
        if (playerMovement.isDraggingDeer) SellDeer();
    }

   /// <summary>
    /// Increase player's current money
    /// Destory deer game object (will not exist anymore)
    /// </summary>
    private void SellDeer()
    {
        moneyController.updateMoney(50);
        Debug.Log("sold");
        playerMovement.isDraggingDeer = false;
        Destroy(gameObject); // would no longer exist
    }
}