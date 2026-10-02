using UnityEngine;
using System.Collections;

public class OutsideController : MonoBehaviour
{
    [SerializeField] GameObject outsideObjectGO;
    [SerializeField] float outsideSpawnTime;
    [SerializeField] int outSideObjectMax;
    // private OutsideObject outsideObject;
    private int currOutsideObjectNum;

    [SerializeField] Transform[] objectTransforms;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get outside object
        currOutsideObjectNum = 0;

        // Spawn outside objects
        StartCoroutine(SpawnDeerCoroutine());
    }

    /// <summary>
    /// Spawn deer every deerSpawnTime seconds
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnDeerCoroutine(){
        int transformNum = Random.Range(0, objectTransforms.Length);
        if (currOutsideObjectNum < outSideObjectMax) {
            Instantiate(outsideObjectGO, objectTransforms[transformNum]);
            currOutsideObjectNum++;
        }
        yield return new WaitForSeconds(outsideSpawnTime);
    }

}
