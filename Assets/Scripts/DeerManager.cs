using UnityEngine;
using System.Collections;

public class DeerManager : MonoBehaviour
{
    static public int numDeer;
    [SerializeField] GameObject deerGO; 
    [SerializeField] Transform[] deerTransforms;

    void Awake()
    {
        numDeer = 0; // Initialize
    } 

    void Start()
    {
        
    }

    void Update()
    {
        if (numDeer < 3)
        {
            StartCoroutine(SpawnDeer());
            numDeer++;
        }
    }

    private IEnumerator SpawnDeer()
    {
        yield return new WaitForSeconds(5f);
        Instantiate(deerGO, deerTransforms[Random.Range(0, deerTransforms.Length)]);
        Debug.Log("Number of deer increased: " + numDeer);
    }

    public void decreaseNumDeer()
    {
        numDeer = numDeer - 1;
        Debug.Log("Number of deer decreased: " + numDeer);
    }
}
