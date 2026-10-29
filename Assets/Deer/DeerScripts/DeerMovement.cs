using UnityEngine;
using UnityEngine.AI;

public class DeerMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField] Transform movePoint;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = movePoint.position;
    }
}
