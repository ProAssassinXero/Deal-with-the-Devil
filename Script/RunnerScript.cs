using UnityEngine;
using System.Collections.Generic;

public class RunnerScript : Enemy
{
    public GameObject sprite;
    public Vector2 movementDirection;

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
        sprite.transform.position = transform.position;
    }

    public void CalculateDirection(Vector2 posBefore)
    {
        Vector2 displacement = (Vector2)transform.position - posBefore;

        if (displacement.magnitude > 0.0001f)
        {
            Vector2 normalized = displacement.normalized;
            movementDirection = new Vector2(Mathf.Round(normalized.x), Mathf.Round(normalized.y));
        }
        else
        {
            movementDirection = Vector2.zero;
        }
    }
}
