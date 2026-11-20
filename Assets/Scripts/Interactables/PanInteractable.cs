using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class PanInteractable : MonoBehaviour, IInteractable
{
    // The pan can be empty,
    // filled with raw meat, or
    // filled with cooked meat
    private enum state
    {
        empty,
        cooking,
        cooked,
    }

    [SerializeField] GameObject meatGO;
    [SerializeField] MeshRenderer meatMeshRenderer;
    [SerializeField] Material cookedMeat;
    [SerializeField] Material rawMeat;
    [SerializeField] float hungerIncrease;
    // private DeerManager deerManager;

    private PlayerMovement playerMovement;
    private HungerStat hungerStat;
    private string text;
    private state currState;

    // keep track of how long deer has been cooking for
    private float elapsed; // calculated in update
    private float inPanTime; // set in CookDeers

    private SoundManager soundManager; 

    private bool doneSpeaking = false;

    void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        hungerStat = player.GetComponent<HungerStat>();
        playerMovement = player.GetComponent<PlayerMovement>();
        soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();
        // deerManager = GameObject.FindWithTag("DeerManager").GetComponent<DeerManager>();

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
                CookedDeer(); // change mesh
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
                CookDeer(); 
                break;
            
            case state.cooking:
                // player feedback
                text = "The deer is cooking"; //visual
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
                    text = "Press E to cook the deer."; //visual
                    if (!doneSpeaking)
                    {
                        soundManager.PlayAssistantVoice(soundManager.cook); //audio
                        doneSpeaking = true;
                    }
                }
                break;

            case state.cooking:
                text = "The deer is cooking."; //visual
                break;

            case state.cooked:
                text = "Press E to eat the deer.";//visual
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
    private void CookDeer()
    {   
        if (playerMovement.isDraggingDeer)
        {
            // thing to display/ player feedback
            meatGO.SetActive(true);
            meatMeshRenderer.material = rawMeat;

            // change deer state -> cooking
            playerMovement.isDraggingDeer = false;
            Destroy(playerMovement.deerGO); // would no longer exist
            currState = state.cooking;
            // Debug.Log(currState);
            // change mesh

            // sfx
            

            // player feedback
            text = "You are cooking the deer."; // visual
            //sfx
            StartCoroutine(CookDeerSFX());
            inPanTime = Time.time;
            currState = state.cooking;

        }       
        else
        {
            // Debug.Log("Can click E when not carrying deer on empty pan");
            text = "You are not currently carrying any deer"; // visual
            soundManager.PlayAssistantVoice(soundManager.notCarrying); //audio
        } 
    }

    private IEnumerator CookDeerSFX()
    {
        soundManager.Playsfx(soundManager.pot);
        yield return new WaitForSeconds(0.5f);
        soundManager.Playsfx(soundManager.ignite);
        yield return new WaitForSeconds(0.5f);
        soundManager.PlayAssistantVoice(soundManager.cooking); //audio
        yield return new WaitForSeconds(0.5f);
    }

    /// <summary>
    /// Assertion: current state is cooked
    /// Change mesh to reflect cooked deer
    /// </summary>
    private void CookedDeer()
    {
        // player feedback
        text = "Deer is cooked"; // visual
        soundManager.Playsfx(soundManager.ding); // sfx
        soundManager.PlayAssistantVoice(soundManager.cooked); // audio

        // change mesh
        meatMeshRenderer.material = cookedMeat;
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
        text = "You have eaten the deer"; // visual
        soundManager.Playsfx(soundManager.eat); // sfx
        // soundManager.PlayAssistantVoice(soundManager.eaten); // audio

        // eating increases the player's 'fullness'
        hungerStat.IncreaseCurrStat(hungerIncrease); 

        // update state
        currState = state.empty;

        // hide meat mesh
        meatGO.SetActive(false);

        // reset doneSpeaking
        doneSpeaking = false;
    }
}