using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections;

public class DeerAI : MonoBehaviour
{
    // [SerializeField] GameObject deerGO;
    private NavMeshAgent navMeshAgent;
    [SerializeField] float moveTimeLimit;
    [SerializeField] float moveTimeRange;
    private float actualMoveLimit;

    [SerializeField] float maxMoveDist;
    private float moveDist;

    [Tooltip("For debugging")]
    [SerializeField] Transform currMovePoint;
    private bool atMovePoint = false;
    private float startTime = 0;
    private float elapsed = 0;

    private DeerManager deerManager;
    public Transform[] movePoints;

    private DeerInteractable deerInteractable;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        deerInteractable = GetComponent<DeerInteractable>();
        deerManager = GameObject.FindWithTag("DeerManager").GetComponent<DeerManager>();

        // initialize and populate move points 
        movePoints = new Transform[deerManager.movePoints.Length];
        for(int i = 0; i < deerManager.movePoints.Length; i++)
        {
            movePoints[i] = deerManager.movePoints[i];
        }

        currMovePoint = movePoints[GetMovePoint()];
    }

    void Update()
    {
        switch(deerInteractable.currentState)
        {
            case DeerState.Live:
                MoveDeer(currMovePoint);

                // if distance between deer and move point is within threshold
                moveDist = Vector3.Distance(currMovePoint.position, gameObject.transform.position);
                if (moveDist <= maxMoveDist && !atMovePoint) {
                    atMovePoint = true; // set at move point flag
                    startTime = Time.time; // record time entered
                }

                // if deer is at the movePoint
                if (atMovePoint)
                {
                    // start counting
                    elapsed = Time.time - startTime;

                    // if x time has passed
                    // update curr move point
                    if (elapsed >= actualMoveLimit)
                    {
                        currMovePoint = movePoints[GetMovePoint()];
                        atMovePoint = false;
                    }
                }
                break;
            
            case DeerState.Dead:
                // play death animation
                StartCoroutine(LerpFunction(Quaternion.Euler(new Vector3(90,0,0)), 5));
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
    private float GetMoveTimeLimit()
    {
        return UnityEngine.Random.Range(moveTimeLimit - moveTimeRange, 
                            moveTimeLimit + moveTimeRange);
    }

    /// <summary>
    /// Move nav mesh towards move point
    /// </summary>
    /// <param name="point"></param>
    private void MoveDeer(Transform point)
    {
        try {
            navMeshAgent.destination = point.position;
        } catch(Exception e)
        {
            Debug.Log(e);
        }
        
    }

    /// <summary>
    /// Get a random move point
    /// And random time the deer will stay there 
    /// </summary>
    /// <returns></returns>
    private int GetMovePoint()
    {
        actualMoveLimit = GetMoveTimeLimit();
        return (int)UnityEngine.Random.Range(0, movePoints.Length-1);
    }

    // https://gamedevbeginner.com/glossary/how-to-lerp-rotation-in-unity/
    IEnumerator LerpFunction(Quaternion endValue, float duration)
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
