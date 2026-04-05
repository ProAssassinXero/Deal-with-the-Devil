using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform target;
    NavMeshAgent Agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Agent.updateUpAxis = false;
        Agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        Agent.SetDestination(target.position);
    }
}
