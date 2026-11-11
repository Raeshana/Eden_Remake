using UnityEngine;
using UnityEngine.AI;
using System;
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
    private float startTimeStop = 0;
    private float elapsedStop = 0;
    private float startTimeMove = 0;
    private float elapsedMove = 0;
    private float moveDist = 0;

    private DeerManager deerManager;
    public Transform[] movePoints;

    private DeerInteractable deerInteractable;

    void Awake()
    {
        // navMeshAgent = GetComponent<NavMeshAgent>();
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

    void Update()
    {
        // float time += Time.deltaTime;

        switch(deerInteractable.currentState)
        {
            case DeerState.Live:
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
                
                // // if distance between deer and move point is within threshold
                
                // if (moveDist <= maxMoveDist && !atMovePoint) {
                //     atMovePoint = true; // set at move point flag
                //     startTimeStop = Time.time; // record time entered
                // }

                // // if deer is at the movePoint
                // if (atMovePoint)
                // {
                //     // start counting
                //     elapsedStop = Time.time - startTimeStop;

                //     // if x time has passed
                //     // update curr move point
                //     if (elapsedStop >= actualMoveLimit)
                //     {
                //         lastMovePoint = currMovePoint;
                        
                //         atMovePoint = false;
                //         startTimeMove = 0; // start recording time moving
                //     }
                // }
                break;
            
            case DeerState.Dead:
                // play death animation
                StartCoroutine(LerpRotation(Quaternion.Euler(new Vector3(0,0,90)), 1));
                break;

            case DeerState.Dragged:
                break;
        }
    }

    /// <summary>
    /// Get a random number within the move time range
    /// +- the move time limit
    /// </summary>
    /// <returns></returns>
    // private float GetMoveTimeLimit()
    // {
    //     return UnityEngine.Random.Range(moveTimeLimit - moveTimeRange, 
    //                         moveTimeLimit + moveTimeRange);
    // }

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
