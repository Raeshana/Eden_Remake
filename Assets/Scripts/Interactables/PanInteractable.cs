using UnityEngine;
using System.Collections;
using TMPro;

public class PanInteractable : MonoBehaviour, IInteractable
{
    private PlayerMovement playerMovement;
    private HungerStat hungerStat;
    private string text;

    void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        hungerStat = player.GetComponent<HungerStat>();
        playerMovement = player.GetComponent<PlayerMovement>();
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
            Debug.Log("E to cook deer");
            text = "E to cook deer";
        }
        else
        {
            text = " ";
        }
    }

    // interactable
    public void Interact()
    {
        if (playerMovement.isDraggingDeer) EatDeer();
    }

    /// <summary>
    /// Increases player current hunger stat by 5
    /// Destory deer game object (will not exist anymore)
    /// </summary>
    private void EatDeer()
    {
        hungerStat.IncreaseCurrStat(5);   
        Debug.Log("eaten");
        playerMovement.isDraggingDeer = false;
        Destroy(playerMovement.deerGO); // would no longer exist
    }
}