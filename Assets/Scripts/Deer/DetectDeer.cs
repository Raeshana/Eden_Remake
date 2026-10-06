using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using TMPro;

public class DetectDeer0 : MonoBehaviour
{
    // Ref. https://youtu.be/48p1M7McQ_0?si=JHEVSlWgCrmgfR9W
    [Tooltip("Choose what objects get hit by raycast")]
    [SerializeField] private LayerMask raycastLayers;
    private RaycastHit hit;
    
    [Header("Shop Variables")]
    private GameObject shopController;
    private MoneyController moneyController;

    [Header("Stats Variables")]
    private GameObject Player;
    private HungerStat hungerStat;

    [Header("Gun Stuff")]
    [SerializeField] private TMP_Text crosshairs;
    [SerializeField] private float gunReload;
    private float lastShotFired = 0.0f;

    [Header("Deer Stuff")]
    private bool isCarryingDeer;
    private GameObject deerGO;
    [SerializeField] GameObject deerIconGO;

    private void Awake()
    {
        // Find the money controller
        shopController = GameObject.FindWithTag("ShopController");
        moneyController = shopController.GetComponent<MoneyController>();

        // Find player and stats
        Player = GameObject.FindWithTag("Player");
        hungerStat = Player.GetComponent<HungerStat>();

        // Set iscarryingdeer to false
        isCarryingDeer = false;
    }

    // See Order of Execution for Event Functions for information on FixedUpdate() and Update() related to physics queries
    void FixedUpdate()
    {   
        // Does the ray intersect any objects excluding the player layer
        Physics.Raycast(transform.position, transform.forward, out hit, 100f, raycastLayers);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

        // Change crosshairs colour to red on detect deer
        if (hit.rigidbody != null && hit.transform.tag == "Deer" && !isCarryingDeer)
        {
            crosshairs.color = Color.red;
        }
        else // change back when deer not detected
        {
            crosshairs.color = Color.white;
        }

        // player left clicks
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("Left Clicky");
            
            // Is the player carrying a deer?
            if (isCarryingDeer)
            {
                if (hit.rigidbody != null && hit.transform.tag == "Pan")
                {
                    EatDeer();
                }
                else if (hit.rigidbody != null && hit.transform.tag == "TanningRack")
                {
                    SellDeer();
                }
                else
                {
                    // return deer to 'original position'
                    // just make it visible 
                    deerGO.SetActive(true); 
                    ReleaseDeer();
                }
            }

            // Did raycast hit live deer while not already carrying one?
            if (hit.rigidbody != null && hit.transform.tag == "Deer" && !isCarryingDeer)
            {   
                // Has enough time elapsed since the last time the player shot?
                if ((Time.time - lastShotFired) >= gunReload)
                {
                    Debug.Log("Shot fired, the deer has been hit and is now dead.");
                    StartCoroutine(ShootDeer());
                }
                else {
                    Debug.Log("Shot on cooldown: " + (Time.deltaTime - lastShotFired));
                }
            }

            // Did raycast hit dead deer while not already carrying one?
            if (hit.rigidbody != null && hit.transform.tag == "DeadDeer" && !isCarryingDeer)
            {
                Debug.Log("Picked up deer's carcass");
                // Deer death animation
                DragDeer();
            }
        }
        // else
        // {
        //     Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        //     Debug.Log("Did not Hit");
        // }

    }

    /// <summary>
    /// Wait 1 second before changing deer label
    /// to avoid instant deer drag
    /// Set lastShotFired to current time
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShootDeer()
    {
        // deerManager.decreaseNumDeer();
        lastShotFired = Time.time;
        yield return new WaitForSeconds(1); 
        hit.transform.tag = "DeadDeer";
    }

    /// <summary>
    /// Store deer game object to reference later if needed
    /// Hide deer
    /// Display transparent little deer in UI(?) to indicate drag
    /// </summary>
    private void DragDeer()
    {
        isCarryingDeer = true;
        Debug.Log("dragging deer");
        deerGO = hit.transform.gameObject;
        deerGO.SetActive(false);
        deerIconGO.SetActive(true);
    }

    /// <summary>
    /// No longer carrying deer
    /// Deer icon no longer visible
    /// </summary>
    private void ReleaseDeer()
    {
        isCarryingDeer = false;
        deerIconGO.SetActive(false);
    }

    /// <summary>
    /// Increase player's current money
    /// Destory deer game object (will not exist anymore)
    /// Release deer
    /// </summary>
    private void SellDeer()
    {
        moneyController.updateMoney(-50);
        Destroy(deerGO); // would no longer exist
        ReleaseDeer();
    }

    /// <summary>
    /// 
    /// </summary>
    private void EatDeer()
    {
        hungerStat.IncreaseCurrStat(5);   
        Destroy(deerGO); // would no longer exist
        ReleaseDeer();
    }
}