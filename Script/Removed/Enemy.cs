using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int targetIndex;
    public int saveTargetIndex;
    public bool destinationUpdate = false;
    public Transform[] targetGroup;
    public Transform CurrentTarget;
    public Transform self;
    public NavMeshAgent Agent;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateUpAxis = false;
        Agent.updateRotation = false;
        targetIndex = 0;
        saveTargetIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTarget = FindTarget();

        if(CurrentTarget)
        {
            Positon();
        }
    }

    void Positon()
    {
        Agent.SetDestination(CurrentTarget.position);
        destinationUpdate = false;
    }

    float Distance(Vector2 Pos1 , Vector2 Pos2)
    {
        return (Pos1 - Pos2).magnitude;
    }

    Transform FindTarget()
    {
        Transform Choosen = CurrentTarget;
        foreach (Transform PossibleTargets in targetGroup)
        {
            
            if (!transform.gameObject.active)
            {
                continue;
            }
            if (Distance(transform.position, PossibleTargets.position) < Distance(transform.position, CurrentTarget.position))
            {
                Choosen = PossibleTargets;
            }
        }
        return Choosen;
    }
}