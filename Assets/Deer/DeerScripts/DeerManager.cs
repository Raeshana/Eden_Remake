using UnityEngine;
using System.Collections;

public class DeerManager : MonoBehaviour
{
    // static public int numDeer;
    [SerializeField] GameObject deerGO; 
    [SerializeField] Transform[] deerTransforms;
    private bool isSpawning;

    void Awake()
    {
        // numDeer = 0; // Initialize
        isSpawning = false;
    } 

    void Update()
    {
        // if no deer exist
        if (GameObject.FindWithTag("Deer") == null && !isSpawning)
        {
            // spawn one in same spot as before after 5 seconds
            StartCoroutine(SpawnDeer());
            // numDeer++;
        }
        // stop deer spawning
        else StopCoroutine(SpawnDeer());
    }

    private IEnumerator SpawnDeer()
    {
        isSpawning = true;
        yield return new WaitForSeconds(5f);
        Instantiate(deerGO, deerTransforms[0]);
        isSpawning = false;
        // Debug.Log("Number of deer increased: " + numDeer);
    }

    public void decreaseNumDeer()
    {
        // numDeer = numDeer - 1;
        // Debug.Log("Number of deer decreased: " + numDeer);
    }
}
