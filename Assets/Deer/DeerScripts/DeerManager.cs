using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeerManager : MonoBehaviour
{
    static public int numDeer;
    [SerializeField] GameObject deerGO; 
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float spawnTime;

    public Transform[] movePoints;
    
    private float lastSpawnTime = 0;
    private float elapsed = 0; 

    public Image cursor;
    public Image hand;

    void Awake()
    {
        numDeer = 0; // Initialize
        // get cursor for deer to reference
        cursor = GameObject.FindWithTag("Cursor").GetComponent<Image>();
    } 

    void Update()
    {
        elapsed = Time.time - lastSpawnTime;
        // Spawn deer depending on current num deer
        if (elapsed >= (numDeer * spawnTime))
        {
            lastSpawnTime = Time.time;
            SpawnDeer();
            numDeer++;
        }
    }   

    /// <summary>
    /// Instantiate deer at a random spawn point
    /// </summary>
    private void SpawnDeer()
    {
        Instantiate(deerGO, spawnPoints[Random.Range(0, spawnPoints.Length-1)]);
    }

    /// <summary>
    /// When deer is killed, decrease total deer
    /// </summary>
    public void decreaseNumDeer()
    {
        numDeer = numDeer - 1;
        Debug.Log("Number of deer decreased: " + numDeer);
    }
}
