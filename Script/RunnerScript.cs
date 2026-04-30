using UnityEngine;
using System.Collections.Generic;

public class RunnerScript : Enemy
{
    public override Transform FindTarget()
    {
        Transform Choosen = CurrentTarget;
        foreach (Transform PossibleTargets in targetGroup)
        {
            if (Choosen == null)
            {
                Choosen = PossibleTargets;
            }

            if (!PossibleTargets.gameObject.activeInHierarchy)
            {
                continue;
            }
            if (Distance(transform.position, PossibleTargets.position) >= Distance(transform.position, Choosen.position))
            {
                Choosen = PossibleTargets;
            }
        }
        return Choosen;
    }

    public override void Update()
    {
        base.Update();
    }
}
