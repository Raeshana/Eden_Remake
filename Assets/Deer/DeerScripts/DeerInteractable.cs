using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class DeerInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject deerMesh;
    [HideInInspector]
    public DeerState currentState;

    private PlayerMovement playerMovement;
    private string text;

    private SoundManager soundManager;
    private DeerManager deerManager;

    private Image cursor;

    void Awake()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();   
        deerManager = GameObject.FindWithTag("DeerManager").GetComponent<DeerManager>();
        currentState = DeerState.Live;
        cursor = GameObject.FindWithTag("Cursor").GetComponent<Image>();
    }

    public string InteractionText
    {
        get { return text; }
    }

    // interactable
    public void Hover()
    {
        switch (currentState)
        {
            case DeerState.Live:
            {
                // turn cursor red to indicate hit
                Debug.Log(cursor.name);
                cursor.color = Color.red;
                text = "E to shoot deer";
                break;
            }
            case DeerState.Dead:
            {
                // turn cursor blue to indicate 'something idk'
                cursor.color = Color.blue;
                text = "E to pick up deer";
                break;
            }
            case DeerState.Dragged:
            {
                // cursor.sprite = 
                text = "You are dragging the deer. E to deselect.";
                break;
            }
        }
        
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
        // sound of shot
        soundManager.Playsfx(soundManager.gunshot);
        currentState = DeerState.Dead;
        yield return new WaitForSeconds(1); 
        // dying deer animation
        // decrease total deer
        deerManager.decreaseNumDeer();
    }

    /// <summary>
    /// Hide deer by deactivating deer mesh (child)
    /// Display transparent little deer in UI(?) to indicate drag
    /// Dragging deer
    /// </summary>
    private void DragDeer()
    {
        // set dragged dear GO to be true
        playerMovement.isDraggingDeer = true;
        playerMovement.deerGO = gameObject;
        currentState = DeerState.Dragged;
        deerMesh.SetActive(false);
        soundManager.Playsfx(soundManager.leather);
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
        soundManager.Playsfx(soundManager.leather);
    }
}