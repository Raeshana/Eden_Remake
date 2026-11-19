using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class DeerInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject deerMesh;
    public DeerState currentState;

    private PlayerMovement playerMovement;
    private MoneyController moneyController;
    private HungerStat hungerStat;
    private string text;

    private SoundManager soundManager;
    private DeerManager deerManager;

    void Awake()
    {
        moneyController = GameObject.FindWithTag("MoneyController").GetComponent<MoneyController>();   
        GameObject player = GameObject.FindWithTag("Player");
        hungerStat = player.GetComponent<HungerStat>();  
        playerMovement = player.GetComponent<PlayerMovement>();
        soundManager = GameObject.FindWithTag("SoundManager").GetComponent<SoundManager>();   
        deerManager = GameObject.FindWithTag("DeerManager").GetComponent<DeerManager>();

        currentState = DeerState.Live;
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
                // Debug.Log(deerManager.cursor.name);
                // deerManager.cursor.color = Color.red;
                Debug.Log("E to shoot deer");
                text = "E to shoot deer";
                break;
            }

            case DeerState.Dying:
            {
                text = "Nice shot!";
                break;
            }
                
            case DeerState.Dead:
            {
                // turn cursor blue to indicate 'something idk'
                // cursor.color = Color.blue;
                text = "E to pick up deer";
                Debug.Log("E to pick up deer");
                break;
            }

            case DeerState.Dragged:
                text = "You are dragging the deer. E to deselect.";
                break;
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

            case DeerState.Dying:
                // play death animation
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
    /// to dead to avoid instant deer drag
    /// Set lastShotFired to current time
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShootDeer()
    {
        // change state to dying so player cannot shoot it again
        currentState = DeerState.Dying;

        // sound of shot
        soundManager.Playsfx(soundManager.gunshot);
        // death animation
        StartCoroutine(LerpRotation(Quaternion.Euler(new Vector3(90,0,0)), 1));
        // decrease total deer
        deerManager.decreaseNumDeer();
        // deer dying noises
        soundManager.Playsfx(soundManager.deerDeath);

        yield return new WaitForSeconds(1.5f);
        currentState = DeerState.Dead;
    }

    // https://gamedevbeginner.com/glossary/how-to-lerp-rotation-in-unity/
    IEnumerator LerpRotation(Quaternion endValue, float duration)
    {
        float time = 0;
        Quaternion startValue = transform.rotation;

        while (time < duration)
        {
            transform.rotation = Quaternion.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.rotation = endValue;
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
        if (playerMovement.isDraggingDeer)
        {
            currentState = DeerState.Dead;
            deerMesh.SetActive(true);
            playerMovement.isDraggingDeer = false;
            soundManager.Playsfx(soundManager.leather); 
        }
    }

    private void OnDisable()
    {
        Debug.Log($"{gameObject.name} -> {GetType().Name} was disabled!", this);
    }
}