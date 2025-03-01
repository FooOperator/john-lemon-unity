using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PatrolRoute", menuName = "Dynamic Observers/Patrol Route", order = 0)]
public class PatrolRoute : ScriptableObject
{
    [SerializeField] public Vector3[] Waypoints;
    [SerializeField] public float TimePerWaypoint;
}
