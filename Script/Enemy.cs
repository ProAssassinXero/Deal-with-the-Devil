using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
     
    public Transform[] target;
    public int targetIndex;
    public NavMeshAgent Agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Agent.updateUpAxis = false;
        Agent.updateRotation = false;
        targetIndex = 0;
    }

    private void Awake()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        Agent.SetDestination(target[targetIndex].position);
    }
}
