using UnityEngine;

public class AI_Animation : MonoBehaviour
{
    public Animator aiAnimator;
    public AIMovement aiMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (aiAnimator == null)
        {
            aiAnimator = GetComponent<Animator>();
            aiMovement = GetComponent<AIMovement>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (aiMovement.movementDirection.x > 0)
        {
            aiAnimator.SetBool("IsWalkingRight", true);
        }
        else
        {
            aiAnimator.SetBool("IsWalkingRight", false);
        }

        if (aiMovement.movementDirection.x < 0)
        {
            aiAnimator.SetBool("IsWalkingLeft", true);
        }
        else
        {
            aiAnimator.SetBool("IsWalkingLeft", false);
        }

        if (aiMovement.movementDirection.y > 0)
        {
            aiAnimator.SetBool("IsWalkingUp", true);
        }
        else
        {
            aiAnimator.SetBool("IsWalkingUp", false);
        }

        if (aiMovement.movementDirection.y < 0)
        {
            aiAnimator.SetBool("IsWalkingDown", true);
        }
        else
        {
            aiAnimator.SetBool("IsWalkingDown", false);
        }


        if (aiMovement.sitting == true)
        {
            //right
            if (aiMovement.lastFacing.x > 0)
            {
                aiAnimator.SetBool("IsSittingRight", true);
            }
            else
            {
                aiAnimator.SetBool("IsSittingRight", false);
            }
            //left
            if (aiMovement.lastFacing.x < 0)
            {
                aiAnimator.SetBool("IsSittingLeft", true);
            }
            else
            {
                aiAnimator.SetBool("IsSittingLeft", false);
            }
            //up
            if (aiMovement.lastFacing.y > 0)
            {
                aiAnimator.SetBool("IsSittingUp", true);
            }
            else
            {
                aiAnimator.SetBool("IsSittingUp", false);
            }
            //down
            if (aiMovement.lastFacing.y < 0)
            {
                aiAnimator.SetBool("IsSittingDown", true);
            }
            else
            {
                aiAnimator.SetBool("IsSittingDown", false);
            }
        }
    }
}
