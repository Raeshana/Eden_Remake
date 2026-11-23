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

    private bool doneSpeaking = false;
    private bool doneSpeakingShoot = false;
    private bool doneSpeakingCarry = false;
    private bool doneSpeakingCarrying = false;

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
    public void Hover(Vector3 hitPoint)
    {
        switch (currentState)
        {
            case DeerState.Live:
            {
                // turn cursor red to indicate hit
                // Debug.Log(deerManager.cursor.name);
                // deerManager.cursor.color = Color.red;
                Debug.Log("Press E to shoot the deer.");
                text = "Press E to shoot the deer.";
                if (!doneSpeakingShoot)
                {
                    soundManager.PlayAssistantVoice(soundManager.shoot); //audio
                    doneSpeakingShoot = true;
                }
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
                text = "Press E to pick up the deer.";
                Debug.Log("Press E to pick up the deer.");
                if (!doneSpeakingCarry)
                {
                    soundManager.PlayAssistantVoice(soundManager.carry); //audio
                    doneSpeakingCarry = true;
                }
                break;
            }

            case DeerState.Dragged:
                text = "You are carrying the deer. Press E to deselect.";
                if (!doneSpeakingCarrying)
                {
                    soundManager.PlayAssistantVoice(soundManager.carrying); //audio
                    doneSpeakingCarrying = true;
                }
                break;
        }
        
    }

    // interactable
    public void Interact(Vector3 hitPoint)
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

        // assistant
        int val = Random.Range(0, soundManager.deerHit.Length-1);
        soundManager.PlayAssistantVoice(soundManager.deerHit[val]); //audio

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
        if (!doneSpeaking)
        {
            soundManager.PlayAssistantVoice(soundManager.leather);
            doneSpeaking = true;
        }
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
            // soundManager.PlayAssistantVoice(soundManager.released); //audio
        }
    }

    // private void OnDisable()
    // {
    //     Debug.Log($"{gameObject.name} -> {GetType().Name} was disabled!", this);
    // }
}