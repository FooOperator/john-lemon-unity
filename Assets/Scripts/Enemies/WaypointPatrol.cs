using System.Collections;
using UnityEngine.AI;
using UnityEngine;

public class WaypointPatrol : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private PatrolRoute patrolRoute;
    [SerializeField] private int nextWaypointIndex;
    [SerializeField] private bool patrolling;
    private IEnumerator e_HoldPosition;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        e_HoldPosition = HoldPosition(patrolRoute.TimePerWaypoint);
    }

    private void Start()
    {
        nextWaypointIndex = 0;
        SetNextDestination(patrolRoute.Waypoints[nextWaypointIndex]);
    }

    private void Update()
    {
        //CheckIfPatrolling();
        if (!(agent.remainingDistance < agent.stoppingDistance)) return;
        //if (patrolling)
        HoldPosition(patrolRoute.TimePerWaypoint);
    }

    //private void CheckIfPatrolling()
    //{
    //    if (agent.velocity > 0) patrolling = true;
    //    else patrolling = false;
    //}

    private void SetNextDestination(Vector3 nextPosition)
    {
        agent.SetDestination(nextPosition);
    }

    private IEnumerator HoldPosition(float time)
    {
        yield return new WaitForSeconds(time);
        SetNextDestination(patrolRoute.Waypoints[nextWaypointIndex]);
        nextWaypointIndex = (nextWaypointIndex + 1) % patrolRoute.Waypoints.Length;
    }
}