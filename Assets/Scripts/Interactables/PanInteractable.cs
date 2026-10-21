using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class PanInteractable : MonoBehaviour, IInteractable
{
    // The pan can be empty,
    // filled with raw meat, or
    // filled with cooked meat
    private enum state
    {
        empty,
        raw,
        cooking,
        cooked,
    }

    private PlayerMovement playerMovement;
    private HungerStat hungerStat;
    private string text;
    private state currState;

    // keep track of how long deer has been cooking for
    private bool startTimer; // set in CookDeer
    private float elapsed; // calculated in update
    private float inPanTime; // set in CookDeers

    void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        hungerStat = player.GetComponent<HungerStat>();
        playerMovement = player.GetComponent<PlayerMovement>();

        // The pan is initially empty
        currState = state.empty;
    }

    void Update()
    {
        if (currState == state.cooking)
        {
            elapsed = Time.time - inPanTime;
            if (elapsed >= 3)
            {
                // change mesh to cooked and update state
                // change mesh
                currState = state.cooked;
                // Debug.Log(currState);
                elapsed = 0f; // reset timer
            }    
        }
        else
        {
            elapsed = 0f;
        }
    }

    private void panStateManager()
    {
        switch (currState)
        {
            case state.empty:
                PlaceDeer(); 
                break;

            case state.raw:
                CookDeer();
                break;
            
            case state.cooking:
                text = "The deer is cooking";
                break;

            case state.cooked:
                EatDeer();
                break;
        }
    } 

    public string InteractionText
    {
        get { return text; }
    }

    // interactable
    public void Hover()
    {
        switch (currState)
        {
            case state.empty:
                if (playerMovement.isDraggingDeer)
                {
                    text = "E to place deer in pan";
                }
                break;
            
            case state.raw:
                if (!startTimer)
                {
                    text = "E to turn on stove";
                }
                break;

            case state.cooking:
                text = "The deer is cooking";
                break;

            case state.cooked:
                text = "E to eat cooked deer";
                break;
        }
    }

    // interactable
    public void Interact()
    {
        panStateManager();
    }

    /// <summary>
    /// Assertion: current state is empty
    /// Check if the player is dragging the deer (must be true)
    /// no longer dragging deer, deer object no longer exists
    /// pan state changes from empty to raw,
    /// the mesh is changed to reflect that (player feedback)
    /// </summary>
    private void PlaceDeer()
    {   
        if (playerMovement.isDraggingDeer)
        {
            // thing to display/ player feedback
            text = "Deer placed in pan";
            
            // change deer state -> raw
            playerMovement.isDraggingDeer = false;
            Destroy(playerMovement.deerGO); // would no longer exist
            currState = state.raw;
            // Debug.Log(currState);
            // change mesh
        }       
        else
        {
            // Debug.Log("Can click E when not carrying deer on empty pan");
            text = "You are not currently carrying any deer";
        } 
    }

    /// <summary>
    /// Assertion: current state is raw
    /// If the pan has been in the raw state for more than 3 seconds,
    /// Change the state to cooked
    /// Change mesh to reflect that (player feedback)
    /// Change state to cooking during those 3 seconds
    /// </summary>
    /// <param name="inPanTime"></param>
    private void CookDeer()
    {   
        // player feedback
        text = "Stove turned on- deer cooking";
        // play stove turning on sound

        // Record the time a deer is set to be cooked
        inPanTime = Time.time;
        currState = state.cooking;
    }

    /// <summary>
    /// Assertion: current state is cooked
    /// Increases player current hunger stat by 5
    /// Destory deer game object (will not exist anymore)
    /// Player feedback: eating sound
    /// </summary>
    private void EatDeer()
    {
        // player feedback
        text = "You have eaten the deer";
        // play eating sound

        // eating increases the player's 'fullness'
        hungerStat.IncreaseCurrStat(5); 

        // update state
        currState = state.empty;
    }
}