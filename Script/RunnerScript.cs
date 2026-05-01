using UnityEngine;
using System.Collections.Generic;

public class RunnerScript : Enemy
{


    public virtual bool CheckHit(Vector2 Ori, Vector2 dir)
    {
        Ray _Ray = new Ray(Ori, dir);
        RaycastHit Hit;
        
        if (Physics.Raycast(_Ray, out Hit))
        {
            Debug.Log(Hit);
            if (Hit.collider.gameObject == Player)
            {
                return true;
            }
        }
        return false;
    }

    public override Transform FindTarget()
    {
        Transform Choosen = CurrentTarget;
        List<Transform> AllPossibleTarger = new List<Transform>(targetGroup);
        foreach (Transform PossibleTargets in targetGroup)
        {
            if (!PossibleTargets.gameObject.activeInHierarchy)
            {
                AllPossibleTarger.Remove(PossibleTargets);
                continue;
            }
            if (Player.transform.position.x < transform.position.x)
            {
                if (transform.position.x > PossibleTargets.position.x)
                {
                    AllPossibleTarger.Remove(PossibleTargets);
                    continue;
                }
            }
            else
            {
                if (transform.position.x > PossibleTargets.position.x)
                {
                    AllPossibleTarger.Remove(PossibleTargets);
                    continue;
                }
            }
            Debug.Log("Pass X");
            if (Player.transform.position.y < transform.position.y)
            {
                if (transform.position.y > PossibleTargets.position.y)
                {
                    AllPossibleTarger.Remove(PossibleTargets);
                    continue;
                }
            }
            else
            {
                if (transform.position.y < PossibleTargets.position.y)
                {
                    AllPossibleTarger.Remove(PossibleTargets);
                    continue;
                }
            }
            Debug.Log("Pass Y");
        }
        foreach (Transform PossibleTargets in AllPossibleTarger)
        {
            if (Choosen == null)
            {
                Choosen = PossibleTargets;
            }
            if ((Distance(transform.position, PossibleTargets.position) <= Distance(transform.position, Choosen.position)))
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
