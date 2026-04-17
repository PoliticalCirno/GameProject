using UnityEngine;
using KBCore.Refs;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCMovement : MonoBehaviour
{
    [SerializeField, Self] private NavMeshAgent agent;
    [SerializeField] private List<GameObject> waypoints = new List<GameObject>();
    [SerializeField] private NPCStates currentState;
    [SerializeField] private Transform player;
    private Vector3 destination;
    private int index;
    private void OnValidate() => this.ValidateRefs();

    void Start()
    {
        currentState = NPCStates.Patrol;
        waypoints = GameObject.FindGameObjectsWithTag("waypoint").ToList();
        if (waypoints.Count < 0) return;
        agent.destination = destination = waypoints[index].transform.position;
    }

    void Update()
    {
        switch (currentState)
        {
            case NPCStates.Patrol:
                if (waypoints.Count < 0) return;
                if (Vector3.Distance(transform.position, destination) < 3f)
                {
                    index = (index + 1) % waypoints.Count;
                    destination = waypoints[index].transform.position;
                    agent.destination = destination;
                }
            break;
            case NPCStates.Chase:
                agent.destination = player.position;
            break;
            default:
            break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentState = NPCStates.Chase;
            player = other.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentState = NPCStates.Patrol;
            agent.destination = destination;
        }
    }
}

[System.Serializable]
public enum NPCStates
{
    Patrol, Chase
}
