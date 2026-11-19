using UnityEngine;
using System.Collections;

public class DeerAI : MonoBehaviour
{

     [SerializeField] GameObject deerGO;
    // // private NavMeshAgent navMeshAgent;
    // [SerializeField] float moveTimeLimit;
    // [SerializeField] float moveTimeRange;
    // private float actualMoveLimit;
    [SerializeField] float moveSpeed;

    [Tooltip("For debugging")]
    [SerializeField] Transform currMovePoint;
    private Transform lastMovePoint;
    // private bool atMovePoint = false;
    private float startTimeMove = 0;
    private float moveDist = 0;

    private DeerManager deerManager;
    public Transform[] movePoints;

    private DeerInteractable deerInteractable;

    void Awake()
    {
        deerInteractable = GetComponent<DeerInteractable>();
        deerManager = GameObject.FindWithTag("DeerManager").GetComponent<DeerManager>();

        // initialize and populate move points 
        movePoints = new Transform[deerManager.movePoints.Length];
        for(int i = 0; i < deerManager.movePoints.Length; i++)
        {
            movePoints[i] = deerManager.movePoints[i];
        }

        lastMovePoint = deerGO.transform;
        currMovePoint = movePoints[GetMovePoint()];
        moveDist = Vector3.Distance(lastMovePoint.position, currMovePoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        switch(deerInteractable.currentState)
        {
            case DeerState.Live:
            {
                // if the deer is alive, we want it to move to currPoint
                float distCovered = (Time.time - startTimeMove) * moveSpeed;
                float fractionOfJourney = distCovered/moveDist;
                deerGO.transform.position = Vector3.Lerp(lastMovePoint.position, currMovePoint.position, fractionOfJourney);
                
                // when it reaches currPoint
                if (deerGO.transform.position == currMovePoint.position) 
                {
                    // set lastPoint to currPoint
                    lastMovePoint = currMovePoint;
                    // find a new point
                    currMovePoint = movePoints[GetMovePoint()];
                    // recalculate dist between points
                    moveDist = Vector3.Distance(lastMovePoint.position, currMovePoint.position);
                    // reaset start time move
                    startTimeMove = Time.time;
                }
                break;
            }

            case DeerState.Dying:
                break;

            case DeerState.Dead:
                break;

            case DeerState.Dragged:
                break;
        }

    }

    /// <summary>
    /// Get a random move point
    /// And random time the deer will stay there 
    /// </summary>
    /// <returns></returns>
    private int GetMovePoint()
    {
        return (int)UnityEngine.Random.Range(0, movePoints.Length-1);
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
}
