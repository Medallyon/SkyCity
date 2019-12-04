using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIMovement : MonoBehaviour
{
    public static List<Vector3> PatrolPoints = new List<Vector3>();
    public static Vector3 RandomPatrolPoint
    {
        get
        {
            return PatrolPoints[Random.Range(0, PatrolPoints.Count)];
        }
    }

    public NavMeshSurface NavMesh;

    private NavMeshAgent agent;

    public void ImplementNavMeshAgent()
    {
        this.agent = this.GetComponent<NavMeshAgent>();
        this.agent.destination = PatrolPoints[Random.Range(0, PatrolPoints.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        if (this.agent.remainingDistance < this.agent.stoppingDistance)
            this.agent.destination = this.GetRandomPatrolPoint();
    }

    Vector3 GetRandomPatrolPoint()
    {
        return PatrolPoints[Random.Range(0, PatrolPoints.Count)];
    }
}
