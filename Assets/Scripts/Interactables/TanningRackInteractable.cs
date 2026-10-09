using UnityEngine;
using System.Collections;
using TMPro;

public class TanningRackInteractable : MonoBehaviour, IInteractable
{
    private PlayerMovement playerMovement;
    private HungerStat hungerStat;
    private MoneyController moneyController;
    private string text;

    void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        hungerStat = player.GetComponent<HungerStat>();
        playerMovement = player.GetComponent<PlayerMovement>();
        moneyController = GameObject.FindWithTag("MoneyController").GetComponent<MoneyController>();
    }

    public string InteractionText
    {
        get { return text; }
    }

    // interactable
    public void Hover()
    {
        if (playerMovement.isDraggingDeer)
        {
            Debug.Log("E to sell deer");
            text = "E to sell deer";
        }
        else
        {
            text = " ";
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
        Destroy(playerMovement.deerGO); // would no longer exist
    }
}