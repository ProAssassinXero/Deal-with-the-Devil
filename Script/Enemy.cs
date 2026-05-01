using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public int targetIndex;
    public int saveTargetIndex;
    public List<Transform> targetGroup;
    public Transform CurrentTarget;
    public NavMeshAgent Agent;

    public MonsterHandler Mananger;
    public GameObject Player;

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
     public virtual void Update()
    {
        if (targetGroup.Count < 1)
        {
            return;
        }

        CurrentTarget = FindTarget();

        if(CurrentTarget)
        {
            Positon();
        }
    }

    public void Positon()
    {
        Agent.SetDestination(CurrentTarget.position);
    }

    public float Distance(Vector2 Pos1 , Vector2 Pos2)
    {
        return Mathf.Abs((Pos1 - Pos2).magnitude);
    }

    public virtual Transform FindTarget()
    {
        Transform Choosen = CurrentTarget;
        foreach (Transform PossibleTargets in targetGroup)
        {
            
            if (!transform.gameObject.activeSelf)
            if (Choosen == null)
            {
                Choosen = PossibleTargets;
                
            }

            if (!PossibleTargets.gameObject.activeInHierarchy)
            {
                continue;
            }
            if (Distance(transform.position, PossibleTargets.position) <= Distance(transform.position, Choosen.position))
            {
                Choosen = PossibleTargets;
            }
        }
        return Choosen;
    }
}