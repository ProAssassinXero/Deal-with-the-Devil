using UnityEngine;
using System.Collections.Generic;

public class RunnerScript : Enemy
{
    void Start()
    {
        int _Random = Random.Range(1, 7);
        CurrentTarget = targetGroup[_Random];
    }

    public override Transform FindTarget()
    {
        if (Distance(transform.position, Player.transform.position) <= 20 && Distance(transform.position, CurrentTarget.position) <5)
        {
            Transform[] Nieghbours = CurrentTarget.GetComponent<RunnerTargetsScript>().Neighbours;
            foreach (Transform Child in Nieghbours)
            {
                if (Distance(Player.transform.position, CurrentTarget.position) < Distance(Player.transform.position, Child.position))
                {
                    CurrentTarget = Child;
                }
            }
        }
        return CurrentTarget;
    }

    public override void Update()
    {
        base.Update();
    }
}
