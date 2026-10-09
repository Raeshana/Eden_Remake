using UnityEngine;
using System.Collections;

public class DeerInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject deerMesh;
    private DeerState currentState = DeerState.Live;

    private PlayerMovement playerMovement;
    private MoneyController moneyController;
    private HungerStat hungerStat;

    void Awake()
    {
        moneyController = GameObject.FindWithTag("MoneyController").GetComponent<MoneyController>();   
        GameObject player = GameObject.FindWithTag("Player");
        hungerStat = player.GetComponent<HungerStat>();  
        playerMovement = player.GetComponent<PlayerMovement>();

    }

    // interactable
    public void Hover()
    {
        // change crosshairs to red
        Debug.Log("E to shoot deer");
    }

    // interactable
    public void Interact()
    {
        switch (currentState)
        {
            case DeerState.Live:
                Debug.Log("hit live deer -> deer is now dead");
                StartCoroutine(ShootDeer());
                break;
            case DeerState.Dead:
                Debug.Log("hit dead deer -> deer is now being dragged");
                DragDeer();
                break;
            case DeerState.Dragged:
                Debug.Log("dropped deer -> released, eaten or sold");
                ReleaseDeer();
                break;
        }
    }

    /// <summary>
    /// Wait 1 second before changing deer label
    /// to avoid instant deer drag
    /// Set lastShotFired to current time
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShootDeer()
    {
        yield return new WaitForSeconds(1); 
        currentState = DeerState.Dead;
        // dying deer animation
        // decrease total deer
    }

    /// <summary>
    /// Hide deer by deactivating deer mesh (child)
    /// Display transparent little deer in UI(?) to indicate drag
    /// Dragging deer
    /// </summary>
    private void DragDeer()
    {
        // set deer icon to true
        // deerIconGO.SetActive(true);
        playerMovement.isDraggingDeer = true;
        playerMovement.deerGO = gameObject;
        currentState = DeerState.Dragged;
        deerMesh.SetActive(false);
    }

    /// <summary>
    /// No longer dragging deer- dead
    /// Unhide deer by activating deer mesh (child)
    /// Deer icon no longer visible
    /// </summary>
    private void ReleaseDeer()
    {
        // deer icon false
        // deerIconGO.SetActive(false);
        playerMovement.isDraggingDeer = false;
        currentState = DeerState.Dead;
        deerMesh.SetActive(true);
    }
}